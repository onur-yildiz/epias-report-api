using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UpdateAccountRolesRequestBody : IUpdateAccountRolesRequestBody
    {
        /// <summary>
        /// List of role names
        /// </summary>
        [Required]
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
