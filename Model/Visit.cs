using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GumAdministration.Model;

public class Visit
{
    /// <summary>Id</summary>
    [Key]
    public long Id {  get; set; }
    
    /// <summary>Связанный клиент</summary>
    [ForeignKey("ClientId")]
    public required Client Client {  get; set; }
    
    /// <summary>Дата и время начала</summary>
    public required DateTime Start { get; set; }
}