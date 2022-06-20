namespace SP.Users.Models
{
    public interface IAuthUser : IUserBase<string>
    {
        string Token { get; set; }
    }
}
