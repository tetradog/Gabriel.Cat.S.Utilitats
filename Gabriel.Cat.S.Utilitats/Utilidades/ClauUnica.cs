using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public interface IClauUnicaPerObjecte
    {
        IComparable Clau { get; }
    }
    public interface IDosClausUniquesPerObjecte : IClauUnicaPerObjecte
    {
        IComparable Clau2 { get; }
    }
    /// <summary>
    /// Sirve para usar clases y structs que son IComparable pero que no son IClauUnicaPerObjecte, hay conversiones implicitas a los tipos basicos
    /// </summary>
    public class ClauUnicaPerObjecte : IClauUnicaPerObjecte
    {
        public ClauUnicaPerObjecte([NotNull] IComparable clau)
        { Clau = clau; }
        public IComparable Clau { get; private set; }
        #region conversion
        public static implicit operator ClauUnicaPerObjecte(char clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(string clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(double clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(float clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(bool clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(int clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }


        public static implicit operator ClauUnicaPerObjecte(short clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(long clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(ulong clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(uint clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }
        public static implicit operator ClauUnicaPerObjecte(ushort clau)
        {
            return new ClauUnicaPerObjecte(clau);
        }


        #endregion

    }

}