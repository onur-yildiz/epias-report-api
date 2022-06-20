using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.Users.Models;
using SP.Users.Models.RequestBody;
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
        public IEnumerable<IUserBase<string>> GetUsers()
        {
            return _repository.GetAllUsers();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IAuthUser Register([FromBody] UserRegisterRequestBody r)
        {
            var user = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return user;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IAuthUser Login([FromBody] UserLoginRequestBody r)
        {
            var user = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return user;
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public IAuthUser RefreshToken([FromHeader] string authorization)
        {
            var userData = _repository.RefreshToken(authorization);
            _logger.Information("{action}: {email}", "REFRESH TOKEN", userData.Email);
            return userData;
        }

        [Authorize]
        [HttpPost("create-api-key")]
        public string CreateApiKey([FromHeader] string authorization)
        {
            return _repository.CreateApiKey(authorization);
        }

        [Authorize]
        [HttpDelete("api-keys")]
        public ActionResult DeleteApiKey([FromHeader] string authorization, [FromBody] DeleteApiKeyRequestBody r)
        {
            _repository.DeleteApiKey(r.ApiKey, authorization);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/roles")]
        public ActionResult UpdateRoles(string userId, [FromBody] UpdateAccountRolesRequestBody r)
        {
            _repository.UpdateRoles(userId, r);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/is-active")]
        public ActionResult UpdateIsActive(string userId, [FromBody] UpdateAccountIsActiveRequestBody r)
        {
            _repository.UpdateIsActive(userId, r);
            return Ok();
        }

    }
}
