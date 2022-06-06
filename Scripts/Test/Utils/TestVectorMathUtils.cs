using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils;
using UnityEngine;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils
{
    public class TestVectorMathUtils
    {
        [Test]
        public void TestUVRectSimple()
        {
            var rect = VectorMathUtils.CalculateUVRect(1f, Vector2.zero);
            Assert.AreEqual(new Rect(0f, 0f, 1f, 1f), rect);

            rect = VectorMathUtils.CalculateUVRect(1f, Vector2.one);
            Assert.AreEqual(new Rect(1f, 1f, 1f, 1f), rect);
        }
        
        [Test]
        public void TestUVRectHalf()
        {
            var rect = VectorMathUtils.CalculateUVRect(0.5f, Vector2.zero);
            Assert.AreEqual(new Rect(-0.5f, -0.5f, 2f, 2f), rect);

            rect = VectorMathUtils.CalculateUVRect(0.5f, Vector2.one);
            Assert.AreEqual(new Rect(0.5f, 0.5f, 2f, 2f), rect);
        }
        
        [Test]
        public void TestUVRectDouble()
        {
            var rect = VectorMathUtils.CalculateUVRect(2f, Vector2.zero);
            Assert.AreEqual(new Rect(0.25f, 0.25f, 0.5f, 0.5f), rect);

            rect = VectorMathUtils.CalculateUVRect(2f, Vector2.one);
            Assert.AreEqual(new Rect(1.25f, 1.25f, 0.5f, 0.5f), rect);
        }
    }
}