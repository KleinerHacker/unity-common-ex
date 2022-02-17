using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils
{
    public class TestCounting
    {
        [Test]
        public void TestDecrementing()
        {
            var decrementing = new Decrementing(10);

            var counter = 0;
            while (!decrementing.Try(() => {}) && counter < 100)
            {
                counter++;
            }
            
            Assert.AreEqual(9, counter);
        }
        
        [Test]
        public void TestIncrementing()
        {
            var incrementing = new Incrementing(10);

            var counter = 0;
            while (!incrementing.Try(() => {}) && counter < 100)
            {
                counter++;
            }
            
            Assert.AreEqual(9, counter);
        }
    }
}