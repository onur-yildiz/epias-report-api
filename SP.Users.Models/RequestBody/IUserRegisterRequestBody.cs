namespace SP.Users.Models.RequestParams
{
    public interface IUserRegisterRequestBody
    {
        string Email { get; set; }
        string LanguageCode { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }
}