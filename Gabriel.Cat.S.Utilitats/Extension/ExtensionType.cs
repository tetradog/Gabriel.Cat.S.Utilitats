using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Gabriel.Cat.S.Utilitats;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionType
    {
        public static bool IsNullableType(this Type type)
        {
            return type.IsClass||type.IsInterface||type.IsGenericType && type.GetGenericTypeDefinition()==typeof(Nullable<>);
        }
        public static bool IsStruct(this Type type)
        { return !type.IsNullableType(); }
        public static bool ImplementInterficie(this Type type, Type interficieType)
        {
            if (interficieType == null)
                throw new ArgumentNullException();
            if (!interficieType.IsInterface)
                throw new ArgumentException("Se esperaba una interficie");

            bool implemented=false;
            Type[] interficiesTipo= type.GetInterfaces();

            for (int i = 0; i < interficiesTipo.Length && !implemented; i++)
                implemented = Equals(interficiesTipo[i], interficieType);


            return implemented;
        }
        public static IList<PropiedadTipo> GetPropiedadesTipos(this Type tipo)
        {
            List<PropiedadTipo> lstPropiedades=new List<PropiedadTipo>();
            foreach (PropertyInfo propertyInfo in tipo.GetRuntimeProperties())
            {
                lstPropiedades.Add(new PropiedadTipo(propertyInfo));
            }
            return lstPropiedades;
        }
    }
}
