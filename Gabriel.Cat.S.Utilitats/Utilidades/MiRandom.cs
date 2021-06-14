using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public static class MiRandom
    {
        static Random llavor = new Random();
        public static int Next(int minValueInclude, int maxValueExclude)
        {
            return llavor.Next(minValueInclude, maxValueExclude);
        }
        public static int Next()
        {
            return llavor.Next();
        }
        public static int Next(int maxValue)
        {
            return llavor.Next(maxValue);
        }
        public static byte[] NextBytes(int length){
         byte[] randomArray=new byte[length];
            llavor.NextBytes(randomArray);
            return randomArray;
        }
        public static void SetRandom([NotNull] Random r) {

                llavor = r; 
        }
    }
}
