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
    public class JobsController : ControllerBase
    {
        private IUserService _userService;
        private readonly JWTSetting setting;
        private readonly IRefreshTokenGenerator tokenGenerator;
        public JobsController(IOptions<JWTSetting> options, IUserService userService)
        {
            _userService = userService;
            setting = options.Value;
            
        }

       
        [AllowAnonymous]
        [HttpPost("addjob")]
        public IActionResult AddJob(AppSystemObject obj)
        {
            using (var jobsBL = new JobsBL(new ObjectDataConnection()))
            {
                return ObjectResponseHandler.GetObjectResponse(jobsBL.AddJob(obj));
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
