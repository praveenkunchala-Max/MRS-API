using System;
using System.Collections.Generic;
using System.Text;

namespace Augadh.SecurityMonitoring.Common.BusinessLayer
{
    public class Common : DisposeLogic
    {
        public static string PrepareXML(List<int> values, string root, string child, string subChild)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<" + root + ">");
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append("<" + child + ">");
                sb.Append("<" + subChild + ">");
                sb.Append(values[i].ToString());
                sb.Append("</" + subChild + ">");
                sb.Append("</" + child + ">");
            }
            sb.Append("</" + root + ">");
            return sb.ToString();
        }
    }
}
