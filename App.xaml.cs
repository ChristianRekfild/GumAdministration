using System.IO;
using System.Windows;
using GumAdministration.Model;
using GumAdministration.Repositories;
using GumAdministration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GumAdministration;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    private IConfigurationRoot _configuration;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Устанавливаем базовый путь к текущему каталогу.
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Добавляем JSON-файл.
        
        try
        {
            _configuration = builder.Build();
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show($"Файл конфигурации 'appsettings.json' не найден. {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
            return;
        }
        catch (System.Exception ex)
        {
            MessageBox.Show($"Произошла ошибка при загрузке конфигурации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
            return;
        }
        
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        MainWindow = new MainWindow();
        MainWindow.Show();
    }

    /// <summary>Настраиваем нужные сервисы</summary>
    private void ConfigureServices(IServiceCollection collection)
    {
        collection.AddSingleton(_configuration);
        var connString = _configuration.GetConnectionString("DefaultConnection");
        collection.AddDbContext<Context>(options => options.UseNpgsql(connString));
        
        collection.AddSingleton<ClientRepository>();
        collection.AddSingleton<ClientService>();
    }

    private void SetConfigurationFromJson()
    {
        
        
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();    
    }
    
}