using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class Propiedad
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
    }
}
