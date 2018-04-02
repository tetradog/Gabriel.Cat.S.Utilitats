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

            array.ToArray().Save(Path.Combine(dir.FullName, nameWithExtension), encoding);
        }
        public static void Save(this IEnumerable<Byte> array, DirectoryInfo dir, string nameWithExtension)
        {
            array.Save(dir, nameWithExtension, null);
        }
        public static void Save(this IEnumerable<Byte> array, string path)
        {
            array.ToArray().Save(path, null);
        }
        public static void Save(this IEnumerable<Byte> array, string path, Encoding encoding)
        {
            array.ToArray().Save(path, encoding);
        }
        public static void Save(this byte[] array, DirectoryInfo dir, string nameWithExtension, Encoding encoding)
        {
            
            array.Save(Path.Combine(dir.FullName, nameWithExtension), encoding);
        }
        public static void Save(this byte[] array, DirectoryInfo dir, string nameWithExtension)
        {
            array.Save(dir, nameWithExtension,null);
        }
        public static void Save(this byte[] array, string path)
        {
            array.Save(path, null);
        }
        public static void Save(this byte[] array, string path, Encoding encoding)
        {
            if (File.Exists(path))
                File.Delete(path);
            if (encoding == null)
                encoding = Encoding.UTF8;
            FileStream file = new FileStream(path, FileMode.Create);
            BinaryWriter bin = new BinaryWriter(file, encoding);
            try
            {
                bin.Write(array);
            }
            finally
            {
                bin.Close();
                file.Close();
            }
        }
    }
}
