using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class DppInjectionUnitNameRequestParams
    {
        public DppInjectionUnitNameRequestParams(string organizationEIC)
        {
            OrganizationEIC = organizationEIC;
        }

        [Required]
        public string OrganizationEIC { get; set; };
    }
}
