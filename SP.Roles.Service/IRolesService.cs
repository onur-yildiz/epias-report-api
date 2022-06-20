using SP.Roles.Models;

namespace SP.Roles.Service
{
    public interface IRolesService
    {
        IRole? GetRole(string roleName);
        void CreateRole(IRole role);
        void DeleteRole(string roleName);
        IEnumerable<IRole>? GetRoles();
    }
}