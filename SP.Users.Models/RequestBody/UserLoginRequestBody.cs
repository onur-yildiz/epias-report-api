using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UserLoginRequestBody : IUserLoginRequestBody
    {
        /// <summary>
        /// Account e-mail address
        /// </summary>
        [Required]
        public string Email { get; set; } = String.Empty;

        /// <summary>
        /// Account password
        /// </summary>
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
