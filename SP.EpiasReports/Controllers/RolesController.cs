using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.EpiasReports.Models;
using SP.EpiasReports.Swagger;
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
        private readonly IRolesService _service;

        public RolesController(Serilog.ILogger logger, IRolesService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Serves all roles.
        /// </summary>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public ApiResponse<IEnumerable<Role>> GetRoles()
        {
            return ApiResponse<IEnumerable<Role>>.Success(_service.GetRoles());
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <param name="role">New role</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPost("")]
        public ApiResponse<dynamic> CreateRole([FromBody] Role role)
        {
            _service.CreateRole(role);
            return ApiResponse<dynamic>.Success();
        }

        /// <summary>
        /// Get a specific role.
        /// </summary>
        /// <param name="name">Role name</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("{name}")]
        public ApiResponse<Role> GetRole(string name)
        {
            return ApiResponse<Role>.Success(_service.GetRole(name));
        }

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="name">Role name</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpDelete("{name}")]
        public ApiResponse<dynamic> DeleteRole(string name)
        {
            _service.DeleteRole(name);
            return ApiResponse<dynamic>.Success();
        }
    }
}