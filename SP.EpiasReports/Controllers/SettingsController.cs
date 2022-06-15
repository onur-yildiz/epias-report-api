using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SP.Authorization;
using SP.Reports.Models.Api;
using SP.Reports.Models.Dpp;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
using SP.Reports.Models.McpSmps;
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;
using SP.Reports.Service;
using SP.Settings.Models;
using SP.Settings.Service;
using SP.User.Service;
using System.ComponentModel.DataAnnotations;

namespace SP.EpiasReport.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly ISettingsService _repository;

        public SettingsController(Serilog.ILogger logger, ISettingsService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("Role")]
        public ActionResult<Role?> GetRole([Required][FromQuery] string role)
        {
            return Ok(_repository.GetRole(role));
        }

        [Authorize(AdminRestricted = true)]
        [HttpPost("Role")]
        public ActionResult CreateRole([FromBody] Role role)
        {
            _repository.CreateRole(role);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpDelete("Role")]
        public ActionResult DeleteRole([FromBody] Role role)
        {
            _repository.DeleteRole(role);
            return Ok();
        }

        [HttpGet("Roles")]
        public ActionResult<IEnumerable<Role>?> GetRoles()
        {
            return Ok(_repository.GetRoles());
        }
    }
}