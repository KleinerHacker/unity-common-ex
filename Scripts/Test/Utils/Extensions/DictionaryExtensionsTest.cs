using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils.Extensions
{
    public class DictionaryExtensionsTest
    {
        [Test]
        public void TestContains()
        {
            var dict = new Dictionary<Type, string>
            {
                { typeof(A), "A" },
                { typeof(C), "C" }
            };
            
            Assert.IsTrue(dict.ContainsType(typeof(A)));
            Assert.IsTrue(dict.ContainsType(typeof(B)));
            Assert.IsTrue(dict.ContainsType(typeof(C)));
            
            Assert.IsTrue(dict.ContainsType(typeof(Base)));
            
            Assert.IsFalse(dict.ContainsType(typeof(D)));
            Assert.IsFalse(dict.ContainsType(typeof(string)));
        }
        
        [Test]
        public void TestGet()
        {
            var dict = new Dictionary<Type, string>
            {
                { typeof(A), "A" },
                { typeof(C), "C" }
            };
            
            Assert.DoesNotThrow(() => dict.GetByType(typeof(A)));
            Assert.DoesNotThrow(() => dict.GetByType(typeof(B)));
            Assert.DoesNotThrow(() => dict.GetByType(typeof(C)));
            
            Assert.DoesNotThrow(() => dict.GetByType(typeof(Base)));
            
            Assert.Throws<InvalidOperationException>(() => dict.GetByType(typeof(D)));
            Assert.Throws<InvalidOperationException>(() => dict.GetByType(typeof(string)));
        }
        
        [Test]
        public void TestGetAll()
        {
            var dict = new Dictionary<Type, string>
            {
                { typeof(A), "A" },
                { typeof(C), "C" }
            };
            
            Assert.AreEqual(1, dict.GetAllByType(typeof(A)).Count);
            Assert.AreEqual(1, dict.GetAllByType(typeof(B)).Count);
            Assert.AreEqual(1, dict.GetAllByType(typeof(C)).Count);
            
            Assert.AreEqual(2, dict.GetAllByType(typeof(Base)).Count);
            
            Assert.AreEqual(0, dict.GetAllByType(typeof(D)).Count);
            Assert.AreEqual(0, dict.GetAllByType(typeof(string)).Count);
        }

        private abstract class Base
        {
        }

        private sealed class A : Base
        {
        }

        private class B : Base
        {
        }

        private sealed class C : B
        {
        }

        private sealed class D : B
        {
            
        }
    }
}