using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionBytes
    {
        public static void Save(this IEnumerable<Byte> array, DirectoryInfo dir, string nameWithExtension, Encoding encoding)
        {
            
            array.Save(dir.FullName + Path.DirectorySeparatorChar + nameWithExtension, encoding);
        }
        public static void Save(this IEnumerable<Byte> array, DirectoryInfo dir, string nameWithExtension)
        {
            array.Save(dir, nameWithExtension, Encoding.UTF8);
        }
        public static void Save(this IEnumerable<Byte> array, string path)
        {
            array.Save(path, Encoding.UTF8);
        }
        public static void Save(this IEnumerable<Byte> array, string path, Encoding encoding)
        {
            if (File.Exists(path))
                File.Delete(path);
            FileStream file = new FileStream(path, FileMode.Create);
            BinaryWriter bin = new BinaryWriter(file, encoding);
            try
            {
                bin.Write(array.ToArray());
            }
            finally
            {
                bin.Close();
                file.Close();
            }
        }
    }
}
