using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class PropiedadTipo
    {
        string nombre;
        IList<System.Attribute> atributos;
        Type tipo;
        UsoPropiedad uso;
        public PropiedadTipo(string nombre, Type tipo, IList<System.Attribute> atributos, UsoPropiedad uso)
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
    }
}
