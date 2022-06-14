using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.User.Models;
using SP.User.Models.RequestParams;
using SP.User.Service;

namespace SP.EpiasReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly Serilog.ILogger _logger;
        private readonly IUserService _repository;

        public UserController(Serilog.ILogger logger, IUserService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<IAuthUserData> Register([FromBody] UserRegisterRequestParams r)
        {
            var user = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<IAuthUserData> Login([FromBody] UserLoginRequestParams r)
        {
            var user = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("RefreshToken")]
        public ActionResult<IAuthUserData> RefreshToken([FromHeader] string authorization)
        {
            var userData = _repository.GetUserDataByToken(authorization);
            _logger.Information("{action}: {email}", "REFRESH TOKEN", userData.Email);
            return Ok(userData);
        }

        [Authorize(AdminRestricted = true)]
        [HttpPost("UpdateRoles")]
        public ActionResult<String> UpdateRoles([FromBody] UpdateAccountRolesRequestParams r)
        {
            _repository.UpdateAccountRoles(r);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPost("UpdateIsActive")]
        public ActionResult<String> UpdateIsActive([FromBody] UpdateAccountIsActiveRequestParams r)
        {
            _repository.UpdateIsActive(r);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpGet("All")]
        public ActionResult<IEnumerable<IAdminServicableUserData>> GetUsers()
        {
            return Ok(_repository.GetUsers());
        }
    }
}
