using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public delegate void MetodoUnsafeArray<T>([NotNull]UnsafeArray<T> unsafeArray) where T:unmanaged;
    public unsafe class UnsafeArray <T> where T:unmanaged
    {
        public T* PtrArray;
        public readonly T* PtrArrayInicial;
        public readonly long Length;
        public UnsafeArray(T* ptrArrayIncial, long length)
        {
            Length = length;
            PtrArrayInicial = ptrArrayIncial;
            PtrArray = ptrArrayIncial;
        }
        public void ResetPosicion()
        {
            PtrArray = PtrArrayInicial;
        }
        public long Posicion
        {
            get
            {
                return PtrArray - PtrArrayInicial;
            }
            set
            {
                PtrArray = PtrArrayInicial + Posicion;
            }
        }
        public T this[long posicion]
        {
            get { return PtrArrayInicial[posicion]; }
            set { PtrArrayInicial[posicion] = value; }
        }
        public T* PtrArrayFin
        {
            get { return PtrArrayInicial + Length - 1; }
        }
        public static unsafe explicit operator T* ([NotNull] UnsafeArray<T> unsafeArray)
        {
            return unsafeArray.PtrArray;
        }
        public static void Usar<T>([NotNull] T[] array, [NotNull] MetodoUnsafeArray<T> metodo) where T:unmanaged
        {
            Exception exAux = default;
            unsafe
            {
                fixed (T* ptrArray = array)
                {
                    try
                    {
                        metodo(new UnsafeArray<T>(ptrArray, array.Length));
                    }
                    catch (Exception ex)
                    {
                        exAux = ex;
                    }
                }
                //lanzo la excepcion en un contexto seguro :D asi no hay problemas con los pointers
                if (exAux != default)
                    throw exAux;
            }
        }

    }
}