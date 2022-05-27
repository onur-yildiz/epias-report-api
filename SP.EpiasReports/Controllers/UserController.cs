using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
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
        public ActionResult<String> Register([FromBody] UserRegisterRequestParams r)
        {
            var token = _repository.Register(r);
            _logger.Information("{action}: {email}", "REGISTER", r.Email);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<String> Login([FromBody] UserLoginRequestParams r)
        {
            var token = _repository.Login(r);
            _logger.Information("{action}: {email}", "LOGIN", r.Email);
            return Ok(token);
        }

        [Authorize(Roles = new string[] { "ADMIN" }, LogBody = true)]
        [HttpPost("AssignRole")]
        public ActionResult<String> AssignRole([FromBody] UpdateRoleRequestParams r)
        {
            _repository.AssignRole(r);
            return Ok();
        }

        [Authorize(Roles = new string[] { "ADMIN" }, LogBody = true)]
        [HttpPost("RemoveRole")]
        public ActionResult<String> RemoveRole([FromBody] UpdateRoleRequestParams r)
        {
            _repository.RemoveRole(r);
            return Ok();
        }

        [Authorize(Roles = new string[] { "ADMIN" }, LogBody = true)]
        [HttpPost("UpdateIsActive")]
        public ActionResult<String> UpdateIsActive([FromBody] UpdateIsActiveRequestParams r)
        {
            _repository.UpdateIsActive(r);
            return Ok();
        }
    }
}
