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
        private readonly IRolesService _repository;

        public RolesController(Serilog.ILogger logger, IRolesService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Serves all roles.
        /// </summary>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public ApiResponse<IEnumerable<IRole>> GetRoles()
        {
            return ApiResponse<IEnumerable<IRole>>.Success(_repository.GetRoles());
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
            _repository.CreateRole(role);
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
        public ApiResponse<IRole> GetRole(string name)
        {
            return ApiResponse<IRole>.Success(_repository.GetRole(name));
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
            _repository.DeleteRole(name);
            return ApiResponse<dynamic>.Success();
        }
    }
}