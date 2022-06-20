using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public interface IUserLoginRequestBody
    {
        string Email { get; set; }

        string Password { get; set; }
    }
}