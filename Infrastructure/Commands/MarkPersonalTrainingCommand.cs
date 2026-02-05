using GumAdministration.Dto;
using GumAdministration.Infrastructure.Commands.Base;
using GumAdministration.Model;
using GumAdministration.Services;
using GumAdministration.ViewModel;

namespace GumAdministration.Infrastructure.Commands;

/// <summary>Команда для закрытия приложения</summary>
internal class MarkPersonalTrainingCommand(MainViewModel vm) : CommandBase
{
    public override bool CanExecute(object? parameter)
    {
        if (parameter is not ClientDto client)
            return false;
        
        if (client.IsPersonalTraining)
            return true;
        
        return false;
    }

    public async override void Execute(object? parameter)
    {
        if (parameter is ClientDto client)
        {
            var now = DateTime.Now;
            var beginningOfTheСurrentВay = new DateTime(now.Year, now.Month, now.Day);
            
            var existedVisit = await vm.visitService.SelectFirst(x => x.Client.Id == client.Id 
                                                                && x.Start >= beginningOfTheСurrentВay);

            // Если сегодня отмечена перс. тренировка - то нельзя поставить ещё одну отметку. Фигня какая-то получится.
            if (existedVisit is not null)
            {
                System.Windows.MessageBox.Show(
                    "Невозможно отметить персональную тренировку\n(так как пользователь уже отмечен сегодня)");
                return;
            }
            
            await vm.visitService.Add(client.Id, DateTime.UtcNow);
        }
    }
}