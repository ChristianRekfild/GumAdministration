using System.Windows.Input;
using GumAdministration.Infrastructure.Commands;
using Upz.Cms.DesktopClient.ViewModel.Base;

namespace GumAdministration.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        CloseApplicationCommand = new CloseApplicationCommand();
    }


    #region Команды
    
    public ICommand CloseApplicationCommand { get; }

    #endregion Команды


}
