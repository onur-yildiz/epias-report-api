using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.User.Models
{
    public interface IAdminServicableUserData: IUserSettings
    {
        string Email { get; set; }
        string Id { get; set; }
        string Name { get; set; }
    }
}
