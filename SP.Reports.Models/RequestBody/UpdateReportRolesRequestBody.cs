using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestBody
{
    public class UpdateReportRolesRequestBody
    {
        public UpdateReportRolesRequestBody(string[] roles)
        {
            Roles = roles;
        }

        [Required]
        public string[] Roles { get; set; }
    }
}
