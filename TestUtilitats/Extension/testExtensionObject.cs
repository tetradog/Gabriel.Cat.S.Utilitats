using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
using System.Reflection;

namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionObject
    {
        class Test { 
            public int T1 { get; set; }

}
        [TestMethod]
        public void TestExtensionObjectPropertySuccess()
        {
            const string PROPERTY = "Count";
            PropertyInfo property = new List<int>().Property(PROPERTY);

            Assert.AreEqual(PROPERTY,property.Name );
        }
        [TestMethod]
        public void TestExtensionObjectPropertyFail()
        {
            const string PROPERTY = "Push";
            Assert.IsNull( new List<int>().Property(PROPERTY));
        }
        [TestMethod]
        public void TestExtensionObjectGetProperty()
        {
            const int VALUE = 10;
            List<int> lst = new List<int>();
            lst.Add(VALUE);
            Assert.AreEqual(lst.Count,lst.GetProperty("Count"));
        }
        [TestMethod]
        public void TestExtensionObjectSetProperty()
        {
            const int VALUE = 10;
            Test t = new Test();
            t.SetProperty("T1", VALUE);
            Assert.AreEqual(VALUE, t.T1);
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExtensionObjectSetPropertyFail()
        {
          
            Test t = new Test();
            t.SetProperty("T1", new object());


        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExtensionObjectSetPropertyNullOnStructReturnPropertyType()
        {

            Test t = new Test();
            t.SetProperty("T1", null);


        }
        [TestMethod]
        public void TestExtensionObjectGetProperties()
        {

            Test t = new Test();
            Assert.AreEqual(1, t.GetPropiedades().Count);

        }
    }
}
