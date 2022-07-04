using Moq;
using SP.DAL.Interfaces;
using SP.Utils.Cryptography;
using SP.Utils.Jwt;

namespace SP.Users.Service.Tests.Mocks
{
    internal class DependencyMocks
    {
        public Mock<IUserRepository> UserRepository = new();
        public Mock<IApiKeyRepository> ApiKeyRepository = new();
        public Mock<IJwtUtils> JwtUtils = new();
        public Mock<ICryptographyUtils> CryptUtils = new();
    }
}
