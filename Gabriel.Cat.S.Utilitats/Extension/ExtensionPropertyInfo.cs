using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionPropertyInfo
    {
        public static object GetValue(this PropertyInfo properti, object obj)
        {
            return properti.GetValue(obj, null);
        }
        public static void SetValue(this PropertyInfo properti, object obj, object value)
        {
            properti.SetValue(obj, value, null);
        }
        public static UsoPropiedad GetPropertyUsage(this PropertyInfo propiedad)
        {
            UsoPropiedad uso = (UsoPropiedad)0;
            if (propiedad.CanRead)
                uso = UsoPropiedad.Get;
            if (propiedad.CanWrite)
                uso = (UsoPropiedad)((int)uso + (int)UsoPropiedad.Set);
            return uso;
        }
        public static IList<Attribute> GetAttributes(this PropertyInfo propiedad)
        {
            List<System.Attribute> atributos = new List<Attribute>();
            atributos.AddRange(propiedad.GetCustomAttributes(true).Casting<System.Attribute>());
            return atributos;
        }
    }
}
