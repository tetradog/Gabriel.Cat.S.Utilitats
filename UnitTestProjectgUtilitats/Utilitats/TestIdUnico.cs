using System;
using Gabriel.Cat.S.Utilitats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectgUtilitats.Utilitats
{
    [TestClass]
    public class TestIdUnico
    {
        [TestMethod]
        public void TestIdUnicoLasClavesSonDiferentes()
        {
            IComparable id1 = new IdUnico();
            IComparable id2 = new IdUnico();
            Assert.IsFalse(id1.CompareTo(id2) == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals);
        }
        [TestMethod]
        public void TestIdUnicoLasClavesSonIguales()
        {
            IdUnico id1 = new IdUnico();
            IComparable id2 = new IdUnico(id1.GetId());
            Assert.IsTrue(id2.CompareTo(id1) == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals);
        }
    }
}
