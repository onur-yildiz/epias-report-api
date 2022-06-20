using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UpdateAccountIsActiveRequestBody
    {
        public UpdateAccountIsActiveRequestBody(bool isActive)
        {
            IsActive = isActive;
        }

        [Required]
        public bool IsActive { get; set; }
    }
}
