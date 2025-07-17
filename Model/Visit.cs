using System.ComponentModel.DataAnnotations;

namespace GumAdministration.Model;

public class Visit
{
    [Key]
    /// <summary> Id </summary>
    public long Id {  get; set; }
    
    /// <summary> Id клиента </summary>
    public Client ClientId {  get; set; }
    /// <summary> Дата и время начала </summary>
    public DateTime Start { get; set; }
}