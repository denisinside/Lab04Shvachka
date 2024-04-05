using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04Shvachka.Exceptions
{
    class AgeError : Exception
    {
        public AgeError() { }
        public AgeError(string message) : base(message)
        {
        }
    }
}
