using System.Windows;
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
    }

    private void ConfigureServices(IServiceCollection collection)
    {
        
    }
}