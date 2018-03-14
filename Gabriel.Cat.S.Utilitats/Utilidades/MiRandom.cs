using System;
using System.Collections.Generic;
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
    }
}
