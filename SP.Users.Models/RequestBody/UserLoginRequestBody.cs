namespace SP.Users.Models.RequestParams
{
    public class UserLoginRequestBody : IUserLoginRequestBody
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
