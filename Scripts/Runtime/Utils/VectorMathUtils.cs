using UnityEngine;

namespace UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils
{
    public static class VectorMathUtils
    {
        public static Vector3 CalculateRandomPositionInCircle(Vector3 center, float radius)
        {
            var arc = Random.Range(0f, Mathf.PI * 2f);
            var rad = Random.Range(0f, radius);

            return center + new Vector3(
                Mathf.Sin(arc) * rad,
                0f,
                Mathf.Cos(arc) * rad
            );
        }

        public static Rect CalculateUVRect(float scale, Vector2 transform)
        {
            var size = 1f / scale;
            var pos = -size / 2f + 0.5f;
            return new Rect(pos + transform.x, pos + transform.y, size, size);
        }
    }
}