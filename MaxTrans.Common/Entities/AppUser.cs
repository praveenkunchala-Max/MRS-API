﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MaxTrans.Common.Entities
{
    public class AppUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? JobId { get; set; }
    }
}
