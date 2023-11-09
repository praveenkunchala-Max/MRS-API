using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MaxTrans.API.Models;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.Entities;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Microsoft.AspNetCore.Authorization;
using MaxTrans.Common.Entities;
using System.Data;
using Microsoft.Extensions.Logging;
using Augadh.SecurityMonitoring.API;

namespace MaxTrans.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly Learn_DBContext context;
        private readonly JWTSetting setting;
        private IRefreshTokenGenerator tokenGenerator;

        private readonly ILogger<UserController> _logger;

        //Learn_DBContext learn_DB, 
        public UserController(IOptions<JWTSetting> options, ILogger<UserController> logger)
        {
            setting = options.Value;
            _logger = logger;
        }
        [NonAction]
        public TokenResponse Authenticate(string username, Claim[] claims)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
            var tokenhandler = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                );
            tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
            tokenResponse.RefreshToken = tokenGenerator.GenerateToken(username);

            return tokenResponse;
        }

        [AllowAnonymous]
        [HttpPost("manageauth")]
        //public IActionResult Authenticate([FromBody] usercred user)
        public IActionResult Authenticate(AppSystemObject obj)
        {
            TokenResponse tokenResponse = new TokenResponse();
            tokenResponse.JWTToken = "NO";
            tokenResponse.RefreshToken = "NO";
            var commonBL = new CommonBL(new ObjectDataConnection());
            //AppSystemObject obj = new AppSystemObject{  Action="", ActionType = "", ObjectName = "", ParamsJson = ""};
            var _pp = commonBL.ManageAuth(obj, "nritdp");



            //var _user = context.TblUser.FirstOrDefault(o => o.Userid == user.username && o.Password == user.password);
            if (_pp == null)
                return Unauthorized();
            var _user = _pp;
            if (_user.UserName != null)
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, _user.UserName),
                            new Claim(ClaimTypes.Role, _user.Role)

                        }
                    ),
                    Expires = DateTime.Now.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokenDescriptor);
                string finaltoken = tokenhandler.WriteToken(token);

                tokenResponse.JWTToken = finaltoken;

                tokenGenerator = new RefreshTokenGenerator();
                tokenResponse.RefreshToken = tokenGenerator.GenerateToken(_user.UserName);
                tokenResponse.UserName = _user.UserName;
                tokenResponse.FirstName = _user.FirstName;
                tokenResponse.LastName = _user.LastName;
                tokenResponse.Email = _user.Email;
                tokenResponse.Role = _user.Role;
                tokenResponse.UserId = _user.UserId;

                //bool sendmail = new EmailController().SendEmail("prasad.node@gmail.com", "test", "body test");
            }
            return Ok(tokenResponse);
        }


        [HttpPost("getuserrolebyid")]
        public async Task<ActionResult> UserRoleEdit(AppSystemObject obj)
        {
            var commonBL = new CommonBL(new ObjectDataConnection());
            var _pp = commonBL.ManageRoles(obj, "nritdp");
            if (_pp == null)
                return Unauthorized();

            var userrole = _pp;
            return Ok(userrole);
        }

        [Route("Refresh")]
        [HttpPost]
        public IActionResult Refresh([FromBody] TokenResponse token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenhandler.ValidateToken(token.JWTToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.securitykey)),
                ValidateIssuer = false,
                ValidateAudience = false

            }, out securityToken);

            var _token = securityToken as JwtSecurityToken;

            if (_token != null && !_token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return Unauthorized();
            }
            var username = principal.Identity.Name;
            //var _reftable = context.TblRefreshtoken.FirstOrDefault(o => o.UserId == username && o.RefreshToken == token.RefreshToken);
            var commonBL = new CommonBL(new ObjectDataConnection());
            AppSystemObject obj = new AppSystemObject { Action = "", ActionType = "", ObjectName = "", ParamsJson = "" };
            var _reftable = commonBL.ManageObject(obj, "nritdp");
            if (_reftable == null)
            {
                return Unauthorized();
            }
            TokenResponse _result = Authenticate(username, principal.Claims.ToArray());
            return Ok(_result);
        }

    }
}