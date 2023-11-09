using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Augadh.SecurityMonitoring.Common.Helpers
{
    public static class StringExtensions
    {
        [CanBeNull]
        public static string FirstCharToLowerCase([CanBeNull] this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLower(str[0]) + str.Substring(1);
        }
    }
}
