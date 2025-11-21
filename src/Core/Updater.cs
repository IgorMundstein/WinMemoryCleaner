using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Updater
    /// </summary>
    public static class Updater
    {
        private static WebClient _client;
        private static DateTimeOffset _lastCheck = DateTimeOffset.MinValue;

        internal static ProcessStartInfo Process;

        private static void OnFileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw new Exception("File download failed.", e.Error);

                if (e.Cancelled)
                    return;

                var updateInfo = (Tuple<string, string, string, Version, string[]>)e.UserState;
                var temp = updateInfo.Item1;
                var path = updateInfo.Item2;
                var exe = updateInfo.Item3;
                var newestVersion = updateInfo.Item4;
                var args = updateInfo.Item5;

                if (File.Exists(temp) && AssemblyName.GetAssemblyName(temp).Version.Equals(newestVersion))
                {
                    Process = new ProcessStartInfo
                    {
                        Arguments = string.Format(CultureInfo.InvariantCulture, @"/c taskkill /f /im ""{0}"" & move /y ""{1}"" ""{2}"" & start """" ""{2}"" /{3} {4}", exe, temp, path, newestVersion, string.Join(" ", args)),
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
                Logger.Error(ex);

                Reset();
            }
        }

        private static void OnVersionCheckCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw new Exception("Version check failed.", e.Error);

                if (e.Cancelled)
                    return;

                var assemblyInfo = e.Result;
                var assemblyVersionMatch = Regex.Match(assemblyInfo, @"AssemblyVersion\(""(.*)""\)\]");

                if (!assemblyVersionMatch.Success)
                    return;

                var newestVersion = Version.Parse(assemblyVersionMatch.Groups[1].Value);

                if (App.Version >= newestVersion)
                {
                    Reset();
                    return;
                }

                var exe = Path.GetFileName(App.Path);
                var temp = Path.Combine(Path.GetTempPath(), exe);

                if (File.Exists(temp))
                    File.Delete(temp);

                var updateInfo = Tuple.Create(temp, App.Path, exe, newestVersion, (string[])e.UserState);

                _client.DownloadFileAsync(Constants.App.Repository.LatestExeUri, temp, updateInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                Reset();
            }
        }

        private static void Reset()
        {
            try
            {
                if (_client != null)
                    _client.Dispose();
            }
            finally
            {
                _client = null;
            }

            try
            {
                if (Process != null)
                    Process = null;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Check for new version and update if available
        /// </summary>
        public static void Update(params string[] args)
        {
            try
            {
                if (Settings.AutoUpdate && DateTimeOffset.Now.Subtract(_lastCheck).TotalHours < Constants.App.AutoUpdateInterval)
                    return;

                _lastCheck = DateTimeOffset.Now;

                Reset();

                _client = new WebClient();
                _client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnFileDownloadCompleted);
                _client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnVersionCheckCompleted);

                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072 | (SecurityProtocolType)12288; // TLS 1.2 | TLS 1.3

                _client.DownloadStringAsync(Constants.App.Repository.AssemblyInfoUri, args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                Reset();
            }
        }
    }
}