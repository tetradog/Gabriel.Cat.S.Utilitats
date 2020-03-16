using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Utilitats;
namespace UnitTestProjectgUtilitats.Types
{
    [TestClass]
    public class TestHex
    {
        [TestMethod]
        public void TestHexShort()
        {
            const short TESTSHORT = 3369;
            Hex testConversion = (Hex)TESTSHORT;
            short intHex = (short)testConversion;//"D29‬"
            Assert.AreEqual(TESTSHORT, intHex);
        }
        [TestMethod]
        public void TestHexUShort()
        {
            const ushort TESTUSHORT = 3369;
            Hex testConversion = (Hex)(uint)TESTUSHORT;
            ushort intHex = (ushort)testConversion;//"D29‬"
            Assert.AreEqual(TESTUSHORT, intHex);
        }
        [TestMethod]
        public void TestHexInt()
        {
            const int TESTINT = 33693636;
            Hex testConversion = (Hex)TESTINT;
            int intHex = (int)testConversion;//"‭2021FC4‬"
            Assert.AreEqual(TESTINT, intHex);
        }
        [TestMethod]
        public void TestHexUInt()
        {
            const uint TESTUINT = 33693636;
            Hex testConversion = (Hex)TESTUINT;
            uint intHex = (uint)testConversion;//"‭2021FC4‬"
            Assert.AreEqual(TESTUINT, intHex);
        }
        [TestMethod]
        public void TestHexLong()
        {
            const long TESTLONG = 33693636L;
            Hex testConversion = (Hex)TESTLONG;
            long intHex = (long)testConversion;//"‭2021FC4‬"
            Assert.AreEqual(TESTLONG, intHex);
        }
        [TestMethod]
        public void TestHexULong()
        {
            const ulong TESTULONG = 33693636L;
            Hex testConversion = (Hex)TESTULONG;
            ulong intHex = (ulong)testConversion;//"‭2021FC4‬"
            Assert.AreEqual(TESTULONG, intHex);
        }
        [TestMethod]
        public void TestHexString()
        {
            const ulong TESTULONG = 33693636L;
            const string TESTSTRING="‭2021FC4‬";//2021FC4
            Hex testConversion = (Hex)TESTULONG;
            string ulongHex = (string)testConversion;

            Assert.IsTrue(String.Compare(TESTSTRING,ulongHex)==0);
        }
    }
}
