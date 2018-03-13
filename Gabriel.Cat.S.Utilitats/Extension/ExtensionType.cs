using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
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
    }
}
