using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Model;

public sealed class Context : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public Context()
    {
        // Создание БД, если она не существует
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Gym.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}