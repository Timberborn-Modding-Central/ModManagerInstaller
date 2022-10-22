using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Timbermod_installer.LogSystem;
using VdfParser;

namespace Timbermod_installer.GamePathFinder.Platforms
{
    public class Steam : IGamePlatform
    {
        public string Name => "Steam";

        public bool TryGetGamePath(out string gamePath)
        {
            gamePath = "";

            string steamPath = GetSteamPath();

            if (string.IsNullOrEmpty(steamPath))
            {
                SendSteamLog("Steam not installed.");
                return false;
            }

            gamePath = GetGamePathFromSteamInstallations(steamPath);

            if (string.IsNullOrEmpty(gamePath))
            {
                SendSteamLog("Game is not installed on steam.");
                return false;
            }

            return true;
        }

        private static string GetGamePathFromSteamInstallations(string steamPath)
        {
            string libraryFoldersPath = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
            if (!File.Exists(libraryFoldersPath))
            {
                return "";
            }

            FileStream libraryFoldersVdf = File.OpenRead(libraryFoldersPath);

            Library libraryFolders;
            try
            {
                var deserializer = new VdfDeserializer();

                libraryFolders = deserializer.Deserialize<Library>(libraryFoldersVdf);
            }
            catch (Exception e)
            {
                SendSteamLog("Failed to parse libraryfolders.vdf");
                SendSteamLog(e.ToString());
                return "";
            }

            var gamePath = "";

            foreach (KeyValuePair<int, LibraryFolder> libraryFolder in libraryFolders.Libraryfolders)
            {
                if (TryGetGamePathInSteamInstallation(Path.GetFullPath(libraryFolder.Value.Path), out gamePath))
                {
                    break;
                }
            }

            return gamePath;
        }

        private static bool TryGetGamePathInSteamInstallation(string installationPath, out string gamePath)
        {
            gamePath = "";

            string appsPath = Path.Combine(installationPath, "steamapps");
            if (!File.Exists(Path.Combine(appsPath, $"appmanifest_{GameInfo.SteamAppId}.acf")))
            {
                return false;
            }

            gamePath = Path.Combine(appsPath, "common", GameInfo.Name);
            return true;
        }

        private static string GetSteamPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return string.IsNullOrWhiteSpace(homePath) ? Path.Combine(homePath, ".local", "share", "Steam") : "";
            }

            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\\Valve\\Steam");
            object value = key?.GetValue("SteamPath");
            return value != null ? value.ToString() : "";
        }

        private static void SendSteamLog(string message)
        {
            LogService.Send("[GameFinder: Steam] " + message);
        }
    }
}