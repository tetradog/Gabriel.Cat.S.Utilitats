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
            StringBuilder texto = new StringBuilder(textoHaEscapar);
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
        public static string NormalitzeFileName(this string fileName, string toReplace = "-")
        {
            //source:https://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(fileName, invalidRegStr, toReplace);

        }
        public static string[] Divide(this string txt, string caracteresSplitSeguidos)
        {
            if (string.IsNullOrEmpty(caracteresSplitSeguidos))
                throw new ArgumentException("Se requieren caracteres para realizar el Split!");
            IList<char[]> filasChar = txt.ToCharArray().Split(caracteresSplitSeguidos.ToCharArray());
            string[] filas = new string[filasChar.Count];
            for (int i = 0; i < filas.Length; i++)
                filas[i] = new string(filasChar[i]);
            return filas;
        }

        public static bool StartsWith(this string str, params string[] toFindAny)
        {
            bool starts = false;
            for (int i = 0; i < toFindAny.Length && !starts; i++)
                starts = str.StartsWith(toFindAny[i]);
            return starts;
        }
    }
}
