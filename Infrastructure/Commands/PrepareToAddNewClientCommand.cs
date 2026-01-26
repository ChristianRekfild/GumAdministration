using System.Windows;
using GumAdministration.Dto;
using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.Model;
using GumAdministration.ViewModel;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для закрытия детального представления клиента</summary>
internal class PrepareToAddNewClientCommand : CommandBase
{
    private readonly MainViewModel _viewModel;
    
    internal PrepareToAddNewClientCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
        => !_viewModel.IsDetailsMode;

    public override void Execute(object? parameter)
    {
        _viewModel.SelectedClient = new ClientDto() { BirthDate = DateTime.Today, CreatedAt = DateTime.Now };
        // _viewModel.SelectedClient.BirthDate = DateTime.Now;
        _viewModel.IsDetailsMode = true;
    }
}
