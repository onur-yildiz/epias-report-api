namespace SP.Users.Models
{
    public interface IAuthUser : IUserBase<string>
    {
        /// <summary>
        /// Auth token
        /// </summary>
        string Token { get; set; }
    }
}
