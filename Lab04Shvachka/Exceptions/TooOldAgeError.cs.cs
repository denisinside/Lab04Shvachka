﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04Shvachka.Exceptions
{
    class TooOldAgeError : Exception
    {  
        public TooOldAgeError() { }
        public TooOldAgeError(string message) : base(message)
        {
        }
    }
}
