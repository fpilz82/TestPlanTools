using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestPlanTools.Extensions
{
    public static class StringExtensions
    {
        public static JObject ToJson(this string text) =>
            JObject.Parse(text);

        public static SecureString ToSecureString(this string @text)
        {
            var secureString = new SecureString();
            foreach (var c in @text)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        public static string ToUnsecureString(this SecureString secureString) =>
            Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(secureString));

        public static string ToJson<T>(this T convert) =>
            JsonConvert.SerializeObject(convert, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        public static string AddRandomString(this string text, int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var random = new Random();
            return text + new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
    }
}