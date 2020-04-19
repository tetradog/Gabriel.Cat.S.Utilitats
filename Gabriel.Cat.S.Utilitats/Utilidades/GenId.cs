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

        Semaphore semaphore = new Semaphore(1, 1);
        public TValue Inicio
        {
            get;
            set;
        }

        public TValue Fin
        {
            get;
            set;
        }

        public TValue Numero { get; set; }

        public TValue Actual()
        {
            return Numero;
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
                catch { Numero = Inicio; }
                finally
                {
                    semaphore.Release();
                }
            }
            return Numero;
        }
        public TValue Siguiente(long numeroDeVeces)
        {

            for (long i = 0; i < numeroDeVeces; i++)
                Siguiente();

            return Numero;
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
                catch { Numero = Inicio; }
                finally
                {
                    semaphore.Release();
                }
            }
            return Numero;
        }
        public TValue Anterior(long numeroDeVeces)
        {
            for (long i = 0; i < numeroDeVeces; i++)
                Anterior();
            return Numero;
        }
        public void Reset()
        {
            Numero = Inicio;
        }
        public void Reset(TValue inicio)
        {
            this.Numero = inicio;
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

