using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Gabriel.Cat.S.Utilitats
{
    public class PropiedadTipo:IComparable<PropiedadTipo>
    {
        AtributoOrden orden;
        string nombre;
        IList<Attribute> atributos;
        Type tipo;
        UsoPropiedad uso;
        public PropiedadTipo(string nombre, Type tipo, IList<Attribute> atributos, UsoPropiedad uso)
        {
            this.nombre = nombre;
            this.atributos = atributos;
            this.uso = uso;
            this.tipo = tipo;
        }

        public PropiedadTipo(PropertyInfo campoTipo) : this(campoTipo.Name, campoTipo.PropertyType, campoTipo.GetAttributes(), campoTipo.GetPropertyUsage())
        {

        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
        }

        public IList<System.Attribute> Atributos
        {
            get
            {
                return atributos;
            }
        }

        public UsoPropiedad Uso
        {
            get
            {
                return uso;
            }
        }

        public Type Tipo
        {
            get
            {
                return tipo;
            }
        }
        public AtributoOrden Orden
        {
            get
            {
                if(Equals(orden,default(AtributoOrden)))
                {
                    orden = Atributos.Filtra((atributo) => atributo is AtributoOrden).FirstOrDefault() as AtributoOrden;
                }
                return orden;
            }
        }

        int IComparable<PropiedadTipo>.CompareTo(PropiedadTipo other)
        {
            int compareTo;
            if (other != default)
            {
                if (Equals(orden,default(AtributoOrden)) && !Equals(other.Orden, default(AtributoOrden)))
                    compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
                else if (!Equals(Orden, default(AtributoOrden)) && Equals(other.Orden, default(AtributoOrden)))
                    compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Superior;
                else compareTo = Orden.CompareTo(other.Orden);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public override string ToString() => Nombre;
    }
    public class AtributoOrden:System.Attribute,IComparable<AtributoOrden>
    {
        public AtributoOrden(int orden)
        {
            Orden = orden;
        }

        public int Orden { get; private set; }

        public int CompareTo(AtributoOrden other)
        {
            int compareTo;
            if (!Equals(other, default(AtributoOrden)))
                compareTo = Orden.CompareTo(other.Orden);
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
    }
}
