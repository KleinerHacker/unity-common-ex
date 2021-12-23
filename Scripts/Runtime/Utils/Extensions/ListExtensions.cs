using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Creates a weight list from a normal list. See <see cref="GetRandomByWeight{T}"/>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="weightExtractor">Function is called for each element to get weight value</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of <see cref="Weighting{T}"/>s</returns>
        public static IEnumerable<Weighting<T>> ToWeightList<T>(this IEnumerable<T> list, Func<T, float> weightExtractor)
        {
            return list.Select(x => new Weighting<T>(x, weightExtractor(x)));
        }

        /// <summary>
        /// Returns a random value from list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludes">Values to exclude from list</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A random value or <c>default</c></returns>
        public static T GetRandom<T>(this IEnumerable<T> list, params T[] excludes)
        {
            var filteredList = list.Where(x => !excludes.Contains(x)).ToList();
            if (filteredList.Count <= 0)
                return default;
            
            return filteredList.ElementAt(Random.Range(0, filteredList.Count()));
        }

        /// <summary>
        /// Returns a random value from list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludePredicate">Values to exclude from list by predicate callback</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A random value or <c>default</c></returns>
        public static T GetRandom<T>(this IEnumerable<T> list, Predicate<T> excludePredicate) => 
            GetRandom(list, list.Where(x => excludePredicate(x)).ToArray());

        /// <summary>
        /// Returns a random value from weight list. This note the weight values.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludes">Values to exclude from list</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A random value or <c>default</c></returns>
        public static T GetRandomByWeight<T>(this IEnumerable<Weighting<T>> list, params T[] excludes)
        {
            var filteredList = list.Where(x => !excludes.Contains(x.Value)).ToList();
            if (filteredList.Count <= 0)
                return default;
            
            var weightSum = filteredList.Sum(x => x.Weight);

            var randomWeight = Random.Range(0, weightSum);
            var weightCounter = 0f;
            foreach (var item in filteredList)
            {
                weightCounter += item.Weight;
                if (weightCounter >= randomWeight)
                    return item.Value;
            }

            return filteredList.Last().Value;
        }

        /// <summary>
        /// Returns a random value from weight list. This note the weight values.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="excludePredicate">Values to exclude from list by predicate callback</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A random value or <c>default</c></returns>
        public static T GetRandomByWeight<T>(this IEnumerable<Weighting<T>> list, Predicate<T> excludePredicate) => 
            GetRandomByWeight(list, list.Select(x => x.Value).Where(x => excludePredicate(x)).ToArray());

        /// <summary>
        /// Calculates the sum of all weights in a <see cref="Weighting{T}"/> list
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static float SumWeight<T>(this IEnumerable<Weighting<T>> list) => list.Sum(x => x.Weight);

        /// <summary>
        /// Calculate a relative weight value (between 0 and 1) for given <see cref="Weighting{T}"/> value. 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"><see cref="Weighting{T}"/> value to calculate relative weight. <b>Should be part of list!</b></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static float GetRelativeWeight<T>(this IEnumerable<Weighting<T>> list, Weighting<T> element)
        {
            var weight = list.SumWeight();
            return element.Weight / weight;
        }

        /// <summary>
        /// Calculate a relative weight value (between 0 and 1) for given <see cref="Weighting{T}"/> value on index.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index">Index of <see cref="Weighting{T}"/> value in list to calculate relative weight</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static float GetRelativeWeight<T>(this IEnumerable<Weighting<T>> list, int index)
        {
            var weighting = list.ElementAt(index);
            return list.GetRelativeWeight(weighting);
        }

        public static IEnumerable<T> GetRandomList<T>(this IEnumerable<T> list, float percentage, params T[] excludes)
        {
            var filteredList = list.Where(x => !excludes.Contains(x)).ToList();
            if (filteredList.Count <= 0)
                return filteredList;
            
            var tmpList = new List<T>();
            var count = (int) (filteredList.Count * Mathf.Clamp01(percentage));

            if (count == filteredList.Count)
                return filteredList;

            for (var i = 0; i < count; i++)
            {
                T randomItem;
                do
                {
                    randomItem = filteredList.GetRandom();
                } while (tmpList.Contains(randomItem));

                tmpList.Add(randomItem);
            }

            return tmpList;
        }

        /// <summary>
        /// Remove the given item from list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item">Item to remove</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>New list without given element</returns>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> list, T item)
        {
            return list.Where(x => !Equals(x, item));
        }

        /// <summary>
        /// Remove all items from list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="items">Items to remove from list</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>List without items</returns>
        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> list, params T[] items) => list.Where(x => !items.Contains(x));

        /// <summary>
        /// Returns the index of given predicate. This is the first index that was found.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate">Predicate to check value</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The index of successful predicate or -1 if no element matches</returns>
        public static int IndexOf<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            for (var i = 0; i < list.Count(); i++)
            {
                if (predicate(list.ElementAt(i)))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the index of given predicate. This is the last index that was found.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate">Predicate to check value</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The index of successful predicate or -1 if no element matches</returns>
        public static int LastIndexOf<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            for (var i = list.Count() - 1; i >= 0; i--)
            {
                if (predicate(list.ElementAt(i)))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Search for the first element that matches the predicate. If no element was found it throws given exception.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate">Predicate to check</param>
        /// <param name="exception">Exception function to create exception is to throw.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first found element</returns>
        /// <exception cref="Exception">Is thrown if no element was found</exception>
        public static T FirstOrThrow<T>(this IEnumerable<T> list, Func<T, bool> predicate, Func<Exception> exception)
        {
            var firstOrDefault = list.FirstOrDefault(predicate);
            if (Equals(firstOrDefault, default(T)))
                throw exception();

            return firstOrDefault;
        }
    }

    /// <summary>
    /// Represent the weighting for weighting lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Weighting<T>
    {
        /// <summary>
        /// Original value
        /// </summary>
        public T Value { get; }
        /// <summary>
        /// Weight for this value
        /// </summary>
        public float Weight { get; }

        public Weighting(T value, float weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}