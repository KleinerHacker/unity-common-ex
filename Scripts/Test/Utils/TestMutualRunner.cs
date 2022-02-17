using System;
using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils
{
    public class TestMutualRunner
    {
        [Test]
        public void Test()
        {
            var counter = 0;
            var runner = new MutualRunner();
            var mainRun = runner.Try(() =>
            {
                counter++;
                var run = runner.Try(() => counter++);
                Assert.IsFalse(run);
            });
            
            Assert.IsTrue(mainRun);
            Assert.AreEqual(1, counter);
        }
    }
}