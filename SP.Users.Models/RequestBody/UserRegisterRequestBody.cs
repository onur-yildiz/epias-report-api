using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UserRegisterRequestBody : IUserLoginRequestBody
    {
        public UserRegisterRequestBody(string name, string email, string password, string languageCode = "en")
        {
            Name = name;
            Email = email;
            Password = password;
            LanguageCode = languageCode;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string LanguageCode { get; set; }
    }
}
