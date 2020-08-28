using DeviceId;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls
{
    class Settings
    {
        public static string Ip { get {return $"http://{Memory.ip}"; } }
        public static string Port { get {return Memory.port; } }

        public static string GetUrl()
        { return Ip + ":" + Port; }

        public static string GetApiUrl()
        { return GetUrl() + "/api"; }

        public static string SessionUrl()
        { return GetApiUrl() + "/getsessionid"; }

        public static string RegistrationUrl()
        { return GetApiUrl() + "/registration"; }

        public static string LoginUrl()
        { return GetApiUrl() + "/login"; }

        public static string LogoutUrl()
        { return GetApiUrl() + "/logout"; }

        public static string ProfileUrl()
        { return GetApiUrl() + "/profile"; }

        public static string ProfileEditUrl()
        { return GetApiUrl() + "/profile/edit"; }

        public static string TestEdit()
        { return GetApiUrl() + "/test/edit"; }

        public static string TestPublish()
        { return GetApiUrl() + "/test/publish"; }

        public static string TestSetActivity()
        { return GetApiUrl() + "/test/setactivity"; }

        public static string TestDelete()
        { return GetApiUrl() + "/profile/createdtests"; }

        public static string TestFavorites()
        { return GetApiUrl() + "/tests/favorites"; }

        public static string CreatedTests()
        { return GetApiUrl() + "/profile/createdtests"; }

        public static string PassedTests()
        { return GetApiUrl() + "/profile/passedtests"; }

        public static string FavoriteTests()
        { return GetApiUrl() + "/profile/favoritetests"; }

        public static string PublicTests()
        { return GetApiUrl() + "/tests/publictests"; }

        public static string SearchTests()
        { return GetApiUrl() + "/tests/search"; }

        public static string PassingTest()
        { return GetApiUrl() + "/test/passing"; }

        public static string PassingNextSlide()
        { return GetApiUrl() + "/test/passing/nextslide"; }

        public static string PassingPrevSlide()
        { return GetApiUrl() + "/test/passing/prevslide"; }

        public static string PassingFinish()
        { return GetApiUrl() + "/test/passing/finish"; }

        public static string RateTest()
        { return GetApiUrl() + "/tests/rate"; }

        private static string PCKey { get; } = 
            MD5Handler.GetMd5Hash(new DeviceIdBuilder()
                .AddMachineName()
                .AddMacAddress()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .ToString());

        public static string SettingsFileName { get { return string.Concat(AppDataDirectory,
                                                                "/settings.txt"); } }

        public static string ReadSettingsCrypt()
        {
            return Crypt.DecryptString(PCKey, File.ReadAllText(SettingsFileName));
        }

        public static string ReadSettings()
        {
            return File.ReadAllText(SettingsFileName);
        }

        private static string getStringSettings()
        {
            var settings = new Dictionary<string, object> { };
            settings["sessionID"] = Memory.session;
            settings["isAuth"] = Memory.isAuth;
            settings["ip"] = Memory.ip;
            settings["port"] = Memory.port;

            return JsonConvert.SerializeObject(settings);
        }

        private static void saveStringSettings(string text)
        {
            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
            }
            File.WriteAllText(SettingsFileName, text);
        }

        public static void SaveSettings()
        {
            saveStringSettings(getStringSettings());
        }

        public static void SaveSettingsCrypt()
        {
            saveStringSettings(Crypt.EncryptString(PCKey, getStringSettings()));
        }

        public static string AppDataDirectory
        {
            get
            {
                return string.Concat(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "/Polls");
            }
        }
    }
}
