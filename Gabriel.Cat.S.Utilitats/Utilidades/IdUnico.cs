using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Gabriel.Cat.S.Extension;
namespace Gabriel.Cat.S.Utilitats
{
    public class IdUnico:IComparable<IdUnico>,IComparable,IClauUnicaPerObjecte
    {
        public const int LENGHT = sizeof(long) + 100;
        const int MAXTIMERANDOMGEN =100;
        static Semaphore semaphoreRandom;
        static readonly Random r;

         byte[] idUnico;

        static IdUnico()
        {
            System.Threading.Thread.Sleep(MiRandom.Next(MAXTIMERANDOMGEN));
            r = new Random();//para generar la semilla se tiene en cuenta la hora del pc de allí el Sleep
            semaphoreRandom = new Semaphore(1, 1);
        }
       
        public IdUnico(byte[] idUnico=null)
        {
            if (idUnico == null)
                idUnico = GenIdAuto();
            this.idUnico = idUnico;
        }

        public byte this[int index] { get => idUnico[index];  }

        IComparable IClauUnicaPerObjecte.Clau => this;

        #region CompareTo
        int CompareTo(IdUnico other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = (int)idUnico.CompareTo(other.idUnico);
            }
            else compareTo = (int)Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        int IComparable<IdUnico>.CompareTo(IdUnico other)
        {
            return CompareTo(other);
        }
        
        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as IdUnico);
        }
        #endregion

        private byte[] GenIdAuto()
        {
            byte[] id = new byte[LENGHT];
            semaphoreRandom.WaitOne();
            r.NextBytes(id);
            semaphoreRandom.Release();
            id.SetArray(0, Serializar.GetBytes(DateTime.Now.Ticks));
            return id;
        }
        public byte[] GetId()
        {
            return (byte[])idUnico.Clone();
        }
    }
}
