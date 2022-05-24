namespace SP.User.Models.RequestParams
{
    public class UserLoginRequestParams : IUserLoginRequestParams
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
