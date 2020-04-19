using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;

namespace TestUtilitats.Extension
{
    [TestClass]
   public class testExtensionCharArray
    {
        [TestMethod]
        public void TestSplitUnCaracter()
        {
            const char CARACTERSPLIT = ' ';

            char[] aux;
            string texto = $"qwertyuiop{CARACTERSPLIT}asdfghjklñ{CARACTERSPLIT}zxcvbnm";
            char[] caracteres = texto.ToCharArray();
            string[] splitRespuesta = texto.Split(CARACTERSPLIT);
            List<char[]> lst = caracteres.Split(CARACTERSPLIT);
            bool correcto = true;
            for(int i=0;i<lst.Count&&correcto;i++)
            {
                aux = splitRespuesta[i].ToCharArray();
                correcto = aux.SearchArray(lst[i]) == 0;
            }
            Assert.IsTrue(correcto);
        }
        [TestMethod]
        public void TestSplitDosCaracteres()
        {
            const char CARACTERSPLIT = ':';
            const char CARACTERSPLIT2 = ';';

            char[] aux;
            string[] splitRespuesta = { "qwertyuiop", "asdfghjklñ", "zxcvbnm" };
            string texto = $"{splitRespuesta[0]}{CARACTERSPLIT}{splitRespuesta[1]}{CARACTERSPLIT2}{splitRespuesta[2]}";
            char[] caracteres = texto.ToCharArray();
            List<char[]> lst = caracteres.Split(CARACTERSPLIT,CARACTERSPLIT2);
            bool correcto = true;
            for (int i = 0; i < lst.Count && correcto; i++)
            {
                aux = splitRespuesta[i].ToCharArray();
                correcto = aux.SearchArray(lst[i]) == 0;
            }
            Assert.IsTrue(correcto);
        }
    }
}
