using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class DppRequestParams : IDppRequestParams
    {
        /// <summary>
        /// End date of the period (inclusive)
        /// </summary>
        [Required]
        public string EndDate { get; set; } = String.Empty;

        /// <summary>
        /// Start date of the period (inclusive)
        /// </summary>
        [Required]
        public string StartDate { get; set; } = String.Empty;

        /// <summary>
        /// Organization EIC. Example: 40X000000000540Y
        /// </summary>
        public string? OrganizationEIC { get; set; }

        /// <summary>
        /// EUVÇB EIC. Example: 40W0000000001960
        /// </summary>
        public string? UevcbEIC { get; set; }
    }
}
