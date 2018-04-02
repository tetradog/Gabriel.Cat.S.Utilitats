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
        public void TestExtensionIListListOfWhatFail()
        {
            Assert.AreNotEqual(typeof(string), (new List<int>()).ListOfWhat());
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
            int[] unsorted = { 6, 3, 4, 5, 2, 1 };
            IList<int> sorted = unsorted.Sort(sort);
            Assert.IsTrue(original.AreEquals(sorted));
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
            int[] unsorted = { 6, 3, 4, 5, 2, 1 };
            IList<int> sorted = unsorted.Sort(sort,false);
            Assert.IsTrue(original.AreEquals(sorted));
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
            string[] unsorted = { "b","d","a",null,"c" };
            IList<string> sorted = unsorted.Sort(sort, true);
            Assert.IsTrue(original.AreEquals(sorted));
        }
    }
}
