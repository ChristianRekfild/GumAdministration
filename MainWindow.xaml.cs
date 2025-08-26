using System.Windows;
using GumAdministration.ViewModel;

namespace GumAdministration;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;
    
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.DataContext = _viewModel;
    }
}