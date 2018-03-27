using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
using System.Collections;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionIList
    {
        [TestMethod]
        public void TestExtensionIListListOfWhatSuccess()
        {
            Assert.AreEqual(typeof(int), (new List<int>()).ListOfWhat());
        }
        [TestMethod]
        public void TestExtensionIListListOfWhatFail()
        {
            Assert.AreNotEqual(typeof(string), (new List<int>()).ListOfWhat());
        }
        [TestMethod]
        public void TestExtensionIListCastingSuccess()
        {
            List<object> objs = new List<object>();
            int[] ints;
            objs.Add(10);
            objs.Add("test");
            objs.Add(112);
            ints= objs.Casting<int>();
            Assert.IsTrue(objs[0].Equals(ints[0])&&!objs[1].Equals(ints[1])&& objs[2].Equals(ints[2]));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestExtensionIListCastingFail()
        {
            List<object> objs = new List<object>();
            int[] ints;
            objs.Add(10);
            objs.Add("test");
            objs.Add(112);
            ints = objs.Casting<int>(false);
           
        }

    }
}
