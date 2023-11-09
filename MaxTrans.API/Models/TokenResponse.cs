using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxTrans.API.Models
{
    public class TokenResponse
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
    }
}
