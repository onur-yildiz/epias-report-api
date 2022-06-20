using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestBody
{
    public class UpdateReportIsActiveRequestBody
    {
        public UpdateReportIsActiveRequestBody(bool isActive)
        {
            IsActive = isActive;
        }

        [Required]
        public bool IsActive { get; set; }
    }
}
