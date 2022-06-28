using SP.Roles.Models;

namespace SP.Roles.Service
{
    public interface IRolesService
    {
        Role GetRole(string roleName);
        void CreateRole(IRole role);
        void DeleteRole(string roleName);
        IEnumerable<Role> GetRoles();
    }
}