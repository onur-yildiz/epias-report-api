using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class UpdateReportRolesRequestParams
    {
        public UpdateReportRolesRequestParams(string[] roles)
        {
            Roles = roles;
        }

        [Required]
        public string[] Roles { get; set; }
    }
}
