using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class Propiedad:IComparable<Propiedad>
    {
        private PropiedadTipo info;
        private object objeto;
        public Propiedad(PropiedadTipo info, object obj)
        {
            this.info = info;
            this.objeto = obj;
        }
        public PropiedadTipo Info
        {
            get
            {
                return info;
            }


        }

        public object Objeto
        {
            get
            {
                return objeto;
            }

        }

        int IComparable<Propiedad>.CompareTo(Propiedad other)
        {
            int compareTo;
            if (other != default)
                compareTo = ((IComparable<PropiedadTipo>)Info).CompareTo(other.Info);
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
    }
}
