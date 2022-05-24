﻿using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SP.User.Service
{
    public class CryptographyUtils
    {
        static public (string, byte[]) Encrypt(string password, byte[]? salt = null)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return (hashed, salt);
        }

        static public bool IsPasswordCorrect(string password, string hashedPassword, byte[] salt)
        {
            var (hashed, _) = Encrypt(password, salt);
            return hashed.Equals(hashedPassword);
        }
    }
}
