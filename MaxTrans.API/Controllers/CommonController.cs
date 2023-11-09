using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Augadh.SecurityMonitoring.API.Handlers;
using Augadh.SecurityMonitoring.Common.DataLayer;
using Augadh.SecurityMonitoring.Common.BusinessLayer;
using Augadh.SecurityMonitoring.Common.Entities;

namespace Augadh.SecurityMonitoring.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {       
        [HttpPost]
        [Route("manageobject")]
        //[TokenAuthenticationFilter]
        public IActionResult ManageObject(AppSystemObject obj)
        {
            using (var commonBL = new CommonBL(new ObjectDataConnection()))
            {
                return ObjectResponseHandler.GetObjectResponse(commonBL.ManageObject(obj, "nritdp"));
            }
        }
    }
}
