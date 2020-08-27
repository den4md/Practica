using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polls
{
    class Parser
    {
        public static T FieldParse<T>(string JSON, string field)
        {
            JObject responseObject = JObject.Parse(JSON);
            if (responseObject[field] == null)
                return default(T);
            return responseObject[field].ToObject<T>();
        }

        public static string SessionParse(string JSON)
        {
            return FieldParse<string>(JSON, "sessionID");
        }

        public static bool ResultParse(string JSON)
        {
            return FieldParse<bool>(JSON, "result");
        }

        public static bool AuthParse(string JSON)
        {
            return FieldParse<bool>(JSON, "isAuth");
        }
    }
}
