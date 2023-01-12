using System.ComponentModel.DataAnnotations;

namespace webapi.Models;

public class AccountRegistreer
{
    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
}