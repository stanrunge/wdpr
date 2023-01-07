using Microsoft.AspNetCore.Identity;

namespace webapi.Models;

public class Account : IdentityUser
{
    public int Id { get; set; }
    public string Password { get; set; }
}