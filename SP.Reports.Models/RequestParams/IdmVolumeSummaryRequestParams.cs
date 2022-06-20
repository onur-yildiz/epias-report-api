using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class IdmVolumeSummaryRequestParams : IDateIntervalRequestParams
    {
        public IdmVolumeSummaryRequestParams(string startDate, string endDate, string period)
        {
            StartDate = startDate;
            EndDate = endDate;
            Period = period;
        }

        [Required]
        public string StartDate { get; set; } = String.Empty;

        [Required]
        public string EndDate { get; set; } = String.Empty;

        [Required]
        public string Period { get; set; } = String.Empty;
    }
}
