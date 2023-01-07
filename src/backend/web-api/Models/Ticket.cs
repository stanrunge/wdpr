namespace webapi.Models;

public class Ticket
{
    public int Id { get; set; }
    
    public Account Klant { get; set; }
    
    public Uitvoering Uitvoering { get; set; }
}