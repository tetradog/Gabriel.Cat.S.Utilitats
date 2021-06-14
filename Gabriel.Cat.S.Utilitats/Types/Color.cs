using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gabriel.Cat.S.Utilitats.V2
{

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    /// <summary>
    /// Description of Color.
    /// </summary>
    public struct Color : IComparable
    {
        [FieldOffset(Pixel.A)]
        byte a;
        [FieldOffset(Pixel.R)]
        byte r;
        [FieldOffset(Pixel.G)]
        byte g;
        [FieldOffset(Pixel.B)]
        byte b;

        public Color(int argb = 0)
        {
            byte[] argbBytes = Serializar.GetBytes(argb);
            a = argbBytes[Pixel.A];
            r = argbBytes[Pixel.R];
            g = argbBytes[Pixel.G];
            b = argbBytes[Pixel.B];

        }

        public Color(byte a, byte r, byte g, byte b)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public byte Alfa
        {
            get
            {
                return A;
            }
            set
            {
                A = value;
            }
        }

        public byte Red
        {
            get
            {
                return R;
            }
            set
            {
                R = value;
            }
        }

        public byte Green
        {
            get
            {
                return G;
            }
            set
            {
                G = value;
            }
        }

        public byte Blue
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }

        public byte A { get => a; set => a = value; }
        public byte R { get => r; set => r = value; }
        public byte G { get => g; set => g = value; }
        public byte B { get => b; set => b = value; }

        public int ToArgb()
        {
            return Serializar.ToInt(new byte[] { A, R, G, B });
        }
        public int ToRgb()
        {
            return Serializar.ToInt(new byte[] { byte.MinValue, R, G, B });
        }

        #region IComparable implementation

        public int CompareTo(object obj)
        {
            Color other;
            int compareTo;
            try
            {
                other = (Color)obj;
                compareTo = ToArgb().CompareTo(other.ToArgb());
            }
            catch
            {
                compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;

            }
            return compareTo;
        }
        #endregion
        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A,color.R,color.G,color.B);
        }
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
    }


}

