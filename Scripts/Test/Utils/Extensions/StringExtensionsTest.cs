using NUnit.Framework;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityCommonEx.Test.common_ex.Scripts.Test.Utils.Extensions
{
    public class StringExtensionsTest
    {
        [Test]
        public void TestLimitSimple()
        {
            var limit = "Hello World".Limit(20, "...");
            Assert.IsTrue(limit.Length <= 20);
            Assert.AreEqual("Hello World", limit);
        } 
        
        [Test]
        public void TestLimitUnder()
        {
            var limit = "Hello World".Limit(14, "...");
            Assert.IsTrue(limit.Length <= 14);
            Assert.AreEqual("Hello World", limit);
        } 
        
        [Test]
        public void TestLimit1Under()
        {
            var limit = "Hello World".Limit(12, "...");
            Assert.IsTrue(limit.Length <= 12);
            Assert.AreEqual("Hello Wor...", limit);
        } 
        
        [Test]
        public void TestLimit()
        {
            var limit = "Hello World".Limit(11, "...");
            Assert.IsTrue(limit.Length <= 11);
            Assert.AreEqual("Hello Wo...", limit);
        } 
        
        [Test]
        public void TestLimit1Over()
        {
            var limit = "Hello World".Limit(10, "...");
            Assert.IsTrue(limit.Length <= 10);
            Assert.AreEqual("Hello W...", limit);
        } 
        
        [Test]
        public void TestLimitOver()
        {
            var limit = "Hello World".Limit(5, "...");
            Assert.IsTrue(limit.Length <= 5);
            Assert.AreEqual("He...", limit);
        } 
    }
}