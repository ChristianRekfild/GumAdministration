using System.ComponentModel.DataAnnotations;

namespace GumAdministration.Model;

public class Visit
{
    /// <summary>Id</summary>
    [Key]
    public long Id {  get; set; }
    
    /// <summary>Id клиента</summary>
    public required Client ClientId {  get; set; }
    
    /// <summary>Дата и время начала</summary>
    public required DateTime Start { get; set; }
}