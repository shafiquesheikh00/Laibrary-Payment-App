using System;
using System.Security.Cryptography;
using System.Text;

namespace Laibrary_Payment_App.Security
{
    public static class PasswordSecurity
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            // 1. Check SHA-256 hash match
            var inputHash = HashPassword(inputPassword);
            if (string.Equals(inputHash, storedHash, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // 2. Fallback for plain-text comparison (e.g. if manually entered in DB)
            if (string.Equals(inputPassword, storedHash, StringComparison.Ordinal))
            {
                return true;
            }

            // 3. Fallback compatibility for development administrator credentials (admin / Admin@123)
            var admin123Hash = HashPassword("Admin@123");
            var adminPlainHash = HashPassword("admin");

            if ((inputPassword == "Admin@123" || inputPassword == "admin") &&
                (string.Equals(storedHash, admin123Hash, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(storedHash, adminPlainHash, StringComparison.OrdinalIgnoreCase) ||
                 storedHash == "Admin@123" || storedHash == "admin"))
            {
                return true;
            }

            return false;
        }
    }
}
