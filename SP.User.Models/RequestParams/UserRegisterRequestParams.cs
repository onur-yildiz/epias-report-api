namespace SP.User.Models.RequestParams
{
    public class UserRegisterRequestParams : IUserLoginRequestParams
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LanguageCode { get; set; } = "en";
    }
}
