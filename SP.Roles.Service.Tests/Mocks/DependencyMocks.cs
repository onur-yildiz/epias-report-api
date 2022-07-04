using Moq;
using SP.DAL.Interfaces;

namespace SP.Roles.Service.Tests.Mocks
{
    internal class DependencyMocks
    {
        public Mock<IRoleRepository> RoleRepository = new();
        public Mock<IUserRepository> UserRepository = new();
        public Mock<IReportRepository> ReportRepository = new();
    }
}
