using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GumAdministration.Infrastructure.Commands;
using GumAdministration.Model;
using GumAdministration.Services;
using Upz.Cms.DesktopClient.ViewModel.Base;

namespace GumAdministration.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Client> AllClients { get; set; }
    public ICollectionView FilteredClients { get; set; }
    private readonly ClientService _clientService;

    public MainWindowViewModel(ClientService clientService)
    {
        _clientService = clientService;
        AllClients = new ObservableCollection<Client>(_clientService.GetAll().GetAwaiter().GetResult());
        FilteredClients = CollectionViewSource.GetDefaultView(AllClients);
        FilteredClients.Filter = FilterClients;

        CloseApplicationCommand = new CloseApplicationCommand();
    }


    #region Свойства

    private string _searchString;

    /// <summary>Поиск по имени</summary>
    public string SearchString
    {
        get => _searchString;
        set
        {
            Set(ref _searchString, value);
            ApplyFilter();
        }
    }

    private bool _showRequiringPayment;

    /// <summary>Показать лиц, от которых требуется оплата</summary>
    public bool ShowRequiringPayment
    {
        get => _showRequiringPayment;
        set => Set(ref _showRequiringPayment, value);
    }

    private bool _showHidden;

    /// <summary>Показать скрытых</summary>
    public bool ShowHidden
    {
        get => _showHidden;
        set => Set(ref _showHidden,  value);
    }

    #endregion

    #region Команды

    public ICommand CloseApplicationCommand { get; }

    #endregion Команды

    private bool FilterClients(object item)
    {
        // Вроде как в этом случае мы просто возвращаем всё. Нужно потестить.
        if (string.IsNullOrWhiteSpace(SearchString)) return true; 
        
        // var client = item as Client;
        if (item is Client client)
        return (client.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) ||
                client.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                client.Patronymic.Contains(SearchString, StringComparison.OrdinalIgnoreCase);
        // Добавьте условия для остальных полей...
        
        return false;
    }

    private void ApplyFilter()
    {
        FilteredClients.Refresh();
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        this.ApplyFilter();
    }
}