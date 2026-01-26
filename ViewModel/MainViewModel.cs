using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GumAdministration.Dto;
using GumAdministration.Extensions;
using GumAdministration.Infrastructure.Commands;
using GumAdministration.Model;
using GumAdministration.Services;
using Upz.Cms.DesktopClient.ViewModel.Base;

namespace GumAdministration.ViewModel;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<ClientDto> AllClients { get; set; }
    public ICollectionView FilteredClients { get; set; }

    public readonly ClientService clientService;
    public readonly VisitService visitService;

    public MainViewModel(ClientService clientService, VisitService visitService)
    {
        this.clientService = clientService;
        this.visitService = visitService;

        AllClients = new ObservableCollection<ClientDto>();
        FilteredClients = CollectionViewSource.GetDefaultView(AllClients);
        FilteredClients.Filter = FilterClients;

        CloseApplicationCommand = new CloseApplicationCommand();
        MarkPersonalTrainingCommand = new MarkPersonalTrainingCommand(this.visitService);

        ShowDetailsCommand = new ShowDetailsCommand(this);
        CloseDetailsCommand = new CloseDetailsCommand(this);
        SaveClientCommand = new SaveClientCommand(this);
        PrepareToAddNewClientCommand = new PrepareToAddNewClientCommand(this);

        LoadClientsAsync();
    }


    #region Свойства

    private bool _isDetailsMode;
    /// <summary>Нужно ли отображать детальную панель (по конкретному клиенту)</summary>
    public bool IsDetailsMode
    {
        get => _isDetailsMode;
        set => Set(ref _isDetailsMode, value);
    }    
    
    private ClientDto? _selectedClient;
    /// <summary>Выбранный клиент на DataGrid</summary>
    public ClientDto? SelectedClient
    {
        get => _selectedClient;
        set 
        {
            Set(ref _selectedClient, value);
        }
    }

    private string? _searchString;
    /// <summary>Поиск по имени</summary>
    public string? SearchString
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
        set
        {
            Set(ref _showRequiringPayment, value);
            ApplyFilter();
        }
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
    public ICommand ShowDetailsCommand { get; }
    public ICommand CloseDetailsCommand { get; }
    public ICommand SaveClientCommand { get; }
    public ICommand PrepareToAddNewClientCommand { get; }

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
        bool isRequiredPayment = ShowRequiringPayment ? client.PaymentRequired : true;
        bool result = matchesSearch && isVisible && isRequiredPayment;

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
            var clients = await clientService.GetAll();
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