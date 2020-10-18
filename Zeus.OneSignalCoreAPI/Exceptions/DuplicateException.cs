using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeus.OneSignalCoreAPI.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(message)
        {
        }

        public DuplicateException() : base()
        {

        }
    }
}
