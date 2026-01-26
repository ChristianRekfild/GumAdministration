namespace GumAdministration.Dto;

public class PaymentDto
{
    /// <summary>Id</summary>
    public long Id {  get; set; }
    
    /// <summary>Id клиента, кто оплатил</summary>
    public long ClientId {get; set; }
    
    /// <summary>Дата создания</summary>
    public required DateTime CreatedAt { get; set; }
    
    /// <summary>Количество добавленных занятий</summary>
    public int? AddedPersonalLessons { get; set; }
}