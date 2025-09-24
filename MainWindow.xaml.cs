using System.Windows;
using GumAdministration.ViewModel;

namespace GumAdministration;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}