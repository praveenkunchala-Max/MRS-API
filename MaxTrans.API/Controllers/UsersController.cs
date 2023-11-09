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
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly JWTSetting setting;
        private readonly IRefreshTokenGenerator tokenGenerator;
        public UsersController(IOptions<JWTSetting> options, IUserService userService)
        {
            _userService = userService;
            setting = options.Value;
            
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody] AuthenticateModel model)
        //{
        //    var user = _userService.Authenticate(model.Username, model.Password);

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(user);
        //}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateGK([FromBody] AuthenticateModel model)
        {
            var user = _userService.AuthenticateGK(model.Username, model.Password, "maxtrans");

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        //[Authorize(Roles = Role.Admin)]
        [AllowAnonymous]
        [HttpPost("manageobject")]
        public IActionResult ManageObject(AppSystemObject obj)
        {
            using (var commonBL = new CommonBL(new ObjectDataConnection()))
            {
                return ObjectResponseHandler.GetObjectResponse(commonBL.ManageObject(obj, "nritdp"));
            }
        }

        //[Authorize(Roles = Role.Admin)]
        //[HttpPost("manageobject")]
        //public IActionResult ManageObject(AugadhSecuritySystemObject obj)
        //{
        //    using (var commonBL = new CommonBL(new ObjectDataConnection()))
        //    {
        //        return ObjectResponseHandler.GetObjectResponse(commonBL.ManageAugadhSecurityMonitoringObjects(obj, "monitoring"));
        //    }
        //}

        //[Authorize(Roles = GKRole.Admin + "," + GKRole.Manager + "," + GKRole.Customer + "," + GKRole.Mechanic + "," + GKRole.Seller + "," + GKRole.Retailer)]
        //[HttpPost("manageobjectgk")]
        //public IActionResult ManageObjectGK(AugadhSecuritySystemObject obj)
        //{
        //    using (var commonBL = new CommonBL(new ObjectDataConnection()))
        //    {
        //        return ObjectResponseHandler.GetObjectResponse(commonBL.ManageGKObjects(obj, "gk"));
        //    }
        //}

    }
}
