namespace backend.Models;

public class Uitvoering
{
    public int Id { get; set; }
    public DateTime BeginTijd { get; set; }
    public DateTime EindTijd { get; set; }
    
    public Voorstelling Voorstelling { get; set; }
}