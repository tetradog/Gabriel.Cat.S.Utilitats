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
            UsoPropiedad uso = default;
            if (propiedad.CanRead)
                uso = UsoPropiedad.Get;
            if (propiedad.CanWrite)
                uso = (UsoPropiedad)((int)uso + (int)UsoPropiedad.Set);
            return uso;
        }
        public static IEnumerable<Attribute> GetAttributes(this PropertyInfo propiedad)
        {
            return propiedad.GetCustomAttributes(true).Casting<System.Attribute>();
        }
    }
}
