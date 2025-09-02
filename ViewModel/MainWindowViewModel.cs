using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
    private readonly VisitService _visitService;

    public MainWindowViewModel(ClientService clientService, VisitService visitService)
    {
        _clientService = clientService;
        _visitService = visitService;

        AllClients = new ObservableCollection<Client>();
        FilteredClients = CollectionViewSource.GetDefaultView(AllClients);
        FilteredClients.Filter = FilterClients;

        CloseApplicationCommand = new CloseApplicationCommand();
        MarkPersonalTrainingCommand = new MarkPersonalTrainingCommand(_visitService);

        LoadClientsAsync();
    }


    #region Свойства

    private Client? _selectedClient;

    /// <summary>Выбранный клиент на DataGrid</summary>
    public Client? SelectedClient
    {
        get => _selectedClient;
        set => Set(ref _selectedClient, value);
    }

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
        set
        {
            Set(ref _showHidden, value);
            ApplyFilter();
        }
    }

    private string _status = "Загрузка";

    /// <summary>Статус работы программы</summary>
    public string Status
    {
        get => _status;
        set
        {
            Set(ref _status, value);
            ApplyFilter();
        }
    }

    #endregion

    #region Команды

    public ICommand CloseApplicationCommand { get; }
    public ICommand MarkPersonalTrainingCommand { get; }

    #endregion Команды

    private bool FilterClients(object item)
    {
        if (item is not Client client)
            return false;

        bool matchesSearch = string.IsNullOrWhiteSpace(SearchString) ||
                             client.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                             client.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                             client.Patronymic.Contains(SearchString, StringComparison.OrdinalIgnoreCase);

        bool isVisible = ShowHidden || !client.Hidden;
        
        bool result = matchesSearch && isVisible;
        return result;
    }

    /// <summary>Применение фильтра</summary>
    private void ApplyFilter()
        => FilteredClients.Refresh();

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        this.ApplyFilter();
    }

    private async void LoadClientsAsync()
    {
        try
        {
            var clients = await _clientService.GetAll();
            foreach (var c in clients)
                AllClients.Add(c);

            // FilteredClients.Refresh();
            this.Status = "Ок";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}