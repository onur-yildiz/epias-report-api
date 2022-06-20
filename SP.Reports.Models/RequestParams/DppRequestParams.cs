using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class DppRequestParams : IDateIntervalRequestParams
    {
        public DppRequestParams(string endDate, string startDate, string organizationEIC, string uevcbEIC)
        {
            EndDate = endDate;
            StartDate = startDate;
            OrganizationEIC = organizationEIC;
            UevcbEIC = uevcbEIC;
        }

        [Required]
        public string EndDate { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string OrganizationEIC { get; set; }

        [Required]
        public string UevcbEIC { get; set; } 
    }
}
