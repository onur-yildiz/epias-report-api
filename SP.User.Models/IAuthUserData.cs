using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.User.Models
{
    public interface IAuthUserData: IAdminServicableUserData
    {
        string Token { get; set; }
    }
}
