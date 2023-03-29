using UnityEngine;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils
{
    public static class MathfEx
    {
        public const float Radiant = 2f * Mathf.PI / 360f;

        public static float Remap(float value, float from1, float to1, float from2, float to2) =>
            Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, from2, to2);

        public static float Remap01(float value, float from, float to) => Remap(value, from, to, 0f, 1f);

        public static Vector2 GetPointInCircleArea(Vector2 center, float radius)
        {
            var arc = Random.Range(0f, 2f * Mathf.PI);
            var posRad = Random.Range(0f, radius);

            var x = Mathf.Sin(arc) * posRad;
            var z = Mathf.Cos(arc) * posRad;

            return center + new Vector2(x, z);
        }

        public static Vector2 GetPointInRectangleArea(Bounds bounds) =>
            GetPointInRectangleArea(bounds.center, bounds.size);

        public static Vector2 GetPointInRectangleArea(Vector2 center, Vector2 size) =>
            new Vector2(
                center.x + Random.Range(-size.x / 2f, size.x / 2f),
                center.y + Random.Range(-size.y / 2f, size.y / 2f)
            );

        public static uint Fibonacci(uint n)
        {
            if (n <= 1)
            {
                return n;
            }

            uint prev = 0;
            uint current = 1;

            for (uint i = 2; i <= n; i++)
            {
                var next = prev + current;
                prev = current;
                current = next;
            }

            return current;
        }
    }

    public struct Box
    {
        #region Static Area

        public static Box FromPoints(Vector3 point1, Vector3 point2)
        {
            var size = new Vector3(
                CalculateFullDif(point1.x, point2.x),
                CalculateFullDif(point1.y, point2.y),
                CalculateFullDif(point1.z, point2.z)
            );
            var relativeCenter = new Vector3(
                CalculateHalfDif(point1.x, point2.x),
                CalculateHalfDif(point1.y, point2.y),
                CalculateHalfDif(point1.z, point2.z)
            );

            var origin = new Vector3(
                Mathf.Min(point1.x, point2.x),
                Mathf.Min(point1.y, point2.y),
                Mathf.Min(point1.z, point2.z)
            );
            var center = origin + relativeCenter;

            return new Box(center, size);
        }

        private static float CalculateHalfDif(float p1, float p2)
        {
            return CalculateFullDif(p1, p2) / 2f;
        }

        private static float CalculateFullDif(float p1, float p2)
        {
            return p1 > p2 ? p1 - p2 : p2 - p1;
        }

        #endregion

        public Vector3 Center { get; }
        public Vector3 Size { get; }

        public Box(Vector3 center, Vector3 size)
        {
            Center = center;
            Size = size;
        }

        public bool IsInBox(Vector3 point, bool ignoreX = false, bool ignoreY = false, bool ignoreZ = false)
        {
            var halfSize = Size / 2f;
            return (ignoreX || IsInRange(Center.x - halfSize.x, Center.x + halfSize.x, point.x)) &&
                   (ignoreY || IsInRange(Center.y - halfSize.y, Center.y + halfSize.y, point.y)) &&
                   (ignoreZ || IsInRange(Center.z - halfSize.z, Center.z + halfSize.z, point.z));
        }

        private bool IsInRange(float min, float max, float value)
        {
            return value >= min && value <= max;
        }

        #region Equals / Hashcode / ToString

        public bool Equals(Box other)
        {
            return Center.Equals(other.Center) && Size.Equals(other.Size);
        }

        public override bool Equals(object obj)
        {
            return obj is Box other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Center.GetHashCode() * 397) ^ Size.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{nameof(Center)}: {Center}, {nameof(Size)}: {Size}";
        }

        #endregion
    }
}