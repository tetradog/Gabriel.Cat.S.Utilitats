using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionCharArray
    {

        public static List<char[]> Split(this char[] caracteres,string caracteresSplit)
        {
            return Split(caracteres, caracteresSplit.ToCharArray());
        }
        public static List<char[]> Split(this char[] caracteres,params char[] caracteresSplit)
        {//falta testing
            const int FIN = -1;
            List<char[]> lstPartes = new List<char[]>();
            int posicionCaracteresSplit=0;
            int posicionAnt;
            do
            {
                posicionAnt = posicionCaracteresSplit;
                posicionCaracteresSplit = -1;
                for(int i=0;i<caracteresSplit.Length&&posicionCaracteresSplit==-1;i++)
                   posicionCaracteresSplit = caracteres.SearchArray(posicionAnt, new char[] { caracteresSplit[i] });
                if(posicionCaracteresSplit!=FIN&& posicionCaracteresSplit + caracteresSplit.Length < caracteres.Length)
                {
                    lstPartes.Add(caracteres.SubArray(posicionAnt,posicionCaracteresSplit-posicionAnt));
                    posicionCaracteresSplit++;
                }
               
            } while (posicionCaracteresSplit != FIN);
            if (posicionAnt + caracteresSplit.Length < caracteres.Length)
                lstPartes.Add(caracteres.SubArray(posicionAnt,caracteres.Length- posicionAnt));
            return lstPartes;
        }
    }
}
