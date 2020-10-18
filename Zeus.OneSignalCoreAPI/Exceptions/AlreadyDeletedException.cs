using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeus.OneSignalCoreAPI.Exceptions
{
    public class AlreadyDeletedException : Exception
    {
        public AlreadyDeletedException(string message) : base(message)
        {
        }

        public AlreadyDeletedException() : base()
        {

        }
    }
}
