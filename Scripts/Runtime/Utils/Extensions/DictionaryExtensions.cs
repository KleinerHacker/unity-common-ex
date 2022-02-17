using System.Collections.Generic;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetOrDefault<TK, T>(this IDictionary<TK, T> dictionary, TK key, T defValue, bool autoAdd = false)
        {
            var containsKey = dictionary.ContainsKey(key);
            
            var result = containsKey ? dictionary[key] : defValue;
            if (autoAdd && !containsKey)
            {
                dictionary.Add(key, result);
            }

            return result;
        }

        public static void AddOrOverwrite<TK, T>(this IDictionary<TK, T> dictionary, TK key, T value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] = value;
            }
        }
    }
}