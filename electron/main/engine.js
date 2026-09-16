'use strict';

const { spawn } = require('child_process');
const readline = require('readline');
const path = require('path');
const fs = require('fs');
const { EventEmitter } = require('events');

/**
 * Bridge to the native C# engine (wmc-engine).
 *
 * Communicates over newline-delimited JSON on stdin/stdout:
 *   - request/response are correlated by an incrementing numeric id
 *   - unsolicited "event" messages (progress, ready) are re-emitted on this EventEmitter
 *
 * The engine binary is manifested `requireAdministrator`. It therefore only launches
 * when the Electron process is itself elevated. For non-elevated development the bridge
 * falls back to `dotnet <dll>`, which bypasses the exe manifest so the UI, live memory
 * and settings all work (privileged optimizations will simply report per-area errors).
 */
class Engine extends EventEmitter {
  constructor() {
    super();
    this.proc = null;
    this.rl = null;
    this._nextId = 1;
    this._pending = new Map();
    this.ready = false;
    this.elevated = false;
    this._readyResolvers = [];
  }

  _resolveEnginePaths() {
    // Packaged: resources/engine/wmc-engine.exe
    const packaged = path.join(process.resourcesPath || '', 'engine', 'wmc-engine.exe');
    // Dev build output
    const devDir = path.join(__dirname, '..', 'engine', 'bin', 'Release', 'net10.0-windows');
    const devExe = path.join(devDir, 'wmc-engine.exe');
    const devDll = path.join(devDir, 'wmc-engine.dll');
    return { packaged, devExe, devDll };
  }

  _candidates() {
    const { packaged, devExe, devDll } = this._resolveEnginePaths();
    const viaDotnet = process.env.WMC_ENGINE_VIA_DOTNET === '1';
    const list = [];

    if (fs.existsSync(packaged)) list.push({ command: packaged, args: [] });
    if (!viaDotnet && fs.existsSync(devExe)) list.push({ command: devExe, args: [] });
    // dotnet fallback bypasses the exe's requireAdministrator manifest (dev, non-elevated).
    if (fs.existsSync(devDll)) list.push({ command: 'dotnet', args: [devDll] });
    if (viaDotnet && fs.existsSync(devExe)) list.push({ command: devExe, args: [] });

    if (!list.length) throw new Error('wmc-engine not found. Run "npm run build:engine" first.');
    return list;
  }

  start() {
    this._launchQueue = this._candidates();
    this._spawnNext();
    return this;
  }

  _spawnNext() {
    const next = this._launchQueue.shift();
    if (!next) {
      this.emit('engine-error', new Error('Failed to launch wmc-engine (elevation may be required).'));
      return;
    }

    let spawnFailed = false;
    this.proc = spawn(next.command, next.args, {
      stdio: ['pipe', 'pipe', 'pipe'],
      windowsHide: true
    });

    this.proc.on('error', (err) => {
      // Elevation failure or missing binary: try the next candidate (e.g. dotnet DLL).
      spawnFailed = true;
      if (this._launchQueue.length) this._spawnNext();
      else this.emit('engine-error', err);
    });

    this.proc.on('exit', (code) => {
      if (spawnFailed) return;
      this.ready = false;
      this.emit('engine-exit', code);
    });

    this.proc.stderr.on('data', (d) => this.emit('engine-stderr', d.toString()));

    this.rl = readline.createInterface({ input: this.proc.stdout });
    this.rl.on('line', (line) => this._onLine(line));
  }

  _onLine(line) {
    line = line.trim();
    if (!line) return;

    let msg;
    try {
      msg = JSON.parse(line);
    } catch {
      return; // ignore non-JSON noise
    }

    if (msg.type === 'ready') {
      this.ready = true;
      this.elevated = !!msg.elevated;
      this.emit('ready', msg);
      this._readyResolvers.splice(0).forEach((r) => r(msg));
      return;
    }

    if (msg.type === 'event') {
      this.emit('event', msg);
      this.emit(`event:${msg.event}`, msg);
      return;
    }

    // Response to a request
    if (typeof msg.id === 'number' && this._pending.has(msg.id)) {
      const { resolve, reject } = this._pending.get(msg.id);
      this._pending.delete(msg.id);
      if (msg.ok) resolve(msg.result);
      else reject(new Error(msg.error || 'Engine error'));
    }
  }

  whenReady() {
    if (this.ready) return Promise.resolve({ elevated: this.elevated });
    return new Promise((resolve) => this._readyResolvers.push(resolve));
  }

  request(cmd, args = {}) {
    if (!this.proc || !this.proc.stdin.writable) {
      return Promise.reject(new Error('Engine is not running'));
    }
    const id = this._nextId++;
    const payload = JSON.stringify({ id, cmd, args }) + '\n';
    return new Promise((resolve, reject) => {
      this._pending.set(id, { resolve, reject });
      this.proc.stdin.write(payload, (err) => {
        if (err) {
          this._pending.delete(id);
          reject(err);
        }
      });
      // Safety timeout (optimize can be slow, so keep it generous)
      setTimeout(() => {
        if (this._pending.has(id)) {
          this._pending.delete(id);
          reject(new Error(`Engine request "${cmd}" timed out`));
        }
      }, 120000);
    });
  }

  stop() {
    try { this.rl && this.rl.close(); } catch {}
    try { this.proc && this.proc.stdin.end(); } catch {}
    try { this.proc && this.proc.kill(); } catch {}
    this.proc = null;
  }
}

module.exports = { Engine };
