using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionStream
    {
        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ExtensionStreamNegativeLenght()
        {
            new MemoryStream(new byte[1]).Read(-1);
        }
        [TestMethod]
        public void ExtensionStreamReadMore()
        {
            const int LENGHTARRAY = 1, LENGHTREAD = LENGHTARRAY + 1;
            Assert.IsTrue(new MemoryStream(new byte[LENGHTARRAY]).Read(LENGHTREAD).Length == LENGHTREAD);
        }
        [TestMethod]
        public void ExtensionStreamReadZero()
        {
            const int LENGHTARRAY = 1, LENGHTREAD = 0;
            Assert.IsTrue(new MemoryStream(new byte[LENGHTARRAY]).Read(LENGHTREAD).Length == LENGHTREAD);
        }
        [TestMethod]
        public void ExtensionStreamLengh0Read1()
        {
            const int LENGHTARRAY = 0, LENGHTREAD = 1;
            Assert.IsTrue(new MemoryStream(new byte[LENGHTARRAY]).Read(LENGHTREAD).Length == LENGHTREAD);
        }
    }
}
