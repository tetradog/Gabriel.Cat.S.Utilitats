using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionRectangle
    {
        [TestMethod]
        public void TestExtensionRectanglePointOutX()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X - 1, rect.Location.Y);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == -1 && relativePoint.Y == 0);
        }
        [TestMethod]
        public void TestExtensionRectanglePointOutY()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X, rect.Location.Y - 1);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == relativePoint.X && relativePoint.Y == -1);
        }
    }
}
