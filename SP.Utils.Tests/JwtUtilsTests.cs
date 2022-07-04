using MongoDB.Bson;
using SP.Utils.Jwt;

namespace SP.Utils.Tests
{
    public class JwtUtilsTests
    {
        const string OBJECT_ID_VALUE = "000000000000000000000000";
        const string TEST_SECRET = "00000000000000000000000000000000";
        const string OVERDUE_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJpZCI6IjAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMCIsIm5iZiI6MTY1NjkyOTE3OCwiZXhwIjoxNjU2OTI5MTg4LCJpYXQiOjE2NTY5MjkxNzh9.rSM524PvOMKm_dCtBdT48qFsPxuDZF2B6LdBcoYdI0E";

        static readonly ObjectId _userId = new(OBJECT_ID_VALUE);
        readonly string _token;

        public JwtUtilsTests()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", TEST_SECRET);
            _token = new JwtUtils().GenerateToken(_userId);
        }

       // //TODO DATETIME MOCK ABSTRACTION LAYER?
       //[Fact]
       // public void GenerateToken_UserId_ReturnsToken()
       // {
       //     //Setup
       //     var jwtUtils = new JwtUtils();

       //     //Act
       //     var result = jwtUtils.GenerateToken(_userId);

       //     //Assert
       //     Assert.NotNull(result);
       // }

        [Fact]
        public void ValidateToken_Token_ReturnsUserId()
        {
            //Setup
            var jwtUtils = new JwtUtils();

            //Act
            var result = jwtUtils.ValidateToken(_token);

            //Assert
            Assert.Equal(_userId, result);
        }

        [Fact]
        public void ValidateToken_OverdueToken_ReturnsNull()
        {
            //Setup
            var jwtUtils = new JwtUtils();

            //Act
            var result = jwtUtils.ValidateToken(OVERDUE_TOKEN);

            //Assert
            Assert.Null(result);
        }
    }
}
