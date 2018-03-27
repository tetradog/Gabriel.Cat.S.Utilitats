using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionInt
    {
        [TestMethod]
        public void TestExtensionIntEsPrimeroFalse()
        {
            const int NOPRIMO = 114;
            Assert.IsFalse(NOPRIMO.EsPrimero());
        }
        [TestMethod]
        public void TestExtensionIntEsPrimeroTrue()
        {
            const int NOPRIMO = 223;
            Assert.IsTrue(NOPRIMO.EsPrimero());
        }
        [TestMethod]
        public void TestExtensionIntDamePrimeroCercano()
        {
            const int NOPRIMO = 14;
            const int PRIMOCERCANO = 17;
            Assert.AreEqual(PRIMOCERCANO,NOPRIMO.DamePrimeroCercano());
        }
    }
}
