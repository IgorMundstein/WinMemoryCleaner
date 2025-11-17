using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Self-update logic for a single-EXE deployment with mandatory checksum validation
    /// </summary>
    public static class Updater
    {
        #region Fields (Alphabetical)
        private static DateTimeOffset _lastAutoUpdate = DateTimeOffset.MinValue;
        #endregion

        #region Methods (Alphabetical)
        private static void ApplyUpdateAndRelaunch(string targetPath, string backupPath, string relaunchArgs)
        {
            var tries = 0;
            var replaced = false;

            while (tries < Constants.App.Update.ReplaceMaxTries)
            {
                tries++;

                var stagedPath = Helper.GetExecutablePath();
                var targetDir = Path.GetDirectoryName(targetPath) ?? string.Empty;
                var tempInTarget = Path.Combine(targetDir, Path.GetFileName(targetPath) + ".new");

                if (IsSameVolume(targetPath, backupPath))
                {
                    try
                    {
                        try
                        {
                            if (File.Exists(tempInTarget))
                                File.Delete(tempInTarget);
                        }
                        catch
                        {
                        }

                        File.Copy(stagedPath, tempInTarget, true);

                        Helper.CreateDirectory(Path.GetDirectoryName(backupPath));
                        File.Replace(tempInTarget, targetPath, backupPath, true);

                        try
                        {
                            if (File.Exists(tempInTarget))
                                File.Delete(tempInTarget);
                        }
                        catch
                        {
                        }

                        replaced = true;
                    }
                    catch
                    {
                        try
                        {
                            if (File.Exists(tempInTarget))
                                File.Delete(tempInTarget);
                        }
                        catch
                        {
                        }
                    }
                }

                if (replaced)
                    break;

                try
                {
                    try
                    {
                        if (File.Exists(targetPath))
                        {
                            Helper.CreateDirectory(Path.GetDirectoryName(backupPath));

                            try
                            {
                                if (File.Exists(backupPath))
                                    File.Delete(backupPath);
                            }
                            catch
                            {
                            }

                            File.Move(targetPath, backupPath);
                        }
                    }
                    catch
                    {
                    }

                    File.Copy(stagedPath, targetPath, true);

                    replaced = File.Exists(targetPath) && new FileInfo(targetPath).Length > 0;

                    if (replaced)
                        break;
                }
                catch
                {
                }

                Thread.Sleep(Constants.App.Update.ReplaceRetryDelayMs);
            }

            if (!replaced)
            {
                try
                {
                    if (File.Exists(backupPath))
                    {
                        try
                        {
                            if (File.Exists(targetPath))
                                File.Delete(targetPath);
                        }
                        catch
                        {
                        }

                        File.Move(backupPath, targetPath);
                    }
                }
                catch (Exception e)
                {
                    Log("UPD-APPLY-ROLLBACK", e);
                }

                return;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = targetPath,
                    Arguments = relaunchArgs ?? string.Empty,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception e)
            {
                Log("UPD-RELAUNCH", e);
            }
        }

        private static string BuildArgumentString(List<string> args)
        {
            if (args == null || args.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                if (NeedsQuoting(arg))
                    sb.Append('\"').Append(arg.Replace("\"", "\\\"")).Append('\"');
                else
                    sb.Append(arg);

                sb.Append(' ');
            }

            return sb.ToString().Trim();
        }

        private static string BuildBackupPath(string targetPath)
        {
            return Path.Combine(GetBackupsDirectory(), Path.GetFileName(targetPath) + ".bak");
        }

        private static Uri BuildChecksumsTxtUri()
        {
            try
            {
                var baseUrl = Constants.App.Repository.LatestExeUri.AbsoluteUri;
                var idx = baseUrl.LastIndexOf('/');

                if (idx <= 0)
                    return null;

                var url = baseUrl.Substring(0, idx + 1) + Constants.App.Update.ChecksumsFileName;

                return new Uri(url);
            }
            catch
            {
                return null;
            }
        }

        private static string BuildExceptionDetails(Exception e, string code)
        {
            try
            {
                var sb = new StringBuilder(512);

                sb.Append("[Code=").Append(code ?? "N/A");
                sb.Append("; Type=").Append(e != null ? e.GetType().FullName : "null");
                sb.Append("; Message=").Append(e != null ? e.GetMessage() : "null");

                try
                {
                    if (e != null && !string.IsNullOrEmpty(e.Source))
                        sb.Append("; Source=").Append(e.Source);
                }
                catch
                {
                }

                try
                {
                    if (e != null && e.TargetSite != null)
                        sb.Append("; Target=").Append(e.TargetSite.Name);
                }
                catch
                {
                }

                try
                {
                    var st = e != null ? e.StackTrace : null;

                    if (!string.IsNullOrEmpty(st))
                    {
                        var firstNl = st.IndexOf('\n');
                        var first = firstNl > 0 ? st.Substring(0, firstNl).Trim() : st.Trim();

                        sb.Append("; Stack=").Append(first);
                    }
                }
                catch
                {
                }

                try
                {
                    if (e != null && e.InnerException != null)
                        sb.Append("; InnerType=").Append(e.InnerException.GetType().FullName);
                }
                catch
                {
                }

                sb.Append(']');

                return sb.ToString();
            }
            catch
            {
                return "[Code=" + (code ?? "N/A") + "]";
            }
        }

        private static string BuildTokenFilePath(string token)
        {
            return Path.Combine(GetTempRootDirectory(), Constants.App.Update.TokenFilePrefix + token + Constants.App.Update.TokenFileSuffix);
        }

        private static bool CanWriteToDirectory(string directory)
        {
            try
            {
                if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
                    return false;

                var probe = Path.Combine(directory, ".__update_write_test_" + Guid.NewGuid().ToString("N") + ".tmp");

                try
                {
                    File.WriteAllText(probe, "x");

                    return File.Exists(probe);
                }
                finally
                {
                    try
                    {
                        if (File.Exists(probe))
                            File.Delete(probe);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static void CleanupStaleBackups()
        {
            try
            {
                var backupsDir = GetBackupsDirectory();

                if (!Directory.Exists(backupsDir))
                    return;

                var retention = TimeSpan.FromDays(Constants.App.Update.BackupRetentionDays);
                var now = DateTime.UtcNow;

                string[] files = null;

                try
                {
                    files = Directory.GetFiles(backupsDir, "*.bak");
                }
                catch
                {
                    files = null;
                }

                if (files == null || files.Length == 0)
                    return;

                for (var i = 0; i < files.Length; i++)
                {
                    var f = files[i];

                    try
                    {
                        var age = now - File.GetLastWriteTimeUtc(f);

                        try
                        {
                            if (age >= retention)
                                File.Delete(f);
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private static string ComputeSha256Upper(string filePath)
        {
            try
            {
                using (var sha = CreateSha256())
                {
                    if (sha == null)
                        return null;

                    using (var s = File.OpenRead(filePath))
                    {
                        var hash = sha.ComputeHash(s);
                        var sb = new StringBuilder(hash.Length * 2);

                        for (var i = 0; i < hash.Length; i++)
                            sb.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));

                        return sb.ToString();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static SHA256 CreateSha256()
        {
            try
            {
                return new SHA256CryptoServiceProvider();
            }
            catch
            {
                try
                {
                    return SHA256.Create();
                }
                catch
                {
                    return null;
                }
            }
        }

        private static string DownloadStringSafe(Uri uri, int timeoutMs)
        {
            try
            {
                using (var client = new TimeoutWebClient(timeoutMs))
                {
                    client.Headers[HttpRequestHeader.UserAgent] = Constants.App.Update.UserAgent;
                    client.Encoding = Encoding.UTF8;

                    return client.DownloadString(uri);
                }
            }
            catch
            {
                return null;
            }
        }

        private static string FindSha256ForFile(string checksumsText, string assetFileName)
        {
            if (string.IsNullOrEmpty(checksumsText) || string.IsNullOrEmpty(assetFileName))
                return null;

            var target = assetFileName.Trim();
            var lines = checksumsText.Replace("\r\n", "\n").Replace('\r', '\n').Split(new[] { '\n' }, StringSplitOptions.None);

            for (var i = 0; i < lines.Length; i++)
            {
                var line = (lines[i] ?? string.Empty).Trim();

                if (line.Length == 0)
                    continue;

                if (line.StartsWith("#", StringComparison.Ordinal) || line.StartsWith(";", StringComparison.Ordinal))
                    continue;

                var m = Regex.Match(line, @"^\s*([A-Fa-f0-9]{64})\s+[* ]?(.+?)\s*$");

                if (m.Success)
                {
                    var hash = m.Groups[1].Value;
                    var name = Path.GetFileName(m.Groups[2].Value.Trim().Trim('"'));

                    if (StringComparer.Ordinal.Equals(name, target))
                        return hash.ToUpperInvariant();

                    continue;
                }

                m = Regex.Match(line, @"^\s*SHA\s*256\s*\((.+?)\)\s*=\s*([A-Fa-f0-9]{64})\s*$", RegexOptions.IgnoreCase);

                if (m.Success)
                {
                    var name = Path.GetFileName(m.Groups[1].Value.Trim().Trim('"'));
                    var hash = m.Groups[2].Value;

                    if (StringComparer.Ordinal.Equals(name, target))
                        return hash.ToUpperInvariant();

                    continue;
                }

                m = Regex.Match(line, @"^\s*(.+?)\s*[:=]\s*([A-Fa-f0-9]{64})\s*$");

                if (m.Success)
                {
                    var name = Path.GetFileName(m.Groups[1].Value.Trim().Trim('"'));
                    var hash = m.Groups[2].Value;

                    if (StringComparer.Ordinal.Equals(name, target))
                        return hash.ToUpperInvariant();

                    continue;
                }
            }

            return null;
        }

        private static string GetAssetFileNameFromUri(Uri assetUri)
        {
            try
            {
                var path = assetUri != null ? assetUri.AbsolutePath : null;

                if (string.IsNullOrEmpty(path))
                    return null;

                var idx = path.LastIndexOf('/');

                return idx >= 0 ? path.Substring(idx + 1) : path;
            }
            catch
            {
                return null;
            }
        }

        private static string GetBackupsDirectory()
        {
            var dir = Path.Combine(GetTempRootDirectory(), Constants.App.Update.BackupDirName);

            Helper.CreateDirectory(dir);

            return dir;
        }

        private static string GetTempRootDirectory()
        {
            var root = Path.Combine(Path.GetTempPath(), Constants.App.Update.TempRootDirName);

            Helper.CreateDirectory(root);

            return root;
        }

        private static void HardenNetworking()
        {
            try
            {
                ServicePointManager.DefaultConnectionLimit = Constants.App.Update.MaxConnectionLimit;
                ServicePointManager.Expect100Continue = true;

                try
                {
                    ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072; // TLS 1.2
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        private static bool IsSameVolume(string a, string b)
        {
            try
            {
                return string.Equals(Path.GetPathRoot(a) ?? string.Empty, Path.GetPathRoot(b) ?? string.Empty, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static void Log(string code, Exception e)
        {
            try
            {
                Logger.Warning(string.Format(Localizer.Culture, "({0}) Update failed. Please try again later. {1}", Localizer.String.AutoUpdate.ToUpper(Localizer.Culture), BuildExceptionDetails(e, code)));
            }
            catch
            {
            }
        }

        private static bool NeedsQuoting(string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (char.IsWhiteSpace(c) || c == '&' || c == '|' || c == '<' || c == '>' || c == '^')
                    return true;
            }

            return false;
        }

        private static void SafeDeleteFile(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
            }
        }

        private static bool StringEqualsConstantTime(string a, string b)
        {
            if (a == null || b == null)
                return false;

            if (a.Length != b.Length)
                return false;

            var diff = 0;

            for (var i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];

            return diff == 0;
        }

        private static bool TryApplyUpdateFromEnvironment()
        {
            var token = Environment.GetEnvironmentVariable(Constants.App.Update.TokenEnvVar);

            if (string.IsNullOrEmpty(token))
                return false;

            string backupPath = null;
            string relaunchArgs = null;
            string targetPath = null;
            string tokenFilePath;

            try
            {
                tokenFilePath = BuildTokenFilePath(token);

                if (!File.Exists(tokenFilePath))
                {
                    App.Shutdown(true);
                    return true;
                }

                var lines = File.ReadAllLines(tokenFilePath, Encoding.UTF8);

                for (var i = 0; i < lines.Length; i++)
                {
                    var line = (lines[i] ?? string.Empty).Trim();

                    if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal) || line.StartsWith(";", StringComparison.Ordinal))
                        continue;

                    if (line.StartsWith("TARGET=", StringComparison.Ordinal))
                        targetPath = line.Substring("TARGET=".Length).Trim();
                    else if (line.StartsWith("BACKUP=", StringComparison.Ordinal))
                        backupPath = line.Substring("BACKUP=".Length).Trim();
                    else if (line.StartsWith("ARGS=", StringComparison.Ordinal))
                        relaunchArgs = line.Substring("ARGS=".Length);
                }
            }
            catch (Exception e)
            {
                Log("UPD-READ-TOKEN", e);
                App.Shutdown(true);

                return true;
            }

            try
            {
                SafeDeleteFile(tokenFilePath);
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(backupPath))
            {
                App.Shutdown(true);

                return true;
            }

            try
            {
                if (!string.Equals(Path.GetFileName(Helper.GetExecutablePath()), Path.GetFileName(targetPath), StringComparison.OrdinalIgnoreCase))
                {
                    App.Shutdown(true);

                    return true;
                }
            }
            catch
            {
            }

            ApplyUpdateAndRelaunch(targetPath, backupPath, relaunchArgs ?? string.Empty);

            App.Shutdown(true);

            return true;
        }

        private static bool TryDownloadFile(Uri uri, string destinationPath, int timeoutMs)
        {
            try
            {
                using (var client = new TimeoutWebClient(timeoutMs))
                {
                    client.Headers[HttpRequestHeader.UserAgent] = Constants.App.Update.UserAgent;
                    client.DownloadFile(uri, destinationPath);

                    return File.Exists(destinationPath) && new FileInfo(destinationPath).Length > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool TryGetAssemblyVersion(string filePath, out Version version)
        {
            version = null;

            try
            {
                version = AssemblyName.GetAssemblyName(filePath).Version;

                return version != null;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryGetLatestVersion(out Version version)
        {
            version = null;

            var assemblyInfo = DownloadStringSafe(Constants.App.Repository.AssemblyInfoUri, Constants.App.Update.DownloadTimeout * 1000);

            if (string.IsNullOrEmpty(assemblyInfo))
                return false;

            var result = Regex.Match(assemblyInfo, @"AssemblyVersion\(""(.*)""\)\]");

            if (!result.Success)
                return false;

            Version parsed;

            if (Version.TryParse(result.Groups[1].Value, out parsed))
            {
                version = parsed;

                return true;
            }

            return false;
        }

        private static bool TryVerifyChecksumViaChecksumsTxt(string filePath, string assetFileName, out bool attempted)
        {
            attempted = false;

            var checksumsUri = BuildChecksumsTxtUri();

            if (checksumsUri == null)
                return false;

            string text = null;

            for (var attempt = 0; attempt < Constants.App.Update.ChecksumsMaxRetries; attempt++)
            {
                text = DownloadStringSafe(checksumsUri, Constants.App.Update.DownloadTimeout * 1000);

                if (!string.IsNullOrEmpty(text))
                    break;

                if (attempt + 1 < Constants.App.Update.ChecksumsMaxRetries)
                    Thread.Sleep(Constants.App.Update.ChecksumsRetryDelay * 1000);
            }

            if (string.IsNullOrEmpty(text))
                return false;

            var expected = FindSha256ForFile(text, assetFileName);

            if (string.IsNullOrEmpty(expected))
                return false;

            var actual = ComputeSha256Upper(filePath);

            if (string.IsNullOrEmpty(actual))
            {
                attempted = true;
                return false;
            }

            attempted = true;

            return StringEqualsConstantTime(expected.ToUpperInvariant(), actual.ToUpperInvariant());
        }

        private static bool TryVerifyFileVersionInfo(string filePath)
        {
            try
            {
                var vi = FileVersionInfo.GetVersionInfo(filePath);

                if (vi == null)
                    return true;

                if (!string.IsNullOrEmpty(vi.ProductName))
                    return string.Equals(vi.ProductName, Constants.App.Name, StringComparison.Ordinal);

                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Entry point: checks for a newer version and updates the application if available
        /// </summary>
        /// <param name="commandLineArguments">Optional command line arguments</param>
        public static void Update(List<string> commandLineArguments = null)
        {
            try
            {
                if (!Settings.AutoUpdate)
                    return;

                if (TryApplyUpdateFromEnvironment())
                    return;

                CleanupStaleBackups();

                if (commandLineArguments != null)
                {
                    foreach (var arg in commandLineArguments)
                    {
                        var commandLineArgument = arg != null ? arg.Replace("/", string.Empty) : null;

                        if (string.Equals(commandLineArgument, Constants.App.CommandLineArgument.Install, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(commandLineArgument, Constants.App.CommandLineArgument.Uninstall, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(commandLineArgument, Constants.App.CommandLineArgument.Service, StringComparison.OrdinalIgnoreCase))
                            return;
                    }
                }

                if (!Helper.IsAutoUpdateSupported)
                {
                    Settings.AutoUpdate = false;
                    Settings.Save();

                    return;
                }

                if (DateTimeOffset.Now.Subtract(_lastAutoUpdate).TotalHours < Constants.App.Update.CheckInterval)
                    return;

                _lastAutoUpdate = DateTimeOffset.Now;

                using (var mutex = new Mutex(false, Constants.App.Update.MutexName))
                {
                    var hasLock = false;

                    try
                    {
                        hasLock = mutex.WaitOne(0);
                    }
                    catch
                    {
                        hasLock = true;
                    }

                    if (!hasLock)
                        return;

                    HardenNetworking();

                    Version newestVersion;

                    if (!TryGetLatestVersion(out newestVersion))
                        return;

                    var currentVersion = Helper.GetCurrentVersion();

                    if (currentVersion == null || currentVersion >= newestVersion)
                        return;

                    var exePath = Helper.GetExecutablePath();
                    var exeDir = Path.GetDirectoryName(exePath) ?? string.Empty;

                    if (!CanWriteToDirectory(exeDir))
                        return;

                    var exeName = Path.GetFileName(exePath);

                    var tempExePath = Path.Combine(Path.GetTempPath(), exeName + "." + newestVersion + ".download");
                    var stagedExePath = Path.Combine(Path.GetTempPath(), exeName + "." + newestVersion + ".new");

                    SafeDeleteFile(tempExePath);
                    SafeDeleteFile(stagedExePath);

                    if (!TryDownloadFile(Constants.App.Repository.LatestExeUri, tempExePath, Constants.App.Update.DownloadTimeout * 1000))
                        return;

                    Version downloadedVersion;

                    if (!TryGetAssemblyVersion(tempExePath, out downloadedVersion) || downloadedVersion != newestVersion)
                    {
                        SafeDeleteFile(tempExePath);

                        return;
                    }

                    bool checksumAttempted;
                    var checksumOk = TryVerifyChecksumViaChecksumsTxt(tempExePath, GetAssetFileNameFromUri(Constants.App.Repository.LatestExeUri), out checksumAttempted);

                    if (!checksumAttempted || !checksumOk)
                    {
                        SafeDeleteFile(tempExePath);
                        return;
                    }

                    if (Constants.App.Update.VerifyFileVersionInfo)
                    {
                        if (!TryVerifyFileVersionInfo(tempExePath))
                        {
                            SafeDeleteFile(tempExePath);

                            return;
                        }
                    }

                    try
                    {
                        File.Move(tempExePath, stagedExePath);
                    }
                    catch
                    {
                        try
                        {
                            File.Copy(tempExePath, stagedExePath, true);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            SafeDeleteFile(tempExePath);
                        }
                    }

                    if (!File.Exists(stagedExePath))
                        return;

                    var relaunchArgsList = new List<string> { "/" + newestVersion };

                    if (commandLineArguments != null)
                    {
                        for (var i = 0; i < commandLineArguments.Count; i++)
                        {
                            var arg = commandLineArguments[i];

                            if (!string.IsNullOrEmpty(arg))
                                relaunchArgsList.Add(arg);
                        }
                    }

                    var backupPath = BuildBackupPath(exePath);
                    var relaunchArgs = BuildArgumentString(relaunchArgsList);
                    var token = Guid.NewGuid().ToString("N");
                    var tokenFilePath = BuildTokenFilePath(token);

                    try
                    {
                        var sb = new StringBuilder();

                        sb.Append("TARGET=").AppendLine(exePath);
                        sb.Append("BACKUP=").AppendLine(backupPath);
                        sb.Append("ARGS=").AppendLine(relaunchArgs);

                        File.WriteAllText(tokenFilePath, sb.ToString(), Encoding.UTF8);
                    }
                    catch (Exception e)
                    {
                        Log("UPD-WRITE-TOKEN", e);
                        SafeDeleteFile(stagedExePath);
                        SafeDeleteFile(tokenFilePath);

                        return;
                    }

                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = stagedExePath,
                            Arguments = string.Empty,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = false
                        };

                        psi.EnvironmentVariables[Constants.App.Update.TokenEnvVar] = token;

                        Process.Start(psi);
                    }
                    catch (Exception e)
                    {
                        Log("UPD-LAUNCH-STAGED", e);
                        SafeDeleteFile(stagedExePath);
                        SafeDeleteFile(tokenFilePath);

                        return;
                    }

                    App.Shutdown(true);
                }
            }
            catch (Exception e)
            {
                Log("UPD-UNHANDLED", e);
            }
        }
        #endregion

        #region Nested Types (Alphabetical)
        /// <summary>
        /// WebClient with per-request timeout and gzip/deflate for downloads
        /// </summary>
        private sealed class TimeoutWebClient : WebClient
        {
            private readonly int _timeoutMs;

            /// <summary>
            /// Creates a client with a specific timeout (milliseconds)
            /// </summary>
            public TimeoutWebClient(int timeoutMs)
            {
                _timeoutMs = timeoutMs;
            }

            /// <summary>
            /// Applies timeout, UA, proxy, and decompression to HttpWebRequest
            /// </summary>
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);

                var http = request as HttpWebRequest;

                if (http != null)
                {
                    http.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    http.KeepAlive = true;
                    http.Proxy = WebRequest.DefaultWebProxy;
                    http.ReadWriteTimeout = _timeoutMs;
                    http.Timeout = _timeoutMs;
                    http.UserAgent = Constants.App.Update.UserAgent;
                }

                return request;
            }
        }
        #endregion
    }
}