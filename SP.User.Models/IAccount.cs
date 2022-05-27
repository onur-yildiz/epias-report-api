namespace SP.User.Models
{
    public interface IAccount : IUserInfo, IUserSettings
    {
        string Password { get; set; }
        byte[] Salt { get; set; }
    }
}