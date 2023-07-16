using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.ExtensionMethods
{
    public class HashingPassword
    {
        public string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = new byte[16]; // 16 bytes for the salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes); // Generate a random salt
            }

            salt = Convert.ToBase64String(saltBytes); // Convert salt to a Base64 string for storage

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000)) // 10000 iterations
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for the hash (256 bits)
                return Convert.ToBase64String(hashBytes); // Convert hash to a Base64 string for storage
            }
        }

        public bool VerifyPassword(string hashedPassword, string salt, string password)
        {
            byte[] saltBytes = Convert.FromBase64String(salt); // Convert Base64 salt back to byte array

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000)) // 10000 iterations
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for the hash (256 bits)
                string passwordHash = Convert.ToBase64String(hashBytes); // Convert hash to a Base64 string for comparison
                return passwordHash == hashedPassword;
            }
        }

        //string salt;
        //string hashedPassword = _passwordService.HashPassword(model.Password, out salt);
    }
}
