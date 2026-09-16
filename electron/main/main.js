'use strict';

const { app, BrowserWindow, ipcMain, Tray, Menu, globalShortcut, Notification, shell, nativeImage } = require('electron');
const path = require('path');
const { Engine } = require('./engine');

const ICON_PATH = path.join(__dirname, '..', 'build', 'icon.ico');

// Single instance — focus the existing window instead of opening a second one.
// If we don't get the lock, quit immediately and never create a window (otherwise a
// second instance can leave a stray hidden window behind).
const gotLock = app.requestSingleInstanceLock();

let mainWindow = null;
let tray = null;
let engine = null;
let settings = {};
let latestMemory = null;
let isQuitting = false;

// Auto-optimization bookkeeping (mirrors MainViewModel.MonitorApp).
let lastAutoByInterval = Date.now();
let lastAutoByMemory = Date.now();
const AUTO_MEMORY_MIN_INTERVAL_MS = 5 * 60 * 1000; // Constants.App.AutoOptimizationMemoryUsageInterval

/* ------------------------------------------------------------------ Window */

function createWindow() {
  mainWindow = new BrowserWindow({
    width: 1120,
    height: 760,
    minWidth: 880,
    minHeight: 600,
    show: false,
    frame: false,
    titleBarStyle: 'hidden',
    backgroundColor: '#0b0f1a',
    icon: ICON_PATH,
    webPreferences: {
      preload: path.join(__dirname, '..', 'preload', 'preload.js'),
      contextIsolation: true,
      nodeIntegration: false,
      sandbox: false
    }
  });

  mainWindow.loadFile(path.join(__dirname, '..', 'renderer', 'index.html'));

  const reveal = () => {
    if (mainWindow && !mainWindow.isDestroyed() && !mainWindow.isVisible() && !settings.StartMinimized) {
      mainWindow.show();
      mainWindow.focus();
    }
    applyAlwaysOnTop();
  };
  mainWindow.once('ready-to-show', reveal);
  // Fallback: if ready-to-show is missed for any reason, don't leave a hidden window behind.
  mainWindow.webContents.once('did-finish-load', reveal);
  setTimeout(reveal, 3000);

  // Close-to-tray behavior.
  mainWindow.on('close', (e) => {
    if (!isQuitting && settings.CloseToTheNotificationArea) {
      e.preventDefault();
      mainWindow.hide();
    }
  });

  mainWindow.on('closed', () => { mainWindow = null; });

  mainWindow.on('maximize', () => sendToRenderer('window:state', { maximized: true }));
  mainWindow.on('unmaximize', () => sendToRenderer('window:state', { maximized: false }));
}

function sendToRenderer(channel, payload) {
  if (mainWindow && !mainWindow.isDestroyed()) {
    mainWindow.webContents.send(channel, payload);
  }
}

function applyAlwaysOnTop() {
  if (mainWindow) mainWindow.setAlwaysOnTop(!!settings.AlwaysOnTop);
}

/* ------------------------------------------------------------------ Engine */

async function startEngine() {
  engine = new Engine();

  engine.on('event', (msg) => sendToRenderer('engine:event', msg));
  engine.on('engine-exit', (code) => sendToRenderer('engine:status', { running: false, code }));
  engine.on('engine-error', (err) => sendToRenderer('engine:status', { running: false, error: String(err) }));

  engine.start();
  const info = await engine.whenReady();
  sendToRenderer('engine:status', { running: true, elevated: info.elevated });

  // Load persisted settings and apply side effects.
  settings = await engine.request('getSettings');
  applyPriority();
  registerHotkey();
  applyLoginItem();

  startPolling();
}

function applyPriority() {
  const priority = settings.RunOnPriority === 0 ? 'low' : 'normal';
  engine.request('setPriority', { priority }).catch(() => {});
}

function applyLoginItem() {
  try {
    app.setLoginItemSettings({ openAtLogin: !!settings.RunOnStartup, path: process.execPath });
  } catch { /* ignored */ }
}

/* -------------------------------------------------------------- Monitoring */

function startPolling() {
  const poll = async () => {
    if (!engine || !engine.ready) return;
    try {
      latestMemory = await engine.request('getMemory');
      sendToRenderer('memory:update', latestMemory);
      updateTrayTooltip();
      evaluateAutoOptimization();
    } catch { /* transient */ }
  };
  poll();
  setInterval(poll, 2000); // matches the original 5s? Original polls every 5s; 2s feels more alive.
}

function evaluateAutoOptimization() {
  if (!latestMemory) return;
  const now = Date.now();

  // Interval-based (hours)
  const intervalHours = settings.AutoOptimizationInterval || 0;
  if (intervalHours > 0 && (now - lastAutoByInterval) / 3600000 >= intervalHours) {
    lastAutoByInterval = now;
    runOptimize('Schedule');
    return;
  }

  // Low-memory threshold (% free)
  const threshold = settings.AutoOptimizationMemoryUsage || 0;
  if (threshold > 0 &&
      latestMemory.physical.freePercent < threshold &&
      (now - lastAutoByMemory) >= AUTO_MEMORY_MIN_INTERVAL_MS) {
    lastAutoByMemory = now;
    runOptimize('LowMemory');
  }
}

/* -------------------------------------------------------------- Optimize */

let optimizing = false;

async function runOptimize(reason) {
  if (optimizing || !engine || !engine.ready) return null;
  optimizing = true;
  sendToRenderer('optimize:state', { running: true, reason });
  try {
    const result = await engine.request('optimize', {
      areas: settings.MemoryAreas || 0,
      processExclusion: settings.ProcessExclusionList || [],
      reason
    });

    if (settings.ShowOptimizationNotifications) notifyOptimized(result, reason);
    sendToRenderer('optimize:result', result);
    return result;
  } catch (err) {
    sendToRenderer('optimize:error', { message: String(err.message || err) });
    return null;
  } finally {
    optimizing = false;
    sendToRenderer('optimize:state', { running: false });
    if (reason === 'Manual' && settings.CloseAfterOptimization) {
      isQuitting = true;
      app.quit();
    }
  }
}

function formatBytes(bytes) {
  if (!bytes || bytes < 0) bytes = 0;
  const units = ['B', 'KB', 'MB', 'GB', 'TB'];
  let i = 0, v = bytes;
  while (v >= 1024 && i < units.length - 1) { v /= 1024; i++; }
  return `${v.toFixed(1)} ${units[i]}`;
}

function notifyOptimized(result, reason) {
  if (!Notification.isSupported()) return;
  const phys = formatBytes(result.released.physical);
  const body = settings.ShowVirtualMemory
    ? `Reason: ${reason}\nPhysical: ${phys}\nVirtual: ${formatBytes(result.released.virtual)}`
    : `Reason: ${reason}\nPhysical freed: ${phys}`;
  new Notification({ title: 'Memory optimized', body, icon: ICON_PATH, silent: true }).show();
}

/* ------------------------------------------------------------------- Tray */

function createTray() {
  let image = nativeImage.createFromPath(ICON_PATH);
  if (!image.isEmpty()) image = image.resize({ width: 16, height: 16 });
  tray = new Tray(image.isEmpty() ? ICON_PATH : image);
  tray.setToolTip('Windows Memory Cleaner');
  rebuildTrayMenu();

  tray.on('double-click', showWindow);
  tray.on('middle-click', () => {
    if (settings.TrayIconOptimizeOnMiddleMouseClick) runOptimize('Manual');
  });
}

function rebuildTrayMenu() {
  if (!tray) return;
  const menu = Menu.buildFromTemplate([
    { label: 'Optimize now', click: () => runOptimize('Manual') },
    { type: 'separator' },
    { label: 'Show', click: showWindow },
    { label: 'Hide', click: () => mainWindow && mainWindow.hide() },
    { type: 'separator' },
    { label: 'GitHub', click: () => shell.openExternal('https://github.com/IgorMundstein/WinMemoryCleaner') },
    { label: 'Exit', click: () => { isQuitting = true; app.quit(); } }
  ]);
  tray.setContextMenu(menu);
}

function updateTrayTooltip() {
  if (tray && latestMemory) {
    tray.setToolTip(`Windows Memory Cleaner — RAM ${latestMemory.physical.usedPercent}% used`);
  }
}

function showWindow() {
  if (!mainWindow) createWindow();
  else { mainWindow.show(); mainWindow.focus(); }
}

/* --------------------------------------------------------------- Hotkey */

function accelerator() {
  const mods = (settings.HotkeyModifiers || 'Control+Shift').split('+').map((m) => m.trim()).filter(Boolean);
  const key = settings.HotkeyKey || 'M';
  return [...mods, key].join('+');
}

function registerHotkey() {
  globalShortcut.unregisterAll();
  if (!settings.UseHotkey) return true;
  try {
    return globalShortcut.register(accelerator(), () => runOptimize('Manual'));
  } catch {
    return false;
  }
}

/* --------------------------------------------------------------- IPC */

function registerIpc() {
  ipcMain.handle('engine:getState', async () => {
    const state = await engine.request('getState');
    settings = state.settings;
    return state;
  });
  ipcMain.handle('engine:getMemory', () => latestMemory || engine.request('getMemory'));
  ipcMain.handle('engine:getProcesses', () => engine.request('getProcesses'));
  ipcMain.handle('engine:getSettings', async () => {
    settings = await engine.request('getSettings');
    return settings;
  });

  ipcMain.handle('engine:saveSettings', async (_e, next) => {
    settings = { ...settings, ...next };
    await engine.request('saveSettings', settings);
    // Apply side effects
    applyPriority();
    applyAlwaysOnTop();
    applyLoginItem();
    const hotkeyOk = registerHotkey();
    rebuildTrayMenu();
    return { ok: true, hotkeyOk };
  });

  ipcMain.handle('engine:optimize', (_e, reason) => runOptimize(reason || 'Manual'));

  // Window controls
  ipcMain.handle('window:minimize', () => mainWindow && mainWindow.minimize());
  ipcMain.handle('window:toggleMaximize', () => {
    if (!mainWindow) return false;
    if (mainWindow.isMaximized()) mainWindow.unmaximize();
    else mainWindow.maximize();
    return mainWindow.isMaximized();
  });
  ipcMain.handle('window:close', () => mainWindow && mainWindow.close());
  ipcMain.handle('window:isMaximized', () => !!(mainWindow && mainWindow.isMaximized()));

  ipcMain.handle('app:info', () => ({
    version: app.getVersion(),
    electron: process.versions.electron,
    chrome: process.versions.chrome,
    node: process.versions.node,
    elevated: engine ? engine.elevated : false
  }));

  ipcMain.handle('shell:openExternal', (_e, url) => shell.openExternal(url));
}

/* --------------------------------------------------------------- Lifecycle */

if (!gotLock) {
  app.quit();
} else {
  app.on('second-instance', showWindow);

  app.whenReady().then(async () => {
    createWindow();
    createTray();
    registerIpc();
    try {
      await startEngine();
    } catch (err) {
      sendToRenderer('engine:status', { running: false, error: String(err) });
    }
  });
}

app.on('window-all-closed', () => {
  // Keep running in tray on Windows unless explicitly quitting.
  if (process.platform !== 'darwin' && isQuitting) app.quit();
});

app.on('before-quit', () => { isQuitting = true; });

app.on('will-quit', () => {
  globalShortcut.unregisterAll();
  if (engine) engine.stop();
});
