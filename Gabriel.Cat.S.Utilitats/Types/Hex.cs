using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    /// <summary>
    /// Description of Hex.Por acabar...se tienen que hacer las operaciones...son muy chungas...
    /// de momento se limita a convertir y operar...en el futuro no se convierte asi pueden ser numero gigantes
    /// </summary>
    public struct Hex : IEquatable<Hex>, IComparable, IComparable<Hex>
    {
        static LlistaOrdenada<char, char> diccionarioCaracteresValidos;
        public static readonly string[] caracteresHex = new string[]{
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "a",
                "b",
                "c",
                "d",
                "e",
                "f",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F"
            };
        static readonly int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
            };
        string numberHex;
        static Hex()
        {
            diccionarioCaracteresValidos = new LlistaOrdenada<char, char>();
            for (int i = 0; i < caracteresHex.Length; i++)
                diccionarioCaracteresValidos.Add(caracteresHex[i][0], caracteresHex[i][0]);
        }


        public Hex(string numero)
        {
            if (!ValidaString(numero))
                throw new Exception("la string contiene caracteres no HEX");
            this.numberHex = numero;
        }
        // this is just an example member, replace it with your own struct members!
        public string Number
        {
            get
            {
                return numberHex;
            }
            private set
            {
                numberHex = value;
            }
        }
        /// <summary>
        /// Concatena 0x al numero
        /// </summary>
        public string ByteString
        {
            get
            {
                return "0x" + Number;
            }
        }


        #region Equals and GetHashCode implementation
        // The code in this region is useful if you want to use this structure in collections.
        // If you don't need it, you can just remove the region and the ": IEquatable<Hex>" declaration.

        public override bool Equals(object obj)
        {
            bool isEquals;
            Hex other;
            try
            {
                other = (Hex)obj;
                isEquals = Equals(other); // use Equals method below
            }
            catch
            {
                isEquals = false;
            }
            return isEquals;
        }

        public bool Equals(Hex other)
        {
            string thisNumber = QuitaCerosInutiles(this.numberHex), otherNumber = QuitaCerosInutiles(other.numberHex);
            // add comparisions for all members here
            return thisNumber.Equals(otherNumber);
        }
        public override string ToString()
        {
            return numberHex;
        }

        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            return numberHex.GetHashCode();
        }

        public static bool operator ==(Hex left, Hex right)
        {
            return ((long)left) == ((long)right);
        }

        public static bool operator !=(Hex left, Hex right)
        {
            return !(left == right);
        }
        public static Hex operator +(Hex left, Hex right)
        {
            long result = left;
            result += (long)right;
            return result;
        }
        public static Hex operator -(Hex left, Hex right)
        {
            long result = left;
            result -= (long)right;
            return result;
        }
        public static Hex operator ++(Hex number)
        {
            long result = number + 1;

            return result;
        }
        char SigienteCaracter(char caracter)
        {
            char caracterNext = '0';
            switch (caracter)
            {
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                    caracterNext = (char)(((int)caracter) + 1);
                    break;
                case 'f':
                    caracterNext = '0';
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                    caracterNext = (Convert.ToInt32(caracter) + 1).ToString()[0];
                    break;
                case '9':
                    caracterNext = 'a';
                    break;

            }
            return caracterNext;
        }
        public static Hex operator --(Hex number)
        {
            long result = number - 1;
            return result;
        }
        char AnteriorCaracter(char caracter)
        {
            char caracterNext = '0';
            switch (caracter)
            {

                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                    caracterNext = (char)(((int)caracter) - 1);
                    break;
                case 'a':
                    caracterNext = '9';
                    break;


                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    caracterNext = (Convert.ToInt32(caracter) - 1).ToString()[0];
                    break;
                case '0':
                    caracterNext = 'f';
                    break;

            }
            return caracterNext;
        }
        public static Hex operator *(Hex left, Hex right)
        {
            long result = left;
            result *= (long)right;
            return result;
        }
        public static Hex operator /(Hex left, Hex right)
        {
            long result = left;
            result /= (long)right;
            return result;
        }
        #endregion



        public static implicit operator Hex(string numero)
        {
            const int BASEHEX = 16;
            return (Hex)Convert.ToUInt64(numero, BASEHEX);
        }
        public static implicit operator Hex(int numero)
        {
            return (Hex)Serializar.GetBytes(numero).InvertirClone();
        }
        public static implicit operator Hex(byte numero)
        {
            return (Hex)Serializar.GetBytes(numero).InvertirClone();
        }
        public static explicit operator Hex(byte[] numero)
        {
            //sacado de internet
            List<char> bytesHex = new List<char>();
            byte byteHaciendose;

            for (int bx = 0, cx = 0; bx < numero.Length; ++bx, ++cx)
            {
                byteHaciendose = ((byte)(numero[bx] >> 4));
                if (byteHaciendose > 9)
                    bytesHex.Add((char)(byteHaciendose - 10 + 'A'));
                else
                    bytesHex.Add((char)(byteHaciendose + '0'));


                byteHaciendose = ((byte)(numero[bx] & 0x0F));
                bytesHex.Add((char)(byteHaciendose > 9 ? byteHaciendose - 10 + 'A' : byteHaciendose + '0'));
            }

            return new string(bytesHex.ToArray());
        }
        public static implicit operator Hex(uint numero)
        {
            return (Hex)Serializar.GetBytes(numero).InvertirClone();
        }

        public static implicit operator Hex(long numero)
        {
            return new Hex(QuitaCerosInutiles(numero.ToString("X4")));
        }
        public static implicit operator Hex(ulong numero)
        {
            return new Hex(QuitaCerosInutiles(numero.ToString("X4")));
        }
        static string QuitaCerosInutiles(string numero)
        {
            StringBuilder num = new StringBuilder(numero);
            while (num.Length > 0 && num[0] == '0') num.Remove(0, 1);
            if (num.Length == 0)
                num = new StringBuilder("0");
            return num.ToString();
        }
        public static implicit operator string(Hex numero)
        {
            return numero.numberHex;
        }
        public static explicit operator int(Hex numero)
        {
            const int LENGHT = 4;
            return Serializar.ToInt(DameArray(numero, LENGHT));
        }
        public static explicit operator uint(Hex numero)
        {
            const int LENGHT = 4;
            return Serializar.ToUInt(DameArray(numero, LENGHT));
        }
        static byte[] DameArray(byte[] bytes, int lenght)
        {
            bytes.Invertir();
            if (bytes.Length < lenght)
                bytes = bytes.AddArray( new byte[lenght - bytes.Length]);
            return bytes;
        }
        public static explicit operator ushort(Hex numero)
        {
            const int LENGHT = 2;
            return Serializar.ToUShort(DameArray(numero, LENGHT));
        }
        public static explicit operator short(Hex numero)
        {
            const int LENGHT = 2;
            return Serializar.ToShort(DameArray(numero, LENGHT));
        }
        public static explicit operator byte(Hex numero)
        {
            const int LENGHT = 1;
            return (DameArray(numero, LENGHT))[0];
        }
        public static implicit operator long(Hex numero)
        {
            const int LENGHT = 8;
            return Serializar.ToLong(DameArray(numero, LENGHT));
        }
        public static implicit operator ulong(Hex numero)
        {
            const int LENGHT = 8;
            return Serializar.ToULong(DameArray(numero, LENGHT));
        }
        public static implicit operator byte[] (Hex numero)
        {//sacado de internet

            StringBuilder numeroString = new StringBuilder(numero.ToString());
            byte[] bytes;
            if (numeroString.Length % 2 != 0)
                numeroString.Insert(0, '0');
            bytes = new byte[numeroString.Length / 2];


            for (int x = 0, i = 0; i < numeroString.Length; i += 2, x += 1)
            {
                bytes[x] = (byte)(HexValue[Char.ToUpper(numeroString[i + 0]) - '0'] << 4 |
                HexValue[Char.ToUpper(numeroString[i + 1]) - '0']);
            }


            return bytes;
        }
        public static bool ValidaString(string stringHex)
        {
            bool valida = true;
            for (int i = 0; i < stringHex.Length && valida; i++)
            {
                valida = diccionarioCaracteresValidos.ContainsKey(stringHex[i]);
            }
            return valida;

        }

        public int CompareTo(object obj)
        {
            int compareTo;
            if (obj is Hex)
                compareTo = CompareTo((Hex)obj);
            else compareTo = -1;
            return compareTo;
        }

        public int CompareTo(Hex other)
        {
            return numberHex.CompareTo(other.numberHex);
        }
    }
}