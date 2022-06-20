using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class DppInjectionUnitNameRequestParams : IDppInjectionUnitNameRequestParams
    {
        /// <summary>
        /// Organization EIC. Example: 40X000000000540Y
        /// </summary>
        [Required]
        public string OrganizationEIC { get; set; } = String.Empty;
    }
}
