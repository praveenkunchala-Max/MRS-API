using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Augadh.SecurityMonitoring.API.Services;
using Augadh.SecurityMonitoring.API.Entities;
using Augadh.SecurityMonitoring.API.Models;

namespace Augadh.SecurityMonitoring.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserAuthenticationController : ControllerBase    
    {
        private IUserService _userService;

        public UserAuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

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
    }
}
