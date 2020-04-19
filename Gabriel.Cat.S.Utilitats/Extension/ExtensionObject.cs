using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionObject
    {
        public static Type GetPropertyType(this object obj, string property, bool ifNullableGetValue = true)
        {

            Type type = obj.GetType().GetProperty(property).PropertyType;

            if (ifNullableGetValue && type.Name.Contains("Nullable"))
                type = Nullable.GetUnderlyingType(type);

            return type;

        }
        public static bool IsNullableProperty(this object obj, string property)
        {
            Type type = obj.GetType().GetProperty(property).PropertyType;

            return type.Name.Contains("Nullable");
        }
   
        public static List<Propiedad> GetPropiedades(this object obj)
        {
           List<Propiedad> propiedades=new List<Propiedad>();
            foreach (PropertyInfo propertie in obj.GetType().GetRuntimeProperties())
            {
                propiedades.Add(new Propiedad(new PropiedadTipo(propertie),propertie.GetValue( obj)));
            }
            return propiedades;
        }
        public static void SetProperty(this object obj, string nameProperty, object value)
        {
            PropertyInfo property = obj.Property(nameProperty);
            if (value == default && !property.GetGetMethod().ReturnType.IsNullableType())
                throw new ArgumentNullException("El tipo no admite null como valor");
            property.SetValue(obj, value);
        }
        public static object GetProperty(this object obj, string nameProperty)
        {
            return obj.Property(nameProperty).GetValue(obj);
        }
        public static PropertyInfo Property(this object obj, string nameProperty)
        {
            return obj.GetType().GetRuntimeProperty(nameProperty);
        }
    }
}
