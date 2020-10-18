using System;
using System.Collections.Generic;
using System.Text;

namespace Zeus.Core.Helpers.Interface
{
    public interface IInputValidator
    {
        bool IsValid(string value);
        bool IsValid(int value);

    }
}
