using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Model;

public sealed class Context : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public Context(DbContextOptions<Context> options)  : base(options)
    {
        // Создание БД, если она не существует
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql();
    }
}