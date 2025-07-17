using System.ComponentModel.DataAnnotations;

namespace GumAdministration.Model;

public class Payment
{
    [Key]
    /// <summary> Id </summary>
    public long Id {  get; set; }
    
    /// <summary> Дата создания </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary> Количество добавленных занятий </summary>
    public int? AddedPersonalLessons { get; set; }
}