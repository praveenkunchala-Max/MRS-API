using System;
using System.Collections.Generic;
using System.Text;

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
    public static class ManageStrings
    {
        public static bool CompareStrings(string stringToCompare, string stringToCompateWith, bool isCaseSensitive)
        {
            bool result = false;

            if (string.Compare(stringToCompare, stringToCompateWith, isCaseSensitive) == 1)
                result = true;

            return result;
        }
    }
}
