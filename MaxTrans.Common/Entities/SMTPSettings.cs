using System;
using System.Collections.Generic;
using System.Text;

namespace MaxTrans.Common.Entities
{
    public class SMTPSettings
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string SMTPPort { get; set; }
        public string Password { get; set; }
    }
}
