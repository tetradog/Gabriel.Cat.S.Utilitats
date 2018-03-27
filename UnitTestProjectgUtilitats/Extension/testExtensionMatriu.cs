using System;
using Gabriel.Cat.S.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionMatriu
    {
        [TestMethod]
        public void TestExtensionMatriuDimensionX()
        {
            const int X = 2;
            int[,] matriu = new int[X, 3];
            Assert.AreEqual(X, matriu.GetLength(DimensionMatriz.X));
        }
        [TestMethod]
        public void TestExtensionMatriuDimensionY()
        {
            const int Y = 2;
            int[,] matriu = new int[3,Y];
            Assert.AreEqual(Y, matriu.GetLength(DimensionMatriz.Y));
        }
        [TestMethod]
        public void TestExtensionMatriuDimensionZ()
        {
            const int Z = 2;
            int[,,] matriu = new int[3,3, Z];
            Assert.AreEqual(Z, matriu.GetLength(DimensionMatriz.Z));
        }
    }
}
