namespace SP.Users.Models
{
    public interface IAuthUser : IUser
    {
        /// <summary>
        /// Auth token
        /// </summary>
        string Token { get; set; }
    }
}
