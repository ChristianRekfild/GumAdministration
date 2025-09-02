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
        => parameter is Client client;

    public async override void Execute(object? parameter)
    {
        if (parameter is Client client)
        {
            Visit visit = new Visit()
            {
                Client = client,
                Start = DateTime.UtcNow
            };
            
            await _visitService.Add(visit);
        }
        
    }
    
}
