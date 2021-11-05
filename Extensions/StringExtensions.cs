using System.Linq;
using System.Text.RegularExpressions;

namespace ByteBank.Extensions
{
    public static class StringExtensions
    {
        public static string[] ToStringArray(this string value)
        {
            return new string[] { value };
        }
        public static string ArrayToString(this string[] value)
        {
            return value.First();
        }
        public static string ApenasLetras(this string value)
        {
            return string.Join(" ", Regex.Matches(value, @"[a-zà-úA-ZÀ-Ú]+"));
        }

    }

}
