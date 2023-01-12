using System.Security.Cryptography;

namespace webapi.Data;

public static class DataEditor
{
    public static string HashPassword(string unHashedPassword)
    {
        using (SHA384 sha384Hash = SHA384.Create())
        {
            byte[] unHashedBytes = System.Text.Encoding.UTF8.GetBytes(unHashedPassword);
            byte[] hashedBytes = sha384Hash.ComputeHash(unHashedBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }
    }
}