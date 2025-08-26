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

    private string _searchFirstName;

    /// <summary>Поиск по имени</summary>
    public string SearchFirstName
    {
        get => _searchFirstName;
        set
        {
            Set(ref _searchFirstName, value);
            ApplyFilter();
        }
    }

    private string _searchLastName;

    /// <summary>Поиск по фамилии</summary>
    public string SearchLastName
    {
        get => _searchLastName;
        set
        {
            Set(ref _searchLastName, value);
            ApplyFilter();
        }
    }

    private string _searchPatronymic;

    /// <summary>Поиск по отчеству</summary>
    public string SearchPatronymic
    {
        get => _searchPatronymic;
        set
        {
            Set(ref _searchPatronymic, value);
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
        // var client = item as Client;
        if (item is Client client)
        return (string.IsNullOrEmpty(SearchFirstName) ||
                client.FirstName.Contains(SearchFirstName, StringComparison.OrdinalIgnoreCase)) &&
               (string.IsNullOrEmpty(SearchLastName) ||
                client.LastName.Contains(SearchLastName, StringComparison.OrdinalIgnoreCase));
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