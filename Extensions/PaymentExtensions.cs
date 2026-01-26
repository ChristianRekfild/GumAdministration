using GumAdministration.Dto;
using GumAdministration.Model;

namespace GumAdministration.Extensions;

public static class PaymentExtensions
{
    /// <summary>
    /// При возврате оплаты как DTO мы кастим UTC время в LocalTime (ибо постгря!)
    /// </summary>
    public static PaymentDto ToDto(this Payment payment)
    {
        var dto = new PaymentDto()
        {
            Id = payment.Id,
            ClientId = payment.ClientId,
            CreatedAt = payment.CreatedAt.ToUniversalTime(),
            AddedPersonalLessons = payment.AddedPersonalLessons
        };

        return dto;
    }

    public static Payment ToPayment(this PaymentDto dto)
    {
        var payment = new Payment()
        {
            Id = dto.Id,
            ClientId = dto.ClientId,
            CreatedAt = dto.CreatedAt.ToLocalTime(),
            AddedPersonalLessons = dto.AddedPersonalLessons
        };

        return payment;
    }
}