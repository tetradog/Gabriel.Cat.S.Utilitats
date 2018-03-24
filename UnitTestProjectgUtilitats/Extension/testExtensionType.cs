using System;
using System.Collections;
using System.Collections.Generic;
using Gabriel.Cat.S.Utilitats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gabriel.Cat.S.Extension;
namespace UnitTestProjectgUtilitats.Extension
{
    [TestClass]
    public class testExtensionType
    {
        interface InteficieTest
        {
            char T1 { get; set; }
        }
        class AtributeA : System.Attribute { }
        class AtributeB : System.Attribute { }
        class AtributeC : System.Attribute { }
        class Test:InteficieTest
        {
            string t3;
            [AtributeA]
            public char T1 { get; set; }
            [AtributeB]
            public int T2 { get; }
            [AtributeC]
            public string T3 { set { t3 = value; } }
        }
        [TestMethod]
        public void TestExtensionTypeGetPropiedadesTiposNumero()
        {
            const int TOTALPROEPIEDADES = 3;
            IList<PropiedadTipo> list = Gabriel.Cat.S.Extension.ExtensionType.GetPropiedadesTipos((typeof(Test)));
            Assert.AreEqual(TOTALPROEPIEDADES, list.Count);
           

        }
        [TestMethod]
        public void TestExtensionTypeGetPropiedadesNombreBien()
        {
        
            IList<PropiedadTipo> list = Gabriel.Cat.S.Extension.ExtensionType.GetPropiedadesTipos((typeof(Test)));
            bool pasada=list[0].Nombre=="T1"&& list[1].Nombre == "T2"&& list[2].Nombre == "T3";
            Assert.IsTrue(pasada);
            

        }
        [TestMethod]
        public void TestExtensionTypeGetPropiedadesTipoBien()
        {
          
            IList<PropiedadTipo> list = Gabriel.Cat.S.Extension.ExtensionType.GetPropiedadesTipos((typeof(Test)));
            bool pasada = list[0].Tipo.Equals(typeof(char)) && list[1].Tipo.Equals(typeof(int)) && list[2].Tipo.Equals(typeof(string));
            Assert.IsTrue(pasada);


        }
        [TestMethod]
        public void TestExtensionTypeGetPropiedadesUsoBien()
        {
         
            IList<PropiedadTipo> list = Gabriel.Cat.S.Extension.ExtensionType.GetPropiedadesTipos((typeof(Test)));
            bool pasada = list[0].Uso==(UsoPropiedad.Get|UsoPropiedad.Set) && list[1].Uso == UsoPropiedad.Get && list[2].Uso == UsoPropiedad.Set;
            Assert.IsTrue(pasada);
         

        }
        [TestMethod]
        public void TestExtensionTypeGetPropiedadesAtributosBien()
        {

            IList<PropiedadTipo> list = Gabriel.Cat.S.Extension.ExtensionType.GetPropiedadesTipos((typeof(Test)));
            bool pasada = list[0].Atributos[0] is AtributeA && list[1].Atributos[0] is AtributeB && list[2].Atributos[0] is AtributeC;
            Assert.IsTrue(pasada);


        }
        [TestMethod]
        public void TestExtensionTypeImplementaInterficieTrue()
        {

            Assert.IsTrue(Gabriel.Cat.S.Extension.ExtensionType.ImplementInterficie(typeof(Test),typeof(InteficieTest)));


        }
        [TestMethod]
        public void TestExtensionTypeImplementaInterficieFalse()
        {
            Assert.IsFalse(Gabriel.Cat.S.Extension.ExtensionType.ImplementInterficie(typeof(Test), typeof(IList)));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExtensionTypeImplementaInterficieNoPasanInterficie()
        {
            Gabriel.Cat.S.Extension.ExtensionType.ImplementInterficie(typeof(Test), typeof(List<object>));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExtensionTypeImplementaInterficiePasanNull()
        {
            Gabriel.Cat.S.Extension.ExtensionType.ImplementInterficie(typeof(Test),null);
        }
        [TestMethod]
        public void TestExtensionTypeNullTypeTrue()
        {
            Assert.IsTrue(typeof(BitmapAnimated).IsNullableType());
        }
        [TestMethod]
        public void TestExtensionTypeNullInterficieTypeTrue()
        {
            Assert.IsTrue(typeof(IList).IsNullableType());
        }
        [TestMethod]
        public void TestExtensionTypeNullableTypeTrue()
        {
            Assert.IsTrue(typeof(int?).IsNullableType());
        }
        [TestMethod]
        public void TestExtensionTypeNullTypeFalse()
        {
            Assert.IsFalse(typeof(int).IsNullableType());
        }
    }
}
