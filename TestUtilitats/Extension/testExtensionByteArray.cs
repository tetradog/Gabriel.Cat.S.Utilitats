using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;

namespace TestUtilitats.Extension
{
    [TestClass]
    public class testExtensionByteArray
    {
        [TestMethod]
        public void TestInvertirArray()
        {
            byte[] array = new byte[byte.MaxValue+1];
            bool correcto=true;
            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)i;
            array.Invertir();
            for (int i = array.Length - 1, j = 0; i >= 0 && correcto; i--, j++)
                correcto = array[i] == (byte)j;
            Assert.IsTrue(correcto);
        }
    }
}
