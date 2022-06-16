using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.Users.Models;
using SP.Users.Models.RequestParams;
using SP.Users.Service;

namespace SP.EpiasReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly Serilog.ILogger _logger;
        private readonly IUsersService _repository;

        public UsersController(Serilog.ILogger logger, IUsersService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public ActionResult<IEnumerable<IUserBase<string>>> GetUsers()
        {
            return Ok(_repository.GetAllUsers());
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<IAuthUser> Register([FromBody] UserRegisterRequestBody r)
        {
            var user = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<IAuthUser> Login([FromBody] UserLoginRequestBody r)
        {
            var user = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public ActionResult<IAuthUser> RefreshToken([FromHeader] string authorization)
        {
            var userData = _repository.RefreshToken(authorization);
            _logger.Information("{action}: {email}", "REFRESH TOKEN", userData.Email);
            return Ok(userData);
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/roles")]
        public ActionResult<String> UpdateRoles(string userId, [FromBody] UpdateAccountRolesRequestBody r)
        {
            _repository.UpdateRoles(userId, r);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/is-active")]
        public ActionResult<String> UpdateIsActive(string userId, [FromBody] UpdateAccountIsActiveRequestBody r)
        {
            _repository.UpdateIsActive(userId, r);
            return Ok();
        }

    }
}
