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
        public void Remove()
        {
            var array = new [] {"Hello", "World", "Unity"};
            array = array.Remove("World").ToArray();
            
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("Hello", array[0]);
            Assert.AreEqual("Unity", array[1]);
        }

        [Test]
        public void IndexOf()
        {
            var array = new [] {"Hello", "World", "Unity"};
            var foundIndex = array.IndexOf("World");
            var unknownIndex = array.IndexOf("Foo");
            
            Assert.AreEqual(1, foundIndex);
            Assert.AreEqual(-1, unknownIndex);
        }
    }
}
