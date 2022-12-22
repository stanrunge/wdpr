namespace backend.Models;

public class Ticket
{
    public int Id { get; set; }
    
    public TicketKoper Klant { get; set; }
    
    public Uitvoering Uitvoering { get; set; }
}