namespace webapi.Models;

public class Medewerker : Account
{
    public int Id { get; set; }
    public string Functie { get; set; }
}