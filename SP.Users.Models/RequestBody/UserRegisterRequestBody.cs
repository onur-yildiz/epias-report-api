namespace SP.Users.Models.RequestParams
{
    public class UserRegisterRequestBody : IUserLoginRequestBody
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LanguageCode { get; set; } = "en";
    }
}
