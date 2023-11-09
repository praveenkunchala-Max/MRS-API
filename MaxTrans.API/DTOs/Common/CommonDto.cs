using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Augadh.SecurityMonitoring.API.DTOs.Common
{
    public class CommonDto
    {
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
