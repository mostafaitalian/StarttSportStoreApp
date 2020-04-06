using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace StartSportStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);

        }
    }
    public static class SessionExtensionss
    {
        public static void SettJson(this ISession session, string key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }
        public static string Getjsonn(this ISession session, string key)
        {
            string data = session.GetString(key);
            object b = JsonConvert.DeserializeObject<string>(data);
            return data;
        }
        public static T GettJson<T>(this ISession session, string key)
        {
            return session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}
