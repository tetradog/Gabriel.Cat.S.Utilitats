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
            Assert.IsTrue(relativePoint.X == 0 && relativePoint.Y == -1);
        }
        [TestMethod]
        public void TestExtensionRectanglePointOutYX()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X-1, rect.Location.Y - 1);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == -1 && relativePoint.Y == -1);
        }
        [TestMethod]
        public void TestExtensionRectanglePointInXOutY()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X +1, rect.Location.Y - 1);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == 1 && relativePoint.Y == -1);
        }
        [TestMethod]
        public void TestExtensionRectanglePointInYOutX()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X - 1, rect.Location.Y + 1);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == -1 && relativePoint.Y == 1);
        }
        [TestMethod]
        public void TestExtensionRectanglePointInYX()
        {
            Rectangle rect = new Rectangle(10, 2, 200, 300);
            Point pointOutX = new Point(rect.Location.X + 1, rect.Location.Y + 1);
            Point relativePoint = Gabriel.Cat.S.Extension.ExtensionRectangle.GetRelativePoint(rect, pointOutX);
            Assert.IsTrue(relativePoint.X == 1 && relativePoint.Y == 1);
        }
    }
}
