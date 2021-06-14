using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Gabriel.Cat.S.Utilitats;
using System.Diagnostics.CodeAnalysis;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionType
    {
        public static object GetObj([NotNull] this Type type, [NotNull] params object[] partes)
        {
            return Activator.CreateInstance(type, partes);
        }
        public static Type SetTypes([NotNull] this Type type, [NotNull] params Type[] types)
        {
            return type.MakeGenericType(types);
        }
        public static Type GetArrayType([NotNull] this Type tipoObj)
        {
            return tipoObj.GetElementType();
        }
        public static bool IsNullableType([NotNull] this Type type)
        {
            return type.IsClass || type.IsInterface || type.IsGenericType && Equals(type.GetGenericTypeDefinition(), typeof(Nullable<>));
        }
        public static bool IsStruct([NotNull] this Type type)
        { return !type.IsNullableType(); }
        public static bool ImplementInterficie([NotNull] this Type type, [NotNull] Type interficieType)
        {
            if (interficieType == null)
                throw new ArgumentNullException();
            if (!interficieType.IsInterface)
                throw new ArgumentException("Se esperaba una interficie");

            bool implemented = false;
            Type[] interficiesTipo = type.GetInterfaces();

            for (int i = 0; i < interficiesTipo.Length && !implemented; i++)
                implemented = Equals(interficiesTipo[i].IsGenericType ? interficiesTipo[i].GetGenericTypeDefinition() : interficiesTipo[i], interficieType);


            return implemented;
        }
        public static IList<PropiedadTipo> GetPropiedadesTipo([NotNull] this Type tipo)
        {
            List<PropiedadTipo> lstPropiedades = new List<PropiedadTipo>();
            foreach (PropertyInfo propertyInfo in tipo.GetRuntimeProperties())
            {
                lstPropiedades.Add(new PropiedadTipo(propertyInfo));
            }
            return lstPropiedades;
        }
    }
}
