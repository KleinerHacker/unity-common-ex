using System;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string Limit(this string s, int length, string capsule = null)
        {
            if (string.IsNullOrEmpty(capsule))
                return s.Substring(0, Math.Min(s.Length, length));

            if (s.Length > length - capsule.Length)
                return s.Substring(0, length - capsule.Length) + capsule;

            return s;
        }
    }
}