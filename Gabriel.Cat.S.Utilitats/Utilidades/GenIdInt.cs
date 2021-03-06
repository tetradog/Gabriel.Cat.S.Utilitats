﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class GenIdInt : GenId<int>
    {
        public static int Default = 0;
        public GenIdInt() : this(Default)
        {
        }
        public GenIdInt(int inicio) : this(inicio, int.MaxValue)
        {
        }
        public GenIdInt(int inicio, int fin)
        {
            if (inicio < fin)
                throw new ArgumentOutOfRangeException(nameof(fin));
            Inicio = inicio;
            Fin = fin;
            MetodoSiguiente = ISiguiente;
            MetodoAnterior = IAnterior;
        }
        #region implemented abstract members of GeneradorID

       void ISiguiente()
        {
            Numero++;
            if (Numero > Fin)
                Numero = Inicio;
        }

        void IAnterior()
        {
            Numero--;
            if (Numero < Inicio)
                Numero = Fin;
        }

        #endregion
    }
}