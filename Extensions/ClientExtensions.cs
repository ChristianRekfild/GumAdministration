using GumAdministration.Dto;
using GumAdministration.Model;

namespace GumAdministration.Extensions;

public static class ClientExtensions
{
    /// <summary>
    /// При возврате клиента как DTO мы кастим UTC время в LocalTime (ибо постгря!)
    /// </summary>
    public static ClientDto ToDto(this Client client)
    {
        var dto = new ClientDto()
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Patronymic = client.Patronymic,
            PhoneNumber = client.PhoneNumber,
            IsPersonalTraining = client.IsPersonalTraining,
            PersonalLessonsLeft = client.PersonalLessonsLeft,
            PaymentRequired = client.PaymentRequired,
            Hidden = client.Hidden,

            BirthDate = client.BirthDate?.ToLocalTime(),
            CreatedAt = client.CreatedAt.ToLocalTime(),
            LastPaidDate = client.LastPaidDate?.ToLocalTime(),
        };
        
        return dto;
    }

    public static Client ToClient(this ClientDto dto)
    {
        var client = new Client()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            PhoneNumber = dto.PhoneNumber,
            IsPersonalTraining = dto.IsPersonalTraining,
            PersonalLessonsLeft = dto.PersonalLessonsLeft,
            PaymentRequired = dto.PaymentRequired,
            Hidden = dto.Hidden,

            BirthDate = dto.BirthDate?.ToUniversalTime(),
            CreatedAt = dto.CreatedAt.ToUniversalTime(),
            LastPaidDate = dto.LastPaidDate?.ToUniversalTime(),
        };
        
        return client;
    }
}