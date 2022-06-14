using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.User.Models
{
    public class AuthUserData : IAuthUserData
    {
        public AuthUserData(IAccount account, string token)
        {
            Email = account.Email;
            Id = account.Id.ToString();
            Name = account.Name;
            IsActive = account.IsActive;
            IsAdmin = account.IsAdmin;
            LanguageCode = account.LanguageCode;
            Roles = account.Roles;
            Token = token;
        }

        public string Email { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string LanguageCode { get; set; }
        public HashSet<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
