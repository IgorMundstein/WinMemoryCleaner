'use strict';

const { contextBridge, ipcRenderer } = require('electron');

/**
 * The only surface the renderer can touch. No Node, no direct IPC — just this
 * explicit, minimal API. Event subscriptions return an unsubscribe function.
 */
const listen = (channel) => (callback) => {
  const handler = (_event, payload) => callback(payload);
  ipcRenderer.on(channel, handler);
  return () => ipcRenderer.removeListener(channel, handler);
};

contextBridge.exposeInMainWorld('wmc', {
  // Engine / data
  getState: () => ipcRenderer.invoke('engine:getState'),
  getMemory: () => ipcRenderer.invoke('engine:getMemory'),
  getProcesses: () => ipcRenderer.invoke('engine:getProcesses'),
  getSettings: () => ipcRenderer.invoke('engine:getSettings'),
  saveSettings: (settings) => ipcRenderer.invoke('engine:saveSettings', settings),
  optimize: (reason) => ipcRenderer.invoke('engine:optimize', reason),

  // App / shell
  appInfo: () => ipcRenderer.invoke('app:info'),
  openExternal: (url) => ipcRenderer.invoke('shell:openExternal', url),

  // Window controls
  window: {
    minimize: () => ipcRenderer.invoke('window:minimize'),
    toggleMaximize: () => ipcRenderer.invoke('window:toggleMaximize'),
    close: () => ipcRenderer.invoke('window:close'),
    isMaximized: () => ipcRenderer.invoke('window:isMaximized')
  },

  // Events (main -> renderer)
  onMemory: listen('memory:update'),
  onEngineEvent: listen('engine:event'),
  onEngineStatus: listen('engine:status'),
  onOptimizeState: listen('optimize:state'),
  onOptimizeResult: listen('optimize:result'),
  onOptimizeError: listen('optimize:error'),
  onWindowState: listen('window:state')
});
