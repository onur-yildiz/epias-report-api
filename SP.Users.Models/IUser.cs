using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Users.Models
{
    public interface IUser: IUserBase
    {
        string Id { get; set; }
    }
}
