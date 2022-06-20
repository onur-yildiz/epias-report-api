using System.ComponentModel.DataAnnotations;

namespace SP.Reports.Models.RequestBody
{
    public class UpdateReportIsActiveRequestBody : IUpdateReportIsActiveRequestBody
    {
        /// <summary>
        /// Report's active state. Report won't be visible to anyone if deactivated.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
