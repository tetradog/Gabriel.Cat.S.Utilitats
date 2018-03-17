using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Gabriel.Cat.S.Utilitats
{
    public class ObjectAutoId : IClauUnicaPerObjecte, IComparable<ObjectAutoId>
    {
        static Semaphore semaphorId = new Semaphore(1, 1);
        static long genA = 0;
        static long genB = 0;

        long partA, partB;
        string idUnic;
        public ObjectAutoId()
        {
            semaphorId.WaitOne();
            if (genB == long.MaxValue)
            {
                genA++;
                genB = 0;
            }
            partB = genB++;
            partA = genA;
            semaphorId.Release();
        }
        public string IdAuto
        {
            get
            {
                if (idUnic == null)
                {
                    idUnic = partA.ToString().PadLeft(long.MaxValue.ToString().Length, '0') + partB.ToString().PadLeft(long.MaxValue.ToString().Length, '0');
                }
                return idUnic;

            }
        }

        IComparable IClauUnicaPerObjecte.Clau
        {
            get
            {
                return IdAuto;
            }
        }

        int IComparable<ObjectAutoId>.CompareTo(ObjectAutoId other)
        {
            int compareTo = other == null ? (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior : partA.CompareTo(other.partA);
            if ((int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals == compareTo)
                compareTo = partB.CompareTo(other.partB);
            return compareTo;
        }
    }
}