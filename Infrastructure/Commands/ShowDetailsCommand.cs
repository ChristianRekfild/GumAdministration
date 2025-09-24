using System.Windows;
using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.ViewModel;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для открытия детального представления клиента</summary>
internal class ShowDetailsCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    internal ShowDetailsCommand(MainViewModel viewModel)
    {
        _mainViewModel = viewModel;
    }

    public override bool CanExecute(object? parameter)
        => _mainViewModel.SelectedClient is not null;

    public override void Execute(object? parameter)
    {
        if (_mainViewModel.SelectedClient is not null)
            _mainViewModel.IsDetailsMode = true;
    }
}