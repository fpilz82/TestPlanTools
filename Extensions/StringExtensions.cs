using System.Security;
using Newtonsoft.Json.Linq;

namespace TPTools.Extensions
{
    public static class StringExtensions
    {
        public static JObject ToJson(this string text) =>
            JObject.Parse(text);

        public static SecureString ToSecureString(this string text)
        {
            var secureString = new SecureString();
            foreach (var c in text)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }
    }
}