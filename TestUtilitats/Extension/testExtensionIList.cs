using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
using System.Collections;
using Gabriel.Cat.S.Utilitats;

namespace UnitTestProjectgUtilitats.Extension
{
 
    [TestClass]
    public class testExtensionIList
    {
        [TestMethod]
        public void TestExtensionIListListOfWhatSuccess()
        {
            Assert.AreEqual(typeof(int), (new List<int>()).ListOfWhat());
        }

        [TestMethod]
        public void TestExtensionIListCastingSuccess()
        {
            List<object> objs = new List<object>();
            int[] ints;
            objs.Add(10);
            objs.Add("test");
            objs.Add(112);
            ints= objs.Casting<int>();
            Assert.IsTrue(objs[0].Equals(ints[0])&&!objs[1].Equals(ints[1])&& objs[2].Equals(ints[2]));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestExtensionIListCastingFail()
        {
            List<object> objs = new List<object>();
            int[] ints;
            objs.Add(10);
            objs.Add("test");
            objs.Add(112);
            ints = objs.Casting<int>(false);
           
        }
        [TestMethod]
        public void TestExtensionIListSortQuickSortAscending()
        {
            IListSortAscending(SortMethod.QuickSort);
        }
        [TestMethod]
        public void TestExtensionIListSortBubbleAscending()
        {
            IListSortAscending(SortMethod.Bubble); 
        }
        void IListSortAscending(SortMethod sort)
        {
            int[] original = { 1, 2, 3, 4, 5, 6 };
            int[] toCompare = { 6, 3, 4, 5, 2, 1 };
            toCompare.Sort(sort);
            Assert.IsTrue(original.AreEquals(toCompare));
        }
        [TestMethod]
        public void TestExtensionIListSortQuickSortDescending()
        {
            IListSortDescending(SortMethod.QuickSort);
        }
        [TestMethod]
        public void TestExtensionIListSortBubbleDescending()
        {
            IListSortDescending(SortMethod.Bubble);
        }
        void IListSortDescending(SortMethod sort)
        {
            int[] original = { 6,5,4,3,2,1 };
            int[] toCompare = { 6, 3, 4, 5, 2, 1 };
            toCompare.Sort(sort,false);
            Assert.IsTrue(original.AreEquals(toCompare));
        }
        [TestMethod]
        public void TestExtensionIListSortQuickSortAscendingNull()
        {
            IListSortAscendingNull(SortMethod.QuickSort);
        }
        [TestMethod]
        public void TestExtensionIListSortBubbleAscendingNull()
        {
            IListSortAscendingNull(SortMethod.Bubble);
        }
        void IListSortAscendingNull(SortMethod sort)
        {
            string[] original = {null, "a","b","c","d"};
            string[] toCompare = { "b","d","a",null,"c" };
            toCompare.Sort(sort, true);
            Assert.IsTrue(original.AreEquals(toCompare));
        }

        [TestMethod]
        public void TestExtensionIListGetElementAtConsecutiu()
        {
            const int POS = 2;
            int[] ints = {0,1,2,3,4,5 };
            Assert.AreEqual<int>(POS, ints.GetElementActual(Ordre.Consecutiu, POS));
        }
        [TestMethod]
        public void TestExtensionIListGetElementAtConsecutiuLoop()
        {
            const int POS = 2;
            int[] ints = { 0, 1, 2, 3, 4, 5 };
            Assert.AreEqual<int>(POS, ints.GetElementActual(Ordre.Consecutiu, POS+ints.Length));
        }
        [TestMethod]
        public void TestExtensionIListGetElementAtConsecutiuIAlInreves()
        {
            const int POS = 2;
            int[] ints = { 0, 1, 2, 3, 4, 5 };
            Assert.AreEqual<int>(POS, ints.GetElementActual(Ordre.ConsecutiuIAlInreves, POS));
        }
        [TestMethod]
        public void TestExtensionIListGetElementAtConsecutiuIAlInrevesLoop()
        {
            const int POS = 2,VALOR=3;
            int[] ints = { 0, 1, 2, 3, 4, 5 };
            Assert.AreEqual<int>(VALOR, ints.GetElementActual(Ordre.ConsecutiuIAlInreves, POS + ints.Length));
        }
        [TestMethod]
        public void TestExtensionIListSwapSuccess()
        {
            int[] ints = { 1, 2 };
            ints.Swap(0, 1);
            Assert.AreEqual(1, ints[1]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestExtensionIListSwapFailOutOffRange()
        {
            int[] ints = { 1, 2 };
            ints.Swap(0, 2);
 
        }
        [TestMethod]
        public void TestExtensionIListConvert()
        {
            byte[] input = { 0x1, 0x2, 0x3 };
            int[] answer = { 1, 2, 3 };
            int[] converted = ExtensionIList.Convert(input,(i) => (int)i);
            Assert.IsTrue(answer.AreEquals(converted));
        }
    }
}
