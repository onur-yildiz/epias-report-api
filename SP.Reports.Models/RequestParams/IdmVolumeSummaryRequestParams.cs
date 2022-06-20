using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class IdmVolumeSummaryRequestParams : IIdmVolumeSummaryRequestParams
    {
        /// <summary>
        /// Start date of the period (inclusive)
        /// </summary>
        [Required]
        public string StartDate { get; set; } = String.Empty;

        /// <summary>
        /// End date of the period (inclusive)
        /// </summary>
        [Required]
        public string EndDate { get; set; } = String.Empty;

        /// <summary>
        /// Period: DAILY, WEEKLY, MONTHLY, PERIODIC
        /// </summary>
        [Required]
        public string Period { get; set; } = String.Empty;
    }
}
