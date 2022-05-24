namespace SP.User.Models.RequestParams
{
    public interface IUserLoginRequestParams
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}