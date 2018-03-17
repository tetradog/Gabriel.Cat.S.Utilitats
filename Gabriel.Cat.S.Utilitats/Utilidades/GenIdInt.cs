using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class GenIdInt : GenId<int>
    {

        public GenIdInt() : this(0)
        {
        }
        public GenIdInt(int inicio) : this(inicio, int.MaxValue)
        {
        }
        public GenIdInt(int inicio, int fin)
        {
            this.inicio = inicio;
            this.fin = fin;
            MetodoSiguiente = ISiguiente;
            MetodoAnterior = IAnterior;
        }
        #region implemented abstract members of GeneradorID

       void ISiguiente()
        {
            Numero++;
            if (Numero > fin)
                Numero = inicio;
        }

        void IAnterior()
        {
            Numero--;
            if (Numero < inicio)
                Numero = fin;
        }

        #endregion
    }
}