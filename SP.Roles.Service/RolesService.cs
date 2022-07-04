using SP.DAL.Interfaces;
using SP.Exceptions;
using SP.Roles.Models;

namespace SP.Roles.Service
{
    public class RolesService : IRolesService
    {
        readonly IRoleRepository _roleRepository;
        readonly IUserRepository _userRepository;
        readonly IReportRepository _reportRepository;

        public RolesService(IRoleRepository roleRepository, IUserRepository userRepository, IReportRepository reportRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _reportRepository = reportRepository;
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = _roleRepository.Get();
            return roles;
        }

        public Role GetRole(string roleName)
        {
            var role = _roleRepository.GetFirst(r => r.Name == roleName);
            if (role == null) throw HttpResponseException.NotExists("Role");
            return role;
        }

        public void CreateRole(IRole role)
        {
            var existingRole = _roleRepository.GetFirst(r => r.Name == role.Name);
            if (existingRole != null) throw HttpResponseException.AlreadyExists("Role");
            _roleRepository.AddOne((Role)role);
        }

        public void DeleteRole(string roleName)
        {
            var removedRole = _roleRepository.RemoveOne(r => r.Name == roleName);
            if (removedRole == null) throw HttpResponseException.DatabaseError();
            _userRepository.UpdateMany_Pull(u => u.Roles.Contains(roleName), "roles", roleName);
            _reportRepository.UpdateMany_Pull(r => r.Roles.Contains(roleName), "roles", roleName);
        }
    }
}
