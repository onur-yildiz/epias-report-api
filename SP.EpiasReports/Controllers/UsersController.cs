using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.EpiasReports.Swagger;
using SP.Users.Models;
using SP.Users.Models.RequestBody;
using SP.Users.Models.RequestParams;
using SP.Users.Service;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Serves all users' info.
        /// </summary>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public IEnumerable<IUserBase<string>> GetUsers()
        {
            return _repository.GetAllUsers();
        }

        /// <summary>
        /// Register a new account.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IAuthUser Register([FromBody] UserRegisterRequestBody r)
        {
            var user = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return user;
        }

        /// <summary>
        /// Log into an account.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IAuthUser Login([FromBody] UserLoginRequestBody r)
        {
            var user = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return user;
        }

        /// <summary>
        /// Refresh auth token with a new one.
        /// </summary>
        /// <param name="authorization">Auth token</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public IAuthUser RefreshToken([FromHeader][Required] string authorization)
        {
            var userData = _repository.RefreshToken(authorization);
            _logger.Information("{action}: {email}", "REFRESH TOKEN", userData.Email);
            return userData;
        }

        /// <summary>
        /// Create a new API key for the account.
        /// </summary>
        /// <param name="authorization">Auth token</param>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{userId}/api-keys/create")]
        public string CreateApiKey(string userId, [FromHeader][Required] string authorization)
        {
            return _repository.CreateApiKey(authorization, userId);
        }

        /// <summary>
        /// Get account's API keys. Admins can get any account's keys.
        /// </summary>
        /// <param name="authorization">Auth token</param>
        /// <param name="userId">User ID</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{userId}/api-keys")]
        public IEnumerable<IApiKey> GetApiKeys(string userId, [FromHeader][Required] string authorization)
        {
            return _repository.GetApiKeys(authorization, userId);
        }

        /// <summary>
        /// Delete an API key. Non-Admin accounts can only delete their own API keys.
        /// </summary>
        /// <param name="authorization">Auth token</param>
        /// <param name="userId">User ID</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{userId}/api-keys")]
        public IActionResult DeleteApiKey(string userId, [FromHeader][Required] string authorization, [FromBody] DeleteApiKeyRequestBody r)
        {
            _repository.DeleteApiKey(r.ApiKey, authorization, userId);
            return Ok();
        }

        /// <summary>
        /// Update account's roles.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/roles")]
        public IActionResult UpdateRoles(string userId, [FromBody] UpdateAccountRolesRequestBody r)
        {
            _repository.UpdateRoles(userId, r);
            return Ok();
        }

        /// <summary>
        /// Update account's active state
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{userId}/is-active")]
        public IActionResult UpdateIsActive(string userId, [FromBody] UpdateAccountIsActiveRequestBody r)
        {
            _repository.UpdateIsActive(userId, r);
            return Ok();
        }

    }
}
