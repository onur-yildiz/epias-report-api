﻿using Microsoft.AspNetCore.Mvc;
using SP.Authorization;
using SP.EpiasReports.Swagger;
using SP.Roles.Models;
using SP.Roles.Service;
using System.ComponentModel.DataAnnotations;

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
        public IEnumerable<IRole>? GetRoles()
        {
            return _repository.GetRoles();
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <param name="role">New role</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPost("")]
        public IActionResult CreateRole([FromBody] Role role)
        {
            _repository.CreateRole(role);
            return Ok();
        }

        /// <summary>
        /// Get a specific role.
        /// </summary>
        /// <param name="name">Role name</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("{name}")]
        public IRole? GetRole(string name)
        {
            return _repository.GetRole(name);
        }

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="name">Role name</param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpDelete("{name}")]
        public IActionResult DeleteRole(string name)
        {
            _repository.DeleteRole(name);
            return Ok();
        }
    }
}