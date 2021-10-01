using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public struct ForContinue
    {
        public ForContinue(int incrementOrDecrement = 1, bool finished = false)
        {
            IncrementOrDecrement = incrementOrDecrement;
            Finished = finished;
        }
        public int IncrementOrDecrement { get; private set; }

        public bool Finished { get; private set; }
    }
}