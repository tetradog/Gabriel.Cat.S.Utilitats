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
        public void TestSetValue()
        {
            int[] dimensions = GetRandomDimensions();
            Array array = Array.CreateInstance(typeof(int), dimensions);
            for (int i = 0, f = array.Length; i < f; i++)
                Gabriel.Cat.S.Extension.ExtensionArray.SetValue(array, dimensions, i, MiRandom.Next(1, 200));
          
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
