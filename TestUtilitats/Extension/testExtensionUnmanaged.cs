using Gabriel.Cat.S.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUtilitats.Extension
{
    [TestClass]
    public class testExtensionUnmanaged
    {
        [TestMethod]
        public void testExtensionUnmanagedJoinArrayFromParams()
        {
            byte[] init = { 0x2, 0x1 };
            byte[] first = { 0x3, 0x6 };
            byte[] second = { 0x8, 0x9 };
            byte[] answer = { 0x2, 0x1, 0x3, 0x6, 0x8, 0x9 };
            byte[] total=init.AddArray(first,second);
            Assert.IsTrue(answer.AreEquals(total));
        }
        [TestMethod]
        public void testExtensionUnmanagedJoinArrayFromIList()
        {
            byte[] init = { 0x2, 0x1 };
            byte[] first = { 0x3, 0x6 };
            byte[] second = { 0x8, 0x9 };
            byte[] answer = { 0x2, 0x1, 0x3, 0x6, 0x8, 0x9 };
            byte[] total = init.AddArray(new List<byte[]>{ first, second});
            Assert.IsTrue(answer.AreEquals(total));

        }
    }
}
