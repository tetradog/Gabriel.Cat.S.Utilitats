using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Gabriel.Cat.S.Utilitats;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionType
    {
        public static bool ImplementInterficie(this Type type, Type interficieType)
        {
            bool implemented=false;
            IEnumerator<Type> interficiesTipo = type.GetTypeInfo().ImplementedInterfaces.GetEnumerator();
            while (!interficiesTipo.MoveNext() && !implemented)
                implemented = interficiesTipo.Current.Equals(interficiesTipo);
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
