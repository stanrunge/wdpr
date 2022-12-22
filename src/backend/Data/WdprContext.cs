using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class WdprContext : DbContext
{
    public WdprContext(DbContextOptions<WdprContext> options)
        : base(options)
    {
    }
    
    public DbSet<Artiest> Artiesten { get; set; }
    public DbSet<Donateur> Donateurs { get; set; }
    public DbSet<Donatie> Donaties { get; set; }
    public DbSet<Klant> Klanten { get; set; }
    public DbSet<Medewerker> Medewerkers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Uitvoering> Uitvoeringen { get; set; }
    public DbSet<Voorstelling> Voorstellingen { get; set; }
    public DbSet<Zaal> Zalen { get; set; }
    public DbSet<Zitplaats> Zitplaatsen { get; set; }
    
}