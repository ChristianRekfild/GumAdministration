using System.Windows;
using GumAdministration.Infrastructure.Commands.Base;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>
/// Команда для закрытия приложения
/// </summary>
internal class CloseApplicationCommand : CommandBase
{

    internal CloseApplicationCommand()
    {
    }

    public override bool CanExecute(object? parameter)
        => true; // Всегда можем вызвать

    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}
