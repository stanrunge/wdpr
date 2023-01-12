using System.ComponentModel.DataAnnotations;

namespace webapi.Models;

public class MedewerkerRegistreer : AccountRegistreer
{
    [Required(ErrorMessage = "Functie is required")]
    public string? Functie { get; init; }
}