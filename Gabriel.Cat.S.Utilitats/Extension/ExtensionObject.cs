using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionObject
    {
        public static IList<Propiedad> GetPropiedades(this object obj)
        {
           List<Propiedad> propiedades=new List<Propiedad>();
            foreach (PropertyInfo propertie in obj.GetType().GetRuntimeProperties())
            {
                propiedades.Add(new Propiedad(new PropiedadTipo(propertie), obj));
            }
            return propiedades;
        }
        public static void SetProperty(this object obj, string nameProperty, object value)
        {
            obj.GetType().GetRuntimeProperty(nameProperty).SetValue(obj, value);
        }
        public static object GetProperty(this object obj, string nameProperty)
        {
            return obj.GetType().GetRuntimeProperty(nameProperty).GetValue(obj);
        }
    }
}
