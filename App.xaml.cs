using System.Windows;
using GumAdministration.Repositories;
using GumAdministration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GumAdministration;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        
        MainWindow = new MainWindow();
        MainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection collection)
    {
        collection.AddSingleton<ClientRepository>();
        collection.AddSingleton<ClientService>();
    }
}