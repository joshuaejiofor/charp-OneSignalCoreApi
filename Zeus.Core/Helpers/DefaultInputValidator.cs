using System;
using System.Text.RegularExpressions;
using Zeus.Core.Helpers.Interface;

namespace Zeus.Core.Helpers
{
    public class DefaultInputValidator : IInputValidator
    {
        private const string _alphabetsNumbersAndDashesOnly = @"^[A-Za-z0-9-]+$";
        public bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, _alphabetsNumbersAndDashesOnly);
        }

        public bool IsValid(int value)
        {
            if (value <= 0) return false;

            return true;
        }

    }
}
