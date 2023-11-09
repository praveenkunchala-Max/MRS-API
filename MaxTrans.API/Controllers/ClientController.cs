using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Augadh.SecurityMonitoring.API.Services;
using Augadh.SecurityMonitoring.API.Models;
using Microsoft.AspNetCore.Http;
using System;
using Augadh.SecurityMonitoring.API.Entities;
using Augadh.SecurityMonitoring.Common.Entities;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Augadh.SecurityMonitoring.API.Handlers;
using MaxTrans.API;
using MaxTrans.API.Models;
using Microsoft.Extensions.Options;

namespace Augadh.SecurityMonitoring.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private IUserService _userService;
        private readonly JWTSetting setting;
        
        public ClientController(IOptions<JWTSetting> options, IUserService userService)
        {
            _userService = userService;
            setting = options.Value;
            
        }

       
        [AllowAnonymous]
        [HttpPost("addclient")]
        public IActionResult AddClient(AppSystemObject obj)
        {
            using (var clientBL = new ClientBL(new ObjectDataConnection()))
            {
                return ObjectResponseHandler.GetObjectResponse(clientBL.AddClient(obj));
            }
        }
    }
}
