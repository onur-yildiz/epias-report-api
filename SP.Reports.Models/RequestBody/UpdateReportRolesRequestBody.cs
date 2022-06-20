using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestBody
{
    public class UpdateReportRolesRequestBody : IUpdateReportRolesRequestBody
    {
        /// <summary>
        /// List of role names
        /// </summary>
        [Required]
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
