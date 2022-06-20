using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestParams
{
    public class UpdateAccountRolesRequestBody
    {
        public UpdateAccountRolesRequestBody(string[] roles)
        {
            Roles = roles;
        }

        [Required]
        public string[] Roles { get; set; }
    }
}
