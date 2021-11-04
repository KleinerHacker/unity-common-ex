using System.Collections.Generic;
using UnityEngine;

namespace UnityCommons.Runtime.commons.Scripts.Runtime.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static void DestroyAll(this IEnumerable<Object> list)
        {
            foreach (var o in list)
            {
                Object.Destroy(o);
            }
        }
    }
}