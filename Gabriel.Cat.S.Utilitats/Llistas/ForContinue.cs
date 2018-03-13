﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public struct ForContinue
    {
        int incrementOrDecrement;
        bool finished;
        public ForContinue(int incrementOrDecrement = 1, bool finished = false)
        {
            this.incrementOrDecrement = incrementOrDecrement;
            this.finished = finished;
        }
        public int IncrementOrDecrement
        {
            get
            {
                return incrementOrDecrement;
            }

            private set
            {
                incrementOrDecrement = value;
            }
        }

        public bool Finished
        {
            get
            {
                return finished;
            }

            private set
            {
                finished = value;
            }
        }
    }
}