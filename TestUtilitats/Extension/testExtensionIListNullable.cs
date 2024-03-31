using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
using System.Linq;
namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionIListNullable
    {
        [TestMethod]
        public void TestExtensionIListNullableListOfWhatSuccess()
        {
            Assert.AreEqual(typeof(int?), (new List<int?>()).ListOfWhat());
        }
        [TestMethod]
        public void TestExtensionIListNullableCasting()
        {
            int?[] ints = new object[] { 1, 2, 3 ,null}.Casting<int?>().ToArray();
        }
    }
}
