using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.User.Models.RequestParams;
using SP.User.Service;

namespace SP.EpiasReport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _repository;

        public UserController(ILogger<UserController> logger, IUserService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<String> Register([FromBody] UserRegisterRequestParams r)
        {
            return Ok(_repository.Register(r));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<String> Login([FromBody] UserLoginRequestParams r)
        {
            return Ok(_repository.Login(r));
        }

        [HttpPost("AssignRole")]
        public ActionResult<String> AssignRole([FromBody] UpdateRoleRequestParams r)
        {
            _repository.AssignRole(r);
            return Ok();
        }

        [HttpPost("RemoveRole")]
        public ActionResult<String> RemoveRole([FromBody] UpdateRoleRequestParams r)
        {
            _repository.RemoveRole(r);
            return Ok();
        }

        [HttpPost("UpdateIsActive")]
        public ActionResult<String> UpdateIsActive([FromBody] UpdateIsActiveRequestParams r)
        {
            _repository.UpdateIsActive(r);
            return Ok();
        }
    }
}
