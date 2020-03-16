using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionPixelFormat
    {
        [TestMethod]
        public void TestExtensionPixelFormatArgbTrue()
        {
            Assert.IsTrue(Gabriel.Cat.S.Extension.ExtensionPixelFormat.IsArgb(System.Drawing.Imaging.PixelFormat.Format16bppArgb1555));
        }
        [TestMethod]
        public void TestExtensionPixelFormatArgbFalse()
        {
            Assert.IsFalse(Gabriel.Cat.S.Extension.ExtensionPixelFormat.IsArgb(System.Drawing.Imaging.PixelFormat.Format16bppRgb555));
        }
    }
}
