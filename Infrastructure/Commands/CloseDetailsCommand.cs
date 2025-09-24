using System.Windows;
using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.ViewModel;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для закрытия детального представления клиента</summary>
internal class CloseDetailsCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    
    internal CloseDetailsCommand(MainViewModel viewModel)
    {
        _mainViewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
        => _mainViewModel.IsDetailsMode;

    public override void Execute(object? parameter)
    {
        _mainViewModel.IsDetailsMode = false;
    }
}
