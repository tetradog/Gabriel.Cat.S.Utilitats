using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionString
    {
        #region NormalizarXml
        //voy ha escapar caracteres no permitidos
        static readonly string[] caracteresXmlSustitutos = {
            "&lt;",
            "&gt;",
            "&amp;",
            "&quot;",
            "&apos;"
        };
        static readonly string[] caracteresXmlReservados = {
            "<",
            ">",
            "&",
            "\"",
            "\'"
        };
        #endregion
        static LlistaOrdenada<string, string> caracteresReservadosXml;
        static ExtensionString()
        {
            #region NormalizarXml
            caracteresReservadosXml = new LlistaOrdenada<string, string>();
            for (int i = 0; i < caracteresXmlReservados.Length; i++)
            {
                caracteresReservadosXml.Add(caracteresXmlReservados[i], caracteresXmlSustitutos[i]);
                caracteresReservadosXml.Add(caracteresXmlSustitutos[i], caracteresXmlReservados[i]);
            }
            #endregion
        }

        #region NormalizarXml
        private static string TratarCaracteresXML(string textoHaEscapar, string[] caracteresASustituir)
        {
            StringBuilder texto = new StringBuilder( textoHaEscapar);
            for (int j = 0; j < caracteresASustituir.Length; j++)
                texto.Replace(caracteresASustituir[j], caracteresReservadosXml[caracteresASustituir[j]]);
            return texto.ToString();
        }
        public static string EscaparCaracteresXML(this string textoHaEscapar)
        {
            return TratarCaracteresXML(textoHaEscapar, caracteresXmlReservados);
        }
        public static string DescaparCaracteresXML(this string textoHaDesescapar)
        {
            return TratarCaracteresXML(textoHaDesescapar, caracteresXmlSustitutos);
        }
        #endregion
        public static string[] Divide(this string txt,string caracteresSplitSeguidos)
        {
            if (string.IsNullOrEmpty(caracteresSplitSeguidos))
                throw new ArgumentException("Se requieren caracteres para realizar el Split!");
            IList<char[]> filasChar = txt.ToCharArray().Split(caracteresSplitSeguidos.ToCharArray());
            string[] filas = new string[filasChar.Count];
            for (int i = 0; i < filas.Length; i++)
                filas[i] = new string(filasChar[i]);
            return filas;
        }
    }
}
