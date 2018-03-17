using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Gabriel.Cat.S.Utilitats
{
    public delegate void MetodoGeneradorID();
    /// <summary>
    /// Description of GeneradorID.
    /// </summary>
    public class GenId<TValue> : ObjectAutoId
    {
        public MetodoGeneradorID MetodoSiguiente;
        public MetodoGeneradorID MetodoAnterior;
        private TValue numero;
        protected TValue inicio;
        protected TValue fin;
        Semaphore semaphore = new Semaphore(1, 1);
        public TValue Inicio
        {
            get
            {
                return inicio;
            }
            set
            {
                inicio = value;
            }
        }

        public TValue Fin
        {
            get
            {
                return fin;
            }
            set
            {
                fin = value;
            }
        }

        public TValue Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public TValue Actual()
        {
            return numero;
        }
        public TValue Siguiente()
        {

            if (MetodoSiguiente != null)
            {
                semaphore.WaitOne();
                try
                {
                    MetodoSiguiente();
                }
                catch { numero = Inicio; }
                finally
                {
                    semaphore.Release();
                }
            }
            return numero;
        }
        public TValue Siguiente(long numeroDeVeces)
        {

            for (long i = 0; i < numeroDeVeces; i++)
                Siguiente();

            return numero;
        }
        public TValue Anterior()
        {
            if (MetodoAnterior != null)
            {
                semaphore.WaitOne();
                try
                {
                    MetodoAnterior();
                }
                catch { numero = Inicio; }
                finally
                {
                    semaphore.Release();
                }
            }
            return numero;
        }
        public TValue Anterior(long numeroDeVeces)
        {
            for (long i = 0; i < numeroDeVeces; i++)
                Anterior();
            return numero;
        }
        public void Reset()
        {
            numero = inicio;
        }
        public void Reset(TValue inicio)
        {
            this.numero = inicio;
        }
        public static GenId<TValue> operator --(GenId<TValue> gen)
        {
            gen.Anterior();
            return gen;
        }
        public static GenId<TValue> operator ++(GenId<TValue> gen)
        {
            gen.Siguiente();
            return gen;
        }
    }
}

