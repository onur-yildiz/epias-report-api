

using MongoDB.Bson;
using Moq;
using SP.Exceptions;
using SP.Reports.Models.ReportListing;
using SP.Roles.Models;
using SP.Roles.Service.Tests.Mocks;
using SP.Users.Models;
using System.Linq.Expressions;

namespace SP.Roles.Service.Tests
{
    public class Tests
    {
        static IEnumerable<object[]> GetSampleRoles_1()
        {
            yield return new object[] {
                new List<Role>() {}
            };
            yield return new object[] {
                new List<Role>() {
                    new Role { Name = "Role_1" },
                    new Role { Name = "Role_2" }
                }
            };
            yield return new object[] {
                new List<Role>() {
                    new Role { Name = "Role_1" },
                    new Role { Name = "Role_2" },
                    new Role { Name = "Role_3" },
                    new Role { Name = "Role_4" }
                }
            };
        }
        static IEnumerable<object[]> GetSampleRoles_2()
        {
            yield return new object[] {
                new string ("Role_2"),
                new List<Role>() {
                    new Role { Name = "Role_1" },
                    new Role { Name = "Role_2" }
                }
            };
            yield return new object[] {
                new string ("Role_4"),
                new List<Role>() {
                    new Role { Name = "Role_1" },
                    new Role { Name = "Role_2" },
                    new Role { Name = "Role_3" },
                    new Role { Name = "Role_4" }
                }
            };
        }

        static (DependencyMocks, RolesService) RolesServiceSetup()
        {
            var mocks = new DependencyMocks();
            var service = new RolesService(mocks.RoleRepository.Object, mocks.UserRepository.Object, mocks.ReportRepository.Object);
            return (mocks, service);
        }

        [Fact]
        public void GetRoles_NoParams_ReturnsEmptyList()
        {
            //Setup
            var (_, service) = RolesServiceSetup();

            //Act
            var roles = service.GetRoles();

            //Assert
            Assert.False(roles.Any());
        }

        [Theory]
        [MemberData(nameof(GetSampleRoles_1))]
        public void GetRoles_NoParams_ReturnsListOfRoles(IEnumerable<Role> roles)
        {
            //Setup
            var (mocks, service) = RolesServiceSetup();
            mocks.RoleRepository.Setup(r => r.Get()).Returns(roles);

            //Act
            var result = service.GetRoles();

            //Assert
            Assert.Equal(roles, result);
        }

        [Fact]
        public void GetRole_NonExistentRoleName_Throws()
        {
            //Setup
            var NonExistentRoleName = "Role_1";
            var (mocks, service) = RolesServiceSetup();

            //Act
            void act() => service.GetRole(NonExistentRoleName);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Theory]
        [MemberData(nameof(GetSampleRoles_2))]
        public void GetRole_RoleName_ReturnsRole(string roleName, IEnumerable<Role> roles)
        {
            //Setup
            var (mocks, service) = RolesServiceSetup();
            var role = roles.FirstOrDefault(r => r.Name == roleName);
            mocks.RoleRepository.Setup(r => r.GetFirst(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role);

            //Act
            var result = service.GetRole(roleName);

            //Assert
            Assert.Equal(roleName, result.Name);
        }

        [Fact]
        public void CreateRole_ExistingRole_Throws()
        {
            //Setup
            var role = new Role { Id = new ObjectId(), Name = "Role_1" };
            var (mocks, service) = RolesServiceSetup();
            mocks.RoleRepository.Setup(r => r.GetFirst(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role);

            //Act
            void act() => service.CreateRole(role);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void CreateRole_Role_CallsAddOne()
        {
            //Setup
            var role = new Role { Id = new ObjectId(), Name = "Role_1" };
            var (mocks, service) = RolesServiceSetup();

            //Act
            service.CreateRole(role);

            //Assert
            mocks.RoleRepository.Verify(r => r.AddOne(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public void DeleteRole_RoleName_CallsRemovesForAll()
        {
            var role = new Role { Id = new ObjectId(), Name = "Role_1" };
            var (mocks, service) = RolesServiceSetup();
            mocks.RoleRepository.Setup(r => r.RemoveOne(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role);

            //Act
            service.DeleteRole(role.Name);

            //Assert
            mocks.RoleRepository.Verify(r => r.RemoveOne(It.IsAny<Expression<Func<Role, bool>>>()), Times.Once);
            mocks.UserRepository.Verify(r => r.UpdateMany_Pull(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mocks.ReportRepository.Verify(r => r.UpdateMany_Pull(It.IsAny<Expression<Func<Report, bool>>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteRole_NonExistentRoleName_Throws()
        {
            var roleName = "Role_1";
            var (mocks, service) = RolesServiceSetup();

            //Act
            void act() => service.DeleteRole(roleName);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }
    }
}