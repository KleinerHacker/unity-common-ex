using System;
using System.Linq;
using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils.Extensions
{
    public class ListExtensionsTest
    {
        [Test]
        public void GetRandom()
        {
            var array = new [] {"Hello", "World", "Unity"};

            for (var i = 0; i < 100; i++)
            {
                array.GetRandom();
            }

            for (var i = 0; i < 100; i++)
            {
                var random = array.GetRandom("Unity");
                Assert.AreNotEqual("Unity", random);
            }
        }
        
        [Test]
        public void GetRandomByWeight()
        {
            var array = new [] {"Hello", "World", "Unity"};
            var weights = array.ToWeightList(x => x == "Unity" ? 0f : 1f);

            for (var i = 0; i < 100; i++)
            {
                var random = weights.GetRandomByWeight();
                Assert.AreNotEqual("Unity", random);
            }

            for (var i = 0; i < 100; i++)
            {
                var random = weights.GetRandomByWeight("World");
                Assert.AreNotEqual("Unity", random);
                Assert.AreNotEqual("World", random);
            }
        }

        [Test]
        public void Remove()
        {
            var array = new [] {"Hello", "World", "Unity"};
            array = array.Remove("World").ToArray();
            
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("Hello", array[0]);
            Assert.AreEqual("Unity", array[1]);
        }
        
        [Test]
        public void RemoveAll()
        {
            var array = new [] {"Hello", "World", "Unity"};
            array = array.RemoveAll("World", "Hello").ToArray();
            
            Assert.AreEqual(1, array.Length);
            Assert.AreEqual("Unity", array[0]);
        }

        [Test]
        public void IndexOf()
        {
            var array = new [] {"Hello", "World", "Unity", "World"};
            var foundIndex = array.IndexOf(x => x == "World");
            var unknownIndex = array.IndexOf(x => x == "Foo");
            
            Assert.AreEqual(1, foundIndex);
            Assert.AreEqual(-1, unknownIndex);
        }
        
        [Test]
        public void LastIndexOf()
        {
            var array = new [] {"Hello", "World", "Unity", "World"};
            var foundIndex = array.LastIndexOf(x => x == "World");
            var unknownIndex = array.LastIndexOf(x => x == "Foo");
            
            Assert.AreEqual(3, foundIndex);
            Assert.AreEqual(-1, unknownIndex);
        }

        [Test]
        public void Doublets()
        {
            var array = new [] {"Hello", "World", "Unity", "World"};
            Assert.IsTrue(array.HasDoublets());
            
            array = new [] {"Hello", "World", "Unity"};
            Assert.IsFalse(array.HasDoublets());
        }
    }
}
