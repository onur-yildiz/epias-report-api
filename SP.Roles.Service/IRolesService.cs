using SP.Roles.Models;

namespace SP.Roles.Service
{
    public interface IRolesService
    {
        Role? GetRole(string roleName);
        void CreateRole(Role role);
        void DeleteRole(string roleName);
        IEnumerable<Role>? GetRoles();
    }
}