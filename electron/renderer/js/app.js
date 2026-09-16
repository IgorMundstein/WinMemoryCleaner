'use strict';

/* ==========================================================================
   Windows Memory Cleaner — renderer
   Talks only to window.wmc (see preload). No Node here.
   ========================================================================== */

const api = window.wmc;
const $ = (sel, root = document) => root.querySelector(sel);
const $$ = (sel, root = document) => [...root.querySelectorAll(sel)];

const RING_C = 540; // 2 * PI * 86, matches CSS stroke-dasharray

// Memory-area metadata. Bit values match the engine's MemoryAreas enum exactly.
const AREAS = [
  { bit: 128, key: 'WorkingSet',            name: 'Working Set',            sub: "Processes' active RAM" },
  { bit: 16,  key: 'StandbyList',           name: 'Standby List',           sub: 'Cached standby pages' },
  { bit: 32,  key: 'StandbyListLowPriority', name: 'Standby (low priority)', sub: 'Low-priority cache' },
  { bit: 4,   key: 'ModifiedPageList',      name: 'Modified Page List',     sub: 'Pending writes' },
  { bit: 64,  key: 'SystemFileCache',       name: 'System File Cache',      sub: 'Cached file data' },
  { bit: 2,   key: 'ModifiedFileCache',     name: 'Modified File Cache',    sub: 'Dirty file pages' },
  { bit: 1,   key: 'CombinedPageList',      name: 'Combined Page List',     sub: 'Deduplicated pages' },
  { bit: 8,   key: 'RegistryCache',         name: 'Registry Cache',         sub: 'Registry hive cache' }
];

const BEHAVIOR_TOGGLES = [
  { key: 'AlwaysOnTop',                 label: 'Always on top' },
  { key: 'StartMinimized',              label: 'Start minimized' },
  { key: 'CloseToTheNotificationArea',  label: 'Close to tray' },
  { key: 'CloseAfterOptimization',      label: 'Close after optimizing' },
  { key: 'RunOnStartup',                label: 'Run on Windows startup' },
  { key: 'ShowOptimizationNotifications', label: 'Optimization notifications' },
  { key: 'ShowVirtualMemory',           label: 'Show virtual memory' },
  { key: 'RunOnLowPriority',            label: 'Run on low priority', derived: true },
  { key: 'CompactMode',                 label: 'Compact mode' },
  { key: 'AutoUpdate',                  label: 'Check for updates' }
];

const LANGUAGES = [
  ['en', 'English'], ['it', 'Italiano'], ['de', 'Deutsch'], ['fr', 'Français'],
  ['es', 'Español'], ['pt-BR', 'Português (BR)'], ['nl', 'Nederlands'], ['pl', 'Polski'],
  ['ru', 'Русский'], ['tr', 'Türkçe'], ['zh-Hans', '简体中文'], ['zh-Hant', '繁體中文'],
  ['ja', '日本語'], ['ko', '한국어'], ['ar', 'العربية']
];

const HOTKEY_KEYS = ['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
  'F1','F2','F3','F4','F5','F6','F7','F8','F9','F10','F11','F12'];

const state = {
  settings: {},
  os: null,
  memory: null,
  optimizing: false,
  history: []
};

/* ------------------------------------------------ utils */
function formatBytes(bytes) {
  if (!bytes || bytes < 0) bytes = 0;
  const units = ['B', 'KB', 'MB', 'GB', 'TB'];
  let i = 0, v = bytes;
  while (v >= 1024 && i < units.length - 1) { v /= 1024; i++; }
  return `${v.toFixed(v >= 100 || i === 0 ? 0 : 1)} ${units[i]}`;
}

function toast(title, body, isError = false) {
  const host = $('#toastHost');
  const el = document.createElement('div');
  el.className = 'toast' + (isError ? ' err' : '');
  el.innerHTML = `<div class="t-title"><svg class="ic"><use href="#${isError ? 'i-close' : 'i-check'}"></use></svg>${title}</div>` +
                 (body ? `<div class="t-body">${body}</div>` : '');
  host.appendChild(el);
  setTimeout(() => { el.classList.add('out'); setTimeout(() => el.remove(), 400); }, isError ? 5200 : 4200);
}

/* ------------------------------------------------ persistence */
let saveTimer = null;
function saveSoon() {
  clearTimeout(saveTimer);
  saveTimer = setTimeout(async () => {
    try {
      const res = await api.saveSettings(state.settings);
      if (res && res.hotkeyOk === false && state.settings.UseHotkey) {
        showHotkeyWarn(true);
      }
    } catch (e) { toast('Could not save', String(e.message || e), true); }
  }, 180);
}

/* ------------------------------------------------ navigation */
function initNav() {
  $$('.nav-item').forEach((btn) => {
    btn.addEventListener('click', () => {
      $$('.nav-item').forEach((b) => b.classList.toggle('active', b === btn));
      const view = btn.dataset.view;
      $$('.view').forEach((v) => v.classList.toggle('active', v.id === `view-${view}`));
      if (view === 'processes') refreshProcesses();
    });
  });
}

/* ------------------------------------------------ window controls */
function initWindowControls() {
  $('#btnMin').addEventListener('click', () => api.window.minimize());
  $('#btnMax').addEventListener('click', () => api.window.toggleMaximize());
  $('#btnClose').addEventListener('click', () => api.window.close());
}

/* ------------------------------------------------ theme */
function applyTheme(theme) {
  document.documentElement.setAttribute('data-theme', theme);
  const useIcon = theme === 'dark' ? 'i-moon' : 'i-sun';
  $('#themeToggle').innerHTML = `<svg class="ic"><use href="#${useIcon}"></use></svg>`;
  $$('#themeSeg button').forEach((b) => b.classList.toggle('active', b.dataset.themeVal === theme));
}
function setTheme(theme) {
  state.settings.Theme = theme;
  applyTheme(theme);
  saveSoon();
}

/* ------------------------------------------------ gauges */
function setRing(arcId, pctId, cardId, pct, showNa = false) {
  const arc = $(arcId), lbl = $(pctId);
  if (showNa) {
    arc.style.strokeDashoffset = RING_C;
    lbl.textContent = '—'; lbl.classList.add('na');
    return;
  }
  lbl.classList.remove('na');
  const p = Math.max(0, Math.min(100, pct));
  arc.style.strokeDashoffset = RING_C * (1 - p / 100);
  lbl.textContent = Math.round(p);
}

function renderMemory(mem) {
  state.memory = mem;
  const phys = mem.physical, virt = mem.virtual;

  setRing('#physArc', '#physPct', '#physCard', phys.usedPercent);
  $('#physUsed').textContent = formatBytes(phys.used);
  $('#physFree').textContent = formatBytes(phys.free);
  $('#physTotal').textContent = formatBytes(phys.total);

  setRing('#virtArc', '#virtPct', '#virtCard', virt.usedPercent);
  $('#virtUsed').textContent = formatBytes(virt.used);
  $('#virtFree').textContent = formatBytes(virt.free);
  $('#virtTotal').textContent = formatBytes(virt.total);

  $('#dashSubtitle').textContent = `${formatBytes(phys.used)} of ${formatBytes(phys.total)} in use`;
  $('#sparkNow').textContent = `${phys.usedPercent}%`;

  pushHistory(phys.usedPercent);
}

function pushHistory(pct) {
  state.history.push(pct);
  if (state.history.length > 60) state.history.shift();
  const h = state.history;
  if (h.length < 2) return;
  const W = 300, H = 90, pad = 4;
  const step = W / (h.length - 1);
  const y = (v) => H - pad - (v / 100) * (H - pad * 2);
  let line = `M0 ${y(h[0]).toFixed(1)}`;
  h.forEach((v, i) => { if (i) line += ` L${(i * step).toFixed(1)} ${y(v).toFixed(1)}`; });
  $('#sparkLine').setAttribute('d', line);
  $('#sparkArea').setAttribute('d', `${line} L${W} ${H} L0 ${H} Z`);
}

/* ------------------------------------------------ areas */
function renderAreas() {
  const grid = $('#areasGrid');
  const supported = state.os ? state.os.supportedAreas : 0xFF;
  grid.innerHTML = '';
  AREAS.forEach((a) => {
    const isSupported = (supported & a.bit) !== 0;
    const on = (state.settings.MemoryAreas & a.bit) !== 0;
    const chip = document.createElement('button');
    chip.className = 'area-chip' + (on ? ' on' : '') + (isSupported ? '' : ' disabled');
    chip.innerHTML =
      `<span class="area-box"><svg class="ic"><use href="#i-check"></use></svg></span>` +
      `<span><span class="area-name">${a.name}</span><br><span class="area-sub">${a.sub}</span></span>`;
    if (isSupported) chip.addEventListener('click', () => toggleArea(a));
    grid.appendChild(chip);
  });
}

function toggleArea(a) {
  let areas = state.settings.MemoryAreas || 0;
  const turningOn = (areas & a.bit) === 0;
  areas ^= a.bit;
  // Standby list and its low-priority variant are mutually exclusive (original behavior).
  if (turningOn && a.bit === 16) areas &= ~32;
  if (turningOn && a.bit === 32) areas &= ~16;
  state.settings.MemoryAreas = areas;
  renderAreas();
  updateOptimizeEnabled();
  saveSoon();
}

function updateOptimizeEnabled() {
  const has = (state.settings.MemoryAreas || 0) !== 0;
  $('#optimizeBtn').disabled = !has || state.optimizing;
}

/* ------------------------------------------------ optimize */
function initOptimize() {
  $('#optimizeBtn').addEventListener('click', () => { if (!state.optimizing) api.optimize('Manual'); });
  $('#selectAll').addEventListener('click', () => {
    state.settings.MemoryAreas = state.os ? state.os.supportedAreas & ~32 : 0xFF & ~32;
    renderAreas(); updateOptimizeEnabled(); saveSoon();
  });
  $('#selectNone').addEventListener('click', () => {
    state.settings.MemoryAreas = 0; renderAreas(); updateOptimizeEnabled(); saveSoon();
  });
}

function setOptimizing(running) {
  state.optimizing = running;
  const btn = $('#optimizeBtn');
  btn.classList.toggle('running', running);
  updateOptimizeEnabled();
  if (!running) {
    $('#optFill').style.width = '0%';
    $('#optText').textContent = 'Optimize';
  }
}

/* ------------------------------------------------ automation */
function initAutomation() {
  const interval = $('#autoInterval'), mem = $('#autoMem');

  interval.addEventListener('input', () => {
    const v = +interval.value;
    $('#autoIntervalOut').textContent = v === 0 ? 'Off' : `${v} h`;
    $('#autoIntervalDesc').textContent = v === 0 ? 'Disabled' : `Every ${v} hour${v > 1 ? 's' : ''}`;
  });
  interval.addEventListener('change', () => {
    state.settings.AutoOptimizationInterval = +interval.value; saveSoon();
  });

  mem.addEventListener('input', () => {
    const v = +mem.value;
    $('#autoMemOut').textContent = v === 0 ? 'Off' : `${v}%`;
    $('#autoMemDesc').textContent = v === 0 ? 'Disabled' : `When free RAM is below ${v}%`;
  });
  mem.addEventListener('change', () => {
    state.settings.AutoOptimizationMemoryUsage = +mem.value; saveSoon();
  });
}

function syncAutomation() {
  const iv = state.settings.AutoOptimizationInterval || 0;
  const mv = state.settings.AutoOptimizationMemoryUsage || 0;
  $('#autoInterval').value = iv;
  $('#autoMem').value = mv;
  $('#autoIntervalOut').textContent = iv === 0 ? 'Off' : `${iv} h`;
  $('#autoIntervalDesc').textContent = iv === 0 ? 'Disabled' : `Every ${iv} hour${iv > 1 ? 's' : ''}`;
  $('#autoMemOut').textContent = mv === 0 ? 'Off' : `${mv}%`;
  $('#autoMemDesc').textContent = mv === 0 ? 'Disabled' : `When free RAM is below ${mv}%`;
}

/* ------------------------------------------------ processes */
async function refreshProcesses() {
  try {
    const procs = await api.getProcesses();
    const excluded = new Set((state.settings.ProcessExclusionList || []).map((s) => s.toLowerCase()));
    const sel = $('#procSelect');
    sel.innerHTML = '';
    procs.filter((p) => !excluded.has(p.toLowerCase())).forEach((p) => {
      const o = document.createElement('option'); o.value = p; o.textContent = p; sel.appendChild(o);
    });
  } catch (e) { /* ignore */ }
  renderExclusionList();
}

function renderExclusionList() {
  const list = $('#procList');
  const items = state.settings.ProcessExclusionList || [];
  list.innerHTML = '';
  if (!items.length) { list.innerHTML = '<div class="proc-empty">No excluded processes</div>'; return; }
  items.forEach((name) => {
    const row = document.createElement('div');
    row.className = 'proc-item';
    row.innerHTML = `<span>${name}</span><button title="Remove"><svg class="ic"><use href="#i-trash"></use></svg></button>`;
    row.querySelector('button').addEventListener('click', () => {
      state.settings.ProcessExclusionList = items.filter((n) => n !== name);
      saveSoon(); refreshProcesses();
    });
    list.appendChild(row);
  });
}

function initProcesses() {
  $('#procAdd').addEventListener('click', () => {
    const val = $('#procSelect').value;
    if (!val) return;
    const list = state.settings.ProcessExclusionList || [];
    if (!list.includes(val)) { list.push(val); list.sort(); state.settings.ProcessExclusionList = list; saveSoon(); refreshProcesses(); }
  });
  $('#procRefresh').addEventListener('click', refreshProcesses);
}

/* ------------------------------------------------ settings toggles */
function getToggle(key) {
  if (key === 'RunOnLowPriority') return (state.settings.RunOnPriority || 0) === 0;
  return !!state.settings[key];
}
function setToggle(key, val) {
  if (key === 'RunOnLowPriority') { state.settings.RunOnPriority = val ? 0 : 1; }
  else { state.settings[key] = val; }
  // Live UI effects
  if (key === 'ShowVirtualMemory') $('#virtCard').style.display = val ? '' : 'none';
  saveSoon();
}

function renderToggles() {
  const grid = $('#toggleGrid');
  grid.innerHTML = '';
  BEHAVIOR_TOGGLES.forEach((t) => {
    const row = document.createElement('div');
    row.className = 'toggle-row';
    row.innerHTML = `<span class="label">${t.label}</span>
      <label class="switch"><input type="checkbox" ${getToggle(t.key) ? 'checked' : ''}/>
        <span class="track"></span><span class="thumb"></span></label>`;
    row.querySelector('input').addEventListener('change', (e) => setToggle(t.key, e.target.checked));
    grid.appendChild(row);
  });
  $('#virtCard').style.display = state.settings.ShowVirtualMemory ? '' : 'none';
}

/* ------------------------------------------------ hotkey */
function showHotkeyWarn(show) {
  const el = $('#hotkeyWarn');
  el.hidden = !show;
  el.className = 'hint-note warn';
  el.innerHTML = show ? `<svg class="ic"><use href="#i-info"></use></svg> This shortcut is already in use by the system. Choose another.` : '';
}

function currentAccelerator() {
  const mods = [];
  if ($('#hkCtrl').checked) mods.push('Control');
  if ($('#hkShift').checked) mods.push('Shift');
  if ($('#hkAlt').checked) mods.push('Alt');
  return { modifiers: mods.join('+'), key: $('#hkKey').value };
}
function updateHotkeyHint() {
  const parts = [];
  if ($('#hkCtrl').checked) parts.push('Ctrl');
  if ($('#hkShift').checked) parts.push('Shift');
  if ($('#hkAlt').checked) parts.push('Alt');
  parts.push($('#hkKey').value);
  $('#ctaHint').textContent = state.settings.UseHotkey ? parts.join(' + ') : 'Hotkey off';
}

function initHotkey() {
  const key = $('#hkKey');
  HOTKEY_KEYS.forEach((k) => { const o = document.createElement('option'); o.value = k; o.textContent = k; key.appendChild(o); });

  const onChange = () => {
    const { modifiers, key: k } = currentAccelerator();
    state.settings.UseHotkey = $('#useHotkey').checked;
    state.settings.HotkeyModifiers = modifiers || 'Control+Shift';
    state.settings.HotkeyKey = k;
    showHotkeyWarn(false);
    updateHotkeyHint();
    saveSoon();
  };
  ['#useHotkey', '#hkCtrl', '#hkShift', '#hkAlt', '#hkKey'].forEach((s) => $(s).addEventListener('change', onChange));
}

function syncHotkey() {
  const mods = (state.settings.HotkeyModifiers || 'Control+Shift').split('+');
  $('#useHotkey').checked = !!state.settings.UseHotkey;
  $('#hkCtrl').checked = mods.includes('Control');
  $('#hkShift').checked = mods.includes('Shift');
  $('#hkAlt').checked = mods.includes('Alt');
  $('#hkKey').value = state.settings.HotkeyKey || 'M';
  updateHotkeyHint();
}

/* ------------------------------------------------ appearance */
function initAppearance() {
  $('#themeToggle').addEventListener('click', () => setTheme(state.settings.Theme === 'dark' ? 'light' : 'dark'));
  $$('#themeSeg button').forEach((b) => b.addEventListener('click', () => setTheme(b.dataset.themeVal)));

  const lang = $('#langSelect');
  LANGUAGES.forEach(([code, name]) => { const o = document.createElement('option'); o.value = code; o.textContent = name; lang.appendChild(o); });
  lang.addEventListener('change', () => { state.settings.Language = lang.value; saveSoon(); });
}

/* ------------------------------------------------ reset & about */
function initReset() {
  $('#resetBtn').addEventListener('click', async () => {
    const keepLang = state.settings.Language;
    state.settings = {
      AlwaysOnTop: false, AutoOptimizationInterval: 0, AutoOptimizationMemoryUsage: 0, AutoUpdate: true,
      CloseAfterOptimization: false, CloseToTheNotificationArea: false, CompactMode: false, CreateStartMenuShortcut: true,
      FontSize: 14, Language: keepLang, MemoryAreas: 223, RunOnPriority: 0, RunOnStartup: false,
      ShowOptimizationNotifications: true, ShowVirtualMemory: false, StartMinimized: false, UseHotkey: false,
      HotkeyModifiers: 'Control+Shift', HotkeyKey: 'M', Theme: 'dark',
      ProcessExclusionList: []
    };
    await api.saveSettings(state.settings);
    hydrateUI();
    toast('Settings reset', 'Restored to default configuration');
  });

  $$('[data-ext]').forEach((b) => b.addEventListener('click', () => api.openExternal(b.dataset.ext)));
}

async function loadAbout() {
  try {
    const info = await api.appInfo();
    $('#appVersion').textContent = info.version;
    $('#aboutFacts').innerHTML = `
      <li><span class="k">App version</span><span>${info.version}</span></li>
      <li><span class="k">Electron</span><span>${info.electron}</span></li>
      <li><span class="k">Chromium</span><span>${info.chrome}</span></li>
      <li><span class="k">Node</span><span>${info.node}</span></li>
      <li><span class="k">Engine</span><span>native C# (P/Invoke)</span></li>
      <li><span class="k">Privileges</span><span>${info.elevated ? 'Administrator' : 'Standard user'}</span></li>`;
  } catch { /* ignore */ }
}

/* ------------------------------------------------ hydrate */
function hydrateUI() {
  applyTheme(state.settings.Theme || 'dark');
  renderAreas();
  renderToggles();
  syncAutomation();
  syncHotkey();
  updateOptimizeEnabled();
  const lang = $('#langSelect'); if (lang) lang.value = state.settings.Language || 'en';
  renderExclusionList();
}

/* ------------------------------------------------ engine events */
function initEngineEvents() {
  api.onMemory((mem) => renderMemory(mem));

  api.onEngineStatus((s) => {
    const chip = $('#engineChip');
    chip.classList.toggle('ok', !!s.running);
    chip.classList.toggle('err', !s.running);
    chip.lastChild.textContent = s.running ? ' Engine' : ' Engine offline';
    if (s.running && s.elevated) $('#elevChip').hidden = false;
    if (s.error) toast('Engine problem', s.error, true);
  });

  api.onEngineEvent((msg) => {
    if (msg.event === 'optimizeStart') { $('#optText').textContent = 'Starting…'; }
    if (msg.event === 'progress') {
      const d = msg.data;
      $('#optFill').style.width = `${d.percent}%`;
      $('#optText').textContent = `${d.name}…`;
    }
  });

  api.onOptimizeState((s) => setOptimizing(s.running));

  api.onOptimizeResult((r) => {
    const failed = (r.results || []).filter((x) => !x.success);
    const phys = formatBytes(r.released.physical);
    const virt = formatBytes(r.released.virtual);
    const body = state.settings.ShowVirtualMemory
      ? `Physical freed: ${phys}\nVirtual freed: ${virt}${failed.length ? `\n${failed.length} area(s) skipped` : ''}`
      : `Physical freed: ${phys}${failed.length ? `\n${failed.length} area(s) skipped` : ''}`;
    toast('Memory optimized', body, false);
    renderMemory({ physical: r.after.physical, virtual: r.after.virtual });
  });

  api.onOptimizeError((e) => toast('Optimization failed', e.message, true));
}

/* ------------------------------------------------ boot */
async function boot() {
  initNav();
  initWindowControls();
  initOptimize();
  initAutomation();
  initProcesses();
  initHotkey();
  initAppearance();
  initReset();
  initEngineEvents();

  try {
    const st = await api.getState();
    state.settings = st.settings;
    state.os = st.os;
    if (st.memory) renderMemory(st.memory);
    if (st.elevated) $('#elevChip').hidden = false;
    $('#engineChip').classList.add('ok');
  } catch (e) {
    // Engine may still be starting; fall back to settings only.
    try { state.settings = await api.getSettings(); } catch { state.settings = {}; }
  }

  hydrateUI();
  loadAbout();
}

document.addEventListener('DOMContentLoaded', boot);
