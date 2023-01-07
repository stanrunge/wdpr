using System.Security.Cryptography;
using backend.Models;
using Microsoft.AspNetCore.Identity;

public static class AccountDataInitializer
{
    public static void SeedData(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            var roleResult = roleManager.CreateAsync(role).Result;
        }
    }

    private static void SeedUsers(UserManager<Account> userManager)
    {
        if (userManager.FindByNameAsync("MainAdmin").Result == null)
        {
            Console.WriteLine(HashPassword("Pass123!"));
            Account a = new Account()
            {
                UserName = "MainAdmin",
                PasswordHash = HashPassword("Pass123!"),
                Email = "admin@testmail.com",
            };
            var result = userManager.CreateAsync(a).Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(a, "Admin").Wait();
            }
        }
    }

    // Move somewhere else in future and call that instead of this
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