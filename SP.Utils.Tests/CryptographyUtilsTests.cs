using SP.Utils.Cryptography;

namespace SP.Utils.Tests
{
    public class CryptographyUtilsTests
    {
        const string _password = "password";
        const string _hashedPassword = "5VH2s/nAtaE798Vauw0nF7ezjbxoJPcA9r6Gg1+gP2I=";

        static readonly byte[] _salt = new byte[] { 0, 0, 0, 0 };

        [Fact]
        public void Encrypt_PasswordAndSalt_ReturnsHashedAndSalt()
        {
            //Setup
            var cryptUtils = new CryptographyUtils();

            //Act
            var (resultHashed, resultSalt) = cryptUtils.Encrypt(_password, _salt);

            //Assert
            Assert.Equal(_hashedPassword, resultHashed);
            Assert.Equal(_salt, resultSalt);
        }

        [Fact]
        public void Encrypt_Password_ReturnsHashedAndSalt()
        {
            //Setup
            var cryptUtils = new CryptographyUtils();

            //Act
            var (resultHashed, resultSalt) = cryptUtils.Encrypt(_password);

            //Assert
            Assert.NotEqual(_hashedPassword, resultHashed);
            Assert.True(resultSalt.Length == 128 / 8);
        }

        [Fact]
        public void IsPasswordCorrect_PasswordHashedAndSalt_ReturnsTrue()
        {
            //Setup
            var cryptUtils = new CryptographyUtils();

            //Act
            var result = cryptUtils.IsPasswordCorrect(_password, _hashedPassword, _salt);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(_password, _hashedPassword, new byte[] { 1, 1, 1, 1 })] // different salt
        [InlineData(_password, "diffHashedPassword", new byte[] { 0, 0, 0, 0 })] // _salt = {0,0,0,0}
        [InlineData("diffPassword", _hashedPassword, new byte[] { 0, 0, 0, 0 })] // _salt = {0,0,0,0}
        public void IsPasswordCorrect_NonMatchingValues_ReturnsFalse(string password, string hashed, byte[] salt)
        {
            //Setup
            var cryptUtils = new CryptographyUtils();

            //Act
            var result = cryptUtils.IsPasswordCorrect(password, hashed, salt);

            //Assert
            Assert.False(result);
        }
    }
}
