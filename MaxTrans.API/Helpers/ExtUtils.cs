using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Augadh.SecurityMonitoring.API.Helpers
{
    public static class ExtUtils
    {
        public static string ToComma<T,
        TU>(this IEnumerable<T> source, Func<T, TU> func)
        {
            return string.Join(", ",
            source.Select(s => func(s).ToString()).ToArray());
        }
    }
}
