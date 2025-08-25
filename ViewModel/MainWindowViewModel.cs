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

    private string _searchFirstName;
    private string _searchLastName;
    private string _searchPatronymic;
    private string _searchEmail;

    /// <summary>Поиск по имени</summary>
    public string SearchFirstName
    {
        get => _searchFirstName;
        set
        {
            _searchFirstName = value;
            ApplyFilter();
        }
    }

    /// <summary>Поиск по фамилии</summary>
    public string SearchLastName
    {
        get => _searchLastName;
        set
        {
            _searchLastName = value;
            ApplyFilter();
        }
    }
    
    /// <summary>Поиск по отчеству</summary>
    public string SearchPatronymic
    {
        get => _searchPatronymic;
        set
        {
            _searchPatronymic = value;
            ApplyFilter();
        }
    }

    #region Команды
    
    public ICommand CloseApplicationCommand { get; }

    #endregion Команды
    
    private bool FilterClients(object item)
    {
        var client = item as Client;
        return (string.IsNullOrEmpty(SearchFirstName) || client.FirstName.Contains(SearchFirstName, StringComparison.OrdinalIgnoreCase)) &&
               (string.IsNullOrEmpty(SearchLastName) || client.LastName.Contains(SearchLastName, StringComparison.OrdinalIgnoreCase));
        // Добавьте условия для остальных полей...
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
