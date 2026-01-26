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

    public async override void Execute(object? parameter)
    {
        if (_viewModel.SelectedClient is null) 
            return;

        var client = await _viewModel.clientService.Get(_viewModel.SelectedClient.Id);

        if (client is not null)
        {
            await _viewModel.clientService.Update(_viewModel.SelectedClient);
            return;
        }
        
        // _viewModel.SelectedClient.CreatedAt = DateTime.Now;
        
        var addedClient =  await _viewModel.clientService.Add(_viewModel.SelectedClient);
        _viewModel.SelectedClient = addedClient;
    }
}