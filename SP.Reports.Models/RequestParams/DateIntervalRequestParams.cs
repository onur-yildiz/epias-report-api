using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class DateIntervalRequestParams : IDateIntervalRequestParams
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
    }
}