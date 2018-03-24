using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionObjectArray
    {
        [TestMethod]
        public void TestExtensionObjectArrayConversionSuccessful()
        {
            object[] objs = new object[] {(byte)0x0, (byte)0xF, (byte)0x30, (byte)0xFF };
            byte[] bytes = Gabriel.Cat.S.Extension.ExtensionObjectArray.CastingToByte(objs);
            bool iguales = true;
            for (int i = 0; i < objs.Length && iguales; i++)
                iguales = objs[i].Equals(bytes[i]);
            Assert.IsTrue(iguales);
        }
        [TestMethod]
        public void TestExtensionObjectArrayConversionFail()
        {
            object[] objs = new object[] { (byte)0x0, (byte)0xF, (byte)0x30, new List<int>() };
            byte[] bytes = Gabriel.Cat.S.Extension.ExtensionObjectArray.CastingToByte(objs);
            bool iguales = true;
            for (int i = 0; i < objs.Length && iguales; i++)
                iguales = objs[i].Equals(bytes[i]);
            Assert.IsFalse(iguales);
        }
    }
}
