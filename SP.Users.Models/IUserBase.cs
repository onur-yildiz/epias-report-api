namespace SP.Users.Models
{
    public interface IUserBase<T>
    {
        /// <summary>
        /// Account E-mail
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Account's active state
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Account's admin state
        /// </summary>
        bool IsAdmin { get; set; }

        /// <summary>
        /// User's preferred language
        /// </summary>
        string LanguageCode { get; set; }

        /// <summary>
        /// Account roles
        /// </summary>
        HashSet<string> Roles { get; set; }
    }
}
