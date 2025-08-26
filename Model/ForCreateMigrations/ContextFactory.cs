using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GumAdministration.Model.ForCreateMigrations;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        string currDir = AppDomain.CurrentDomain.BaseDirectory;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(currDir) // Указываем путь к проекту
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Строка подключения не найдена в файле appsettings.json.");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        optionsBuilder.UseNpgsql(connectionString); // Используй UseNpgsql, UseSqlite и т.д. если нужно

        return new Context(optionsBuilder.Options);
        
    }
}