using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.Model;
using GumAdministration.Services;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для закрытия приложения</summary>
internal class MarkPersonalTrainingCommand : CommandBase
{
    private readonly VisitService _visitService;

    internal MarkPersonalTrainingCommand(VisitService visitService)
    {
        _visitService = visitService;
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter is not Client client)
            return false;
        
        if (client.IsPersonalTraining)
            return true;
        
        return false;
    }

    public async override void Execute(object? parameter)
    {
        if (parameter is Client client)
        {
            Visit visit = new Visit()
            {
                Client = client,
                Start = DateTime.UtcNow
            };

            DateTime startDay = DateTime.Today.ToUniversalTime();
            DateTime endDay = startDay.AddDays(1).AddSeconds(-1).ToUniversalTime();

            Visit? existedVisit = await _visitService.SelectFirst(x =>
                    x.Start > startDay && x.Start < endDay
                                       && x.Client == client);

            if (existedVisit is not null)
            {
                string error = $"""
                                Невозможно добавить визит, т.к. данный клиент
                                ({client.FirstName} {client.LastName} {client.Patronymic})
                                уже приходил сегодня.
                                """;
                System.Windows.MessageBox.Show(error, "ошибка",  System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }

            await _visitService.Add(visit);
        }
    }
}