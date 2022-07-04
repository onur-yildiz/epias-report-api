using MongoDB.Bson;
using Moq;
using SP.Users.Models;
using SP.Exceptions;
using SP.Users.Models.RequestParams;
using System.Linq.Expressions;
using SP.Users.Service.Tests.Mocks;
using SP.Extensions.Object;

namespace SP.Users.Service.Tests
{
    public class Tests
    {
        private const string OBJECT_ID_VALUE = "000000000000000000000000";
        private const string TOKEN = "token";
        private const string NEW_TOKEN = "newToken";
        private const string HASHED_PASSWORD = "hashedPassword";

        static readonly Account _account = new(ObjectId.Parse(OBJECT_ID_VALUE), String.Empty, String.Empty, HASHED_PASSWORD, new HashSet<string>(), true, false, String.Empty, Array.Empty<byte>());
        static readonly AuthUser _authUser = new(_account, TOKEN);
        static readonly AuthUser _authUserNewToken = new(_account, NEW_TOKEN);


        static (DependencyMocks, UsersService) UsersServiceSetup()
        {
            var mocks = new DependencyMocks();
            var service = new UsersService(mocks.UserRepository.Object, mocks.ApiKeyRepository.Object, mocks.JwtUtils.Object, mocks.CryptUtils.Object);
            mocks.JwtUtils.Setup(j => j.GenerateToken(It.IsAny<ObjectId>())).Returns(NEW_TOKEN);
            mocks.JwtUtils.Setup(j => j.ValidateToken(It.IsAny<string>())).Returns(new ObjectId(OBJECT_ID_VALUE));
            mocks.CryptUtils.Setup(c => c.Encrypt(It.IsAny<string>(), It.IsAny<byte[]?>())).Returns((HASHED_PASSWORD, Array.Empty<byte>()));

            return (mocks, service);
        }

        [Fact]
        public void RefreshToken_Token_ReturnsAuthUserWithNewToken()
        {
            //Setup
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.GetById(It.IsAny<ObjectId>())).Returns(_account);

            //Act
            var result = service.RefreshToken(TOKEN);

            //Assert
            Assert.NotEqual(_authUser.Token, result.Token);
            Assert.True(_authUserNewToken.PublicInstancePropertiesEqual(result));
        }

        [Fact]
        public void RefreshToken_InvalidToken_Throws()
        {
            //Setup
            var (mocks, service) = UsersServiceSetup();
            mocks.JwtUtils.Setup(j => j.ValidateToken(It.IsAny<string>())).Returns((ObjectId?)null); // override default setup in UsersServiceSetup

            //Act
            void act() => service.RefreshToken(TOKEN);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void RefreshToken_NonExistingAccountToken_Throws()
        {
            //Setup
            //GetById is not mocked => returns default (null)
            var (_, service) = UsersServiceSetup();

            //Act
            void act() => service.RefreshToken(TOKEN);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void Register_RegisterRequestBody_ReturnsAuthUser()
        {
            //Setup
            var registerReqBody = new UserRegisterRequestBody() { Email = _authUserNewToken.Email, Name = _authUserNewToken.Name, Password = String.Empty, LanguageCode = _authUserNewToken.LanguageCode };
            var (_, service) = UsersServiceSetup();

            //Act
            var result = service.Register(registerReqBody);

            //Assert
            Assert.True(_authUserNewToken.PublicInstancePropertiesEqual(result, nameof(result.Id)));
            Assert.True(ObjectId.TryParse(result.Id, out _));
        }

        [Fact]
        public void Register_ExistingAccountInfo_Throws()
        {
            //Setup
            var registerReqBody = new Mock<IUserRegisterRequestBody>();
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(_account);

            //Act
            void act() => service.Register(registerReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void Login_CorrectLoginCredentials_ReturnsAuthUser()
        {
            //Setup
            var loginReqBody = new Mock<IUserLoginRequestBody>();
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(_account);
            mocks.CryptUtils.Setup(s => s.IsPasswordCorrect(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            //Act
            var result = service.Login(loginReqBody.Object);

            //Assert
            Assert.True(_authUserNewToken.PublicInstancePropertiesEqual(result));
        }

        [Fact]
        public void Login_IncorrectPassword_Throws()
        {
            //Setup
            var (mocks, service) = UsersServiceSetup();
            var loginReqBody = new Mock<IUserLoginRequestBody>();
            mocks.UserRepository.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(_account);

            //Act
            void act() => service.Login(loginReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void Login_NonExistentAccountCredentials_Throws()
        {
            //Setup
            var loginReqBody = new Mock<IUserLoginRequestBody>();
            var (mocks, service) = UsersServiceSetup();

            //Act
            void act() => service.Login(loginReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void UpdateRoles_UserIdAndRoles_CallsUpdate()
        {
            //Setup
            var rolesReqBody = new Mock<IUpdateAccountRolesRequestBody>();
            var userId = String.Empty;
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.UpdateOne_Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns(_account).Verifiable();

            //Act
            service.UpdateRoles(userId, rolesReqBody.Object);

            //Assert
            mocks.UserRepository.Verify();
        }

        [Fact]
        public void UpdateRoles_UserIdAndRoles_UpdateReturnsNullAndThrows()
        {
            //Setup
            var rolesReqBody = new Mock<IUpdateAccountRolesRequestBody>();
            var userId = String.Empty;
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.UpdateOne_Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>())).Returns((Account?)null);

            //Act
            void act() => service.UpdateRoles(userId, rolesReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void UpdateIsActive_UserIdAndActiveStatus_CallsUpdate()
        {
            //Setup
            var isActiveReqBody = new Mock<IUpdateAccountIsActiveRequestBody>();
            var userId = String.Empty;
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.UpdateOne_Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(_account).Verifiable();

            //Act
            service.UpdateIsActive(userId, isActiveReqBody.Object);

            //Assert
            mocks.UserRepository.Verify();
        }

        [Fact]
        public void UpdateIsActive_UserIdAndActiveStatus_UpdateReturnsNullAndThrows()
        {
            //Setup
            var isActiveReqBody = new Mock<IUpdateAccountIsActiveRequestBody>();
            var userId = String.Empty;
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(u => u.UpdateOne_Set(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns((Account?)null);

            //Act
            void act() => service.UpdateIsActive(userId, isActiveReqBody.Object);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void GetApiKeys_UserId_ReturnsApiKeys()
        {
            //Setup
            var (mocks, service) = UsersServiceSetup();
            var list = new List<ApiKey>();
            mocks.ApiKeyRepository.Setup(a => a.Get()).Returns(list);

            //Act
            var result = service.GetApiKeys(OBJECT_ID_VALUE);

            //Assert
            Assert.Equal(list, result);
        }

        [Fact]
        public void GetApiKeys_FaultyUserId_Throws()
        {
            //Setup
            var faultyUserId = String.Empty;
            var (mocks, service) = UsersServiceSetup();

            //Act
            void act() => service.GetApiKeys(faultyUserId);

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void CreateApiKey_UserId_CallsAddAndReturnsApiKey()
        {
            //Setup
            var emptyList = new List<ApiKey>();
            var (mocks, service) = UsersServiceSetup();
            mocks.ApiKeyRepository.Setup(a => a.Get(It.IsAny<Expression<Func<ApiKey, bool>>>())).Returns(emptyList);
            mocks.ApiKeyRepository.Setup(a => a.AddOne(It.IsAny<ApiKey>())).Verifiable();

            //Act
            var result = service.CreateApiKey(OBJECT_ID_VALUE);

            //Assert
            Assert.True(result.Any());
            mocks.ApiKeyRepository.Verify();
        }

        [Fact]
        public void CreateApiKey_FaultyUserId_Throws()
        {
            //Setup
            var faultyUserId = String.Empty;
            var (mocks, service) = UsersServiceSetup();

            //Act
            void act() => service.CreateApiKey(faultyUserId);

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void CreateApiKey_UserId_ThrowsMaxApiKeys()
        {
            //Setup
            var apiKey = new ApiKey(OBJECT_ID_VALUE, String.Empty, String.Empty);
            var listWith3ApiKeys = new List<ApiKey>() { apiKey, apiKey, apiKey };
            var (mocks, service) = UsersServiceSetup();
            mocks.ApiKeyRepository.Setup(a => a.Get(It.IsAny<Expression<Func<ApiKey, bool>>>())).Returns(listWith3ApiKeys);

            //Act
            void act() => service.CreateApiKey(OBJECT_ID_VALUE);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void DeleteApiKey_ApiKeyAndUserId_CallsDelete()
        {
            //Setup
            var apiKey = "123";
            var removedApiKey = new ApiKey(OBJECT_ID_VALUE, apiKey, String.Empty);
            var (mocks, service) = UsersServiceSetup();
            mocks.ApiKeyRepository.Setup(a => a.RemoveOne(It.IsAny<Expression<Func<ApiKey, bool>>>())).Returns(removedApiKey).Verifiable();

            //Act
            service.DeleteApiKey(apiKey, OBJECT_ID_VALUE);

            //Assert
            mocks.ApiKeyRepository.Verify();
        }

        [Fact]
        public void DeleteApiKey_FaultyUserId_Throws()
        {
            //Setup
            var apiKey = "123";
            var faultyUserId = String.Empty;
            var (mocks, service) = UsersServiceSetup();

            //Act
            void act() => service.DeleteApiKey(apiKey, faultyUserId);

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void DeleteApiKey_UserId_RemovalReturnsNullAndThrows()
        {
            //Setup
            var apiKey = "123";
            var (mocks, service) = UsersServiceSetup();
            mocks.ApiKeyRepository.Setup(a => a.RemoveOne(It.IsAny<Expression<Func<ApiKey, bool>>>())).Returns((ApiKey?)null);

            //Act
            void act() => service.DeleteApiKey(apiKey, OBJECT_ID_VALUE);

            //Assert
            Assert.Throws<HttpResponseException>(act);
        }

        [Fact]
        public void GetAllUsers_NoParams_ReturnsUsers()
        {
            //Setup
            var accounts = new List<Account>() { _account };
            var (mocks, service) = UsersServiceSetup();
            mocks.UserRepository.Setup(a => a.Get()).Returns(accounts).Verifiable();

            //Act
            var result = service.GetAllUsers();

            //Assert
            Assert.True(result.Any());
            mocks.UserRepository.Verify();
        }
    }
}
