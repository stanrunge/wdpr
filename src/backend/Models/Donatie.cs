namespace backend.Models;

public class Donatie
{
    public int Id { get; set; }
    public double Bedrag { get; set; }
    public DateTime TijdStempel { get; set; }
    
    public Donateur Donateur { get; set; }
}