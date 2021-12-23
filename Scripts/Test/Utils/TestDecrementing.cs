using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils
{
    public class TestDecrementing
    {
        [Test]
        public void Test()
        {
            var decrementing = new Decrementing(10);

            var counter = 0;
            while (!decrementing.Try(() => {}))
            {
                counter++;
            }
            
            Assert.AreEqual(9, counter);
        }
    }
}