using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
namespace UnitTestProjectgUtilitats.Extension.Reflexion
{
    [TestClass]
    public class testPropertyInfoExtension
    {
        /*
         [TestMethod]  
        [ExpectedException(typeof(ArgumentException))]  
            metodoTestConExcepcionEsperada
             */
        class CustomAttribute : System.Attribute
        { }
        class Test
        {
            string test;
            [CustomAttribute]
            public string Get { set; get; }
            public string OnlyGet { get { return test; } }
            public string OnlySet { set { test = value; } }
        }
        [TestMethod]
        public void ExtensionPropertyInfoGetAttibutesPropertyInfo()
        {
            Assert.IsTrue(Gabriel.Cat.S.Extension.ExtensionPropertyInfo.GetAttributes(typeof(Test).GetProperty("Get"))[0] is CustomAttribute);

        }
        [TestMethod]
        public void ExtensionPropertyInfoGetPropertyUsageOnlyGetPropertyInfo()
        {
            Assert.IsTrue(Gabriel.Cat.S.Extension.ExtensionPropertyInfo.GetPropertyUsage(typeof(Test).GetProperty("OnlyGet")) == Gabriel.Cat.S.Utilitats.UsoPropiedad.Get);
        }
        [TestMethod]
        public void ExtensionPropertyInfoGetPropertyUsageOnlySetPropertyInfo()
        {
            Assert.IsTrue(Gabriel.Cat.S.Extension.ExtensionPropertyInfo.GetPropertyUsage(typeof(Test).GetProperty("OnlySet")) == Gabriel.Cat.S.Utilitats.UsoPropiedad.Set);
        }
        [TestMethod]
        public void ExtensionPropertyInfoGetPropertyUsageGetAndSetPropertyInfo()
        {
            
            Gabriel.Cat.S.Utilitats.UsoPropiedad uso = Gabriel.Cat.S.Extension.ExtensionPropertyInfo.GetPropertyUsage(typeof(Test).GetProperty("Get"));
            Assert.IsTrue((uso & Gabriel.Cat.S.Utilitats.UsoPropiedad.Get) == Gabriel.Cat.S.Utilitats.UsoPropiedad.Get && (uso & Gabriel.Cat.S.Utilitats.UsoPropiedad.Set) == Gabriel.Cat.S.Utilitats.UsoPropiedad.Set);
        }
        [TestMethod]
        public void ExtensionPropertyInfoSetPropertyValuePropertyInfo()
        {
            const string TEST = "TEST";
            Test t = new Test();
            Gabriel.Cat.S.Extension.ExtensionPropertyInfo.SetValue(typeof(Test).GetProperty("Get"), t, TEST);
            Assert.AreEqual(TEST, t.Get);
        }
        [TestMethod]
        public void ExtensionPropertyInfoGetPropertyValuePropertyInfo()
        {
            const string TEST = "TEST";
            Test t = new Test() { Get = TEST };
            string value = (string)Gabriel.Cat.S.Extension.ExtensionPropertyInfo.GetValue(typeof(Test).GetProperty("Get"), t);
            Assert.AreEqual(TEST, value);
            
        }
    }
}
