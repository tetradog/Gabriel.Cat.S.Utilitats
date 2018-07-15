using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class ResourceFile
    {
        Type claseRecurso;
        string nombreRecurso;
        byte[] file;

        public ResourceFile(Type claseRecurso, string nombreRecurso)
        {
            this.claseRecurso = claseRecurso;
            this.nombreRecurso = nombreRecurso;
        }

        public byte[] File
        {
            get
            {
                if (file == null)
                    file = (byte[])claseRecurso.GetProperty(nombreRecurso).GetValue(null);
                return file;
            }
        }
        public virtual void Dispose()
        {
            file = null;
        }
    }
    public class ResourceImage : ResourceFile
    {
        Bitmap img;
        public ResourceImage(Type claseRecurso, string nombreRecurso) : base(claseRecurso, nombreRecurso)
        {
        }
        public Bitmap Image
        {
            get
            {
                if (img == null)
                {
                    img = (Bitmap)Bitmap.FromStream(new System.IO.MemoryStream(File));
                    base.Dispose();
                }
                return img;
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            img = null;
        }
    }
}
