using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UserLoginRequestBody : IUserLoginRequestBody
    {
        public UserLoginRequestBody(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
