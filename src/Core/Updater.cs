using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WinMemoryCleaner
{
    public static class Updater
    {
        private static readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromSeconds(30) };
        private static DateTimeOffset _lastCheck = DateTimeOffset.MinValue;
        private static ProcessStartInfo _process;

        public static ProcessStartInfo Process => _process;

        public static async void Update(params string[] args)
        {
            try
            {
                if (!Settings.AutoUpdate || DateTimeOffset.Now.Subtract(_lastCheck).TotalHours < Constants.App.AutoUpdateInterval)
                    return;

                _lastCheck = DateTimeOffset.Now;
                _process = null;

                var currentVersion = App.Version;

                // Check for updates via GitHub API
                var response = await _httpClient.GetStringAsync(Constants.App.Repository.ApiLatestReleaseUri);
                using var doc = JsonDocument.Parse(response);
                var tagName = doc.RootElement.GetProperty("tag_name").GetString()?.TrimStart('v');

                if (string.IsNullOrEmpty(tagName) || !Version.TryParse(tagName, out var newestVersion))
                    return;

                if (currentVersion >= newestVersion)
                    return;

                // Download the new version
                var exe = Path.GetFileName(App.Path);
                var temp = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.exe");

                Helper.DeleteFile(temp);

                var fileBytes = await _httpClient.GetByteArrayAsync(Constants.App.Repository.LatestExeUri);
                await File.WriteAllBytesAsync(temp, fileBytes);

                // Verify the downloaded file
                if (File.Exists(temp))
                {
                    try
                    {
                        var downloadedVersion = AssemblyName.GetAssemblyName(temp).Version;
                        if (downloadedVersion != null && downloadedVersion.Equals(newestVersion))
                        {
                            _process = new ProcessStartInfo
                            {
                                Arguments = string.Format(CultureInfo.InvariantCulture, @"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, temp, App.Path, newestVersion, string.Join(" ", args)),
                                CreateNoWindow = true,
                                FileName = "cmd",
                                RedirectStandardError = false,
                                RedirectStandardInput = false,
                                RedirectStandardOutput = false,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Hidden
                            };

                            App.Shutdown();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Failed to verify downloaded update: " + ex.GetMessage());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static async Task<Version> CheckForUpdatesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(Constants.App.Repository.ApiLatestReleaseUri);
                using var doc = JsonDocument.Parse(response);
                var tagName = doc.RootElement.GetProperty("tag_name").GetString()?.TrimStart('v');

                if (!string.IsNullOrEmpty(tagName) && Version.TryParse(tagName, out var version))
                    return version;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public static void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}