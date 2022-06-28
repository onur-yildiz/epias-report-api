using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.EpiasReports.Models;
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
        public ApiResponse<IEnumerable<IUserBase<string>>> GetUsers()
        {
            return ApiResponse<IEnumerable<IUserBase<string>>>.Success(_repository.GetAllUsers());
        }

        /// <summary>
        /// Register a new account.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public ApiResponse<AuthUser> Register([FromBody] UserRegisterRequestBody r)
        {
            var user = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return ApiResponse<AuthUser>.Success(user);
        }

        /// <summary>
        /// Log into an account.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ApiResponse<AuthUser> Login([FromBody] UserLoginRequestBody r)
        {
            var user = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return ApiResponse<AuthUser>.Success(user);
        }

        /// <summary>
        /// Refresh auth token with a new one.
        /// </summary>
        /// <param name="authorization">Auth token</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public ApiResponse<AuthUser> RefreshToken([FromHeader][Required] string authorization)
        {
            var user = _repository.RefreshToken(authorization);
            _logger.Information("{action}: {email}", "REFRESH TOKEN", user.Email);
            return ApiResponse<AuthUser>.Success(user);
        }

        /// <summary>
        /// Create a new API key for the account.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        [Authorize]
        [MatchAccount(AdminAllowed = true)]
        [HttpPost("{userId}/api-keys/create")]
        public ApiResponse<string> CreateApiKey(string userId)
        {
            return ApiResponse<string>.Success(_repository.CreateApiKey(userId));
        }

        /// <summary>
        /// Get account's API keys. Admins can get any account's keys.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        [Authorize]
        [MatchAccount(AdminAllowed = true)]
        [HttpGet("{userId}/api-keys")]
        public ApiResponse<IEnumerable<ApiKey>> GetApiKeys(string userId)
        {
            return ApiResponse<IEnumerable<ApiKey>>.Success(_repository.GetApiKeys(userId));
        }

        /// <summary>
        /// Delete an API key. Non-Admin accounts can only delete their own API keys.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [Authorize]
        [MatchAccount(AdminAllowed = true)]
        [HttpDelete("{userId}/api-keys")]
        public ApiResponse<dynamic> DeleteApiKey(string userId, [FromBody] DeleteApiKeyRequestBody r)
        {
            _repository.DeleteApiKey(r.ApiKey, userId);
            return ApiResponse<dynamic>.Success();
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
        public ApiResponse<dynamic> UpdateRoles(string userId, [FromBody] UpdateAccountRolesRequestBody r)
        {
            _repository.UpdateRoles(userId, r);
            return ApiResponse<dynamic>.Success();
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
        public ApiResponse<dynamic> UpdateIsActive(string userId, [FromBody] UpdateAccountIsActiveRequestBody r)
        {
            _repository.UpdateIsActive(userId, r);
            return ApiResponse<dynamic>.Success();
        }

    }
}
