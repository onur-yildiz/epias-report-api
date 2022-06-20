using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestParams
{
    public class UpdateReportIsActiveRequestParams
    {
        public UpdateReportIsActiveRequestParams(bool isActive)
        {
            IsActive = isActive;
        }

        [Required]
        public bool IsActive { get; set; }
    }
}
