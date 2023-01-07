using webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace webapi.Data;

public class WdprContext : IdentityDbContext<Account, IdentityRole, string>
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
    public DbSet<Account> Account { get; set; } = default!;

}