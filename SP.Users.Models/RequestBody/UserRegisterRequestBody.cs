using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UserRegisterRequestBody : IUserRegisterRequestBody
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string Name { get; set; } = String.Empty;

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

        /// <summary>
        /// User language preference
        /// </summary>
        [Required]
        public string LanguageCode { get; set; } = "en";
    }
}
