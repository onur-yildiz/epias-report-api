using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UpdateAccountIsActiveRequestBody : IUpdateAccountIsActiveRequestBody
    {
        /// <summary>
        /// Account's active state. User won't be able to login or access role-secured content
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
