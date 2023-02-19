using System;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static T[] ToSingleArray<T>(this T obj) =>
            obj == null ? Array.Empty<T>() : new[] { obj };

        public static T[] ToSingleArray<T>(this T? obj) where T : struct =>
            obj == null ? Array.Empty<T>() : new[] { obj.Value };
    }
}