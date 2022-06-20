using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.Roles.Models;
using SP.Roles.Service;

namespace SP.EpiasReport.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IRolesService _repository;

        public RolesController(Serilog.ILogger logger, IRolesService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public IEnumerable<Role>? GetRoles()
        {
            return _repository.GetRoles();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPost("")]
        public ActionResult CreateRole([FromBody] Role role)
        {
            _repository.CreateRole(role);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpGet("{name}")]
        public Role? GetRole(string name)
        {
            return _repository.GetRole(name);
        }
        
        [Authorize(AdminRestricted = true)]
        [HttpDelete("{name}")]
        public ActionResult DeleteRole(string name)
        {
            _repository.DeleteRole(name);
            return Ok();
        }
    }
}