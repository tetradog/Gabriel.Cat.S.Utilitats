using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.S.Utilitats;
using Gabriel.Cat.S.Extension;
namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionArray
    {

        [TestMethod]
        public void TestSetAnyValue()
        {
            int[] dimensions = GetRandomDimensions();
            Array array = Array.CreateInstance(typeof(int), dimensions);
            for (int i = 0, f = array.Length; i < f; i++)
                Gabriel.Cat.S.Extension.ExtensionArray.SetValue(array, dimensions, i, MiRandom.Next(1, 200));

        }
        [TestMethod]
        public void TestSetAndGetValue()
        {
            const int LADO = 5;
            int[,] matriz = new int[LADO, LADO];
            int[] dimensiones = { LADO, LADO };
            bool correcto = true;
            for (int i = 0, y = 0, yF = LADO, xF = LADO; y < yF && correcto; y++)
                for (int x = 0; x < xF && correcto; x++,i++)
                {
                    Gabriel.Cat.S.Extension.ExtensionArray.SetValue(matriz, dimensiones, i, MiRandom.Next(int.MaxValue));
                    correcto = matriz[x,y] == (int)Gabriel.Cat.S.Extension.ExtensionArray.GetValue(matriz, dimensiones, i);



                }

            Assert.IsTrue(correcto);

        }
        [TestMethod]
        public void TestGetValue()
        {
            int[] dimensions = GetRandomDimensions();
            Array array = Array.CreateInstance(typeof(int), dimensions);
            for (int i = 0, f = array.Length; i < f; i++)
                Gabriel.Cat.S.Extension.ExtensionArray.GetValue(array, dimensions, i);

        }
        private static int[] GetRandomDimensions()
        {
            int[] lenght = new int[MiRandom.Next(1, 10)];
            for (int i = 0; i < lenght.Length; i++)
                lenght[i] = MiRandom.Next(1, 15);
            return lenght;
        }
    }
}
