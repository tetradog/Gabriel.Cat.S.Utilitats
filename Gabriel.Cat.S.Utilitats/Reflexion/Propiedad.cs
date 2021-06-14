using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class Propiedad:IComparable<Propiedad>
    {
        public Propiedad([NotNull] object obj, [NotNull] string nombre):this(new PropiedadTipo(obj.GetType().GetRuntimeProperty(nombre)),obj)
        {

        }
        public Propiedad([NotNull] PropiedadTipo info,[NotNull] object obj)
        {
            this.Info = info;
            this.Objeto = obj;
        }
        public PropiedadTipo Info { get; private set; }

        public object Objeto { get; private set; }
        public object Value
        {
            get
            {
                return Objeto.GetProperty(Info.Nombre);
            }
            set
            {
                Objeto.SetProperty(Info.Nombre, value);
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
