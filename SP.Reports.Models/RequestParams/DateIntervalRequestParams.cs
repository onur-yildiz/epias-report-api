using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SP.Reports.Models.RequestParams
{
    public class DateIntervalRequestParams : IDateIntervalRequestParams
    {
        public DateIntervalRequestParams(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }
    }
}