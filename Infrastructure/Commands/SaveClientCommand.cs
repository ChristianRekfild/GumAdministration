using System.Windows;
using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.ViewModel;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для закрытия детального представления клиента</summary>
internal class SaveClientCommand : CommandBase
{
    private readonly MainViewModel _viewModel;
    
    internal SaveClientCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
        => _viewModel.IsDetailsMode && _viewModel.SelectedClient is not null;

    public override void Execute(object? parameter)
    {
        if (_viewModel.SelectedClient is null) return;
        
        _viewModel.clientService.Update(_viewModel.SelectedClient);
    }
}
