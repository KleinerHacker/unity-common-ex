using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool ContainsType<T>(this IDictionary<Type, T> dictionary, Type type) => dictionary.Keys.Any(type.IsAssignableFrom);

        public static T GetByType<T>(this IDictionary<Type, T> dictionary, Type type) => 
            dictionary.First(x => type.IsAssignableFrom(x.Key)).Value;
        
        public static IDictionary<Type, T> GetAllByType<T>(this IDictionary<Type, T> dictionary, Type type) => 
            dictionary.Where(x => type.IsAssignableFrom(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
    }
}