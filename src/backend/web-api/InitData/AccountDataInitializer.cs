using webapi.Models;
using webapi.Data;
using Microsoft.AspNetCore.Identity;

namespace webapi.InitData;

public static class AccountDataInitializer
{
    public static void SeedData(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
    {
        SeedRoles(roleManager);
        SeedUsers(userManager);
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        // IdentityRole role;
        if (!roleManager.RoleExistsAsync(Roles.Admin).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Admin;
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync(Roles.Medewerker).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Medewerker;
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync(Roles.Directie).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Directie;
            roleManager.CreateAsync(role).Wait();
        }
        if (!roleManager.RoleExistsAsync(Roles.Donateur).Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = Roles.Donateur;
            roleManager.CreateAsync(role).Wait();
        }


    }

    private static void SeedUsers(UserManager<Account> userManager)
    {
        if (userManager.FindByNameAsync("testadmin@testmail.com").Result == null)
        {
            Account a = new Account()
            {
                UserName = "testadmin@testmail.com",
                // Password = DataEditor.HashPassword("Pass123!")
            };
            var result = userManager.CreateAsync(a, "Pass123!").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(a, Roles.Admin).Wait();
            }
        }
    }

}