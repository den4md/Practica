using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Polls
{
    class Memory
    {
        private static Memory memory = new Memory();

        public static string session { get; private set; }
        public static bool isAuth { get; set; }
        public static string ip { get; private set; }
        public static string port { get; private set; }

        private Memory()
        {
            session = "";
            ip = "89.28.116.199";
            port = "18000";
        }

        public static void obtainData()
        {
            if (File.Exists(Settings.SettingsFileName))
            {
                string file = Settings.ReadSettingsCrypt();
                session = Parser.SessionParse(file);
                isAuth = Parser.AuthParse(file);
                ip = Parser.FieldParse<string>(file, "ip");
                port = Parser.FieldParse<string>(file, "port");
            }

            Settings.SaveSettingsCrypt();

            if (session.Equals(""))
            {
                session = requestSession();
                isAuth = false;
                Settings.SaveSettingsCrypt();
            }

            ApiRequests.SetSessionCookie(Settings.Ip, session);
        }

        public static string requestSession()
        {
            string responseBody = ApiRequests.SessionGet();
            return Parser.SessionParse(responseBody);
        }

        public static Memory getInstance()
        { return memory; }
    }
}
