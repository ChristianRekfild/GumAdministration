using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Model;

public class Client
{
    /// <summary> Id </summary>
    [Key]
    public long Id { get; set; }

    /// <summary> Имя </summary>
    public string FirstName { get; set; }

    /// <summary> Фамилия </summary>
    public string LastName { get; set; }

    /// <summary> Отчество </summary>
    public string Patronymic { get; set; }

    /// <summary> Номер телефона </summary>
    public string? PhoneNumber { get; set; }

    /// <summary> Дата рождения </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary> Дата создания </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary> Оплата по модели персональной тренировки? True - да, будет идти расчёт по кол-ву занятий
    /// Нет - будёт идти расчёт по времени, которое "оплачено" абонементом</summary>
    public bool IsPersonalTraining { get; set; }

    /// <summary>Сколько осталось персональных занятий</summary>
    public int PersonalLessonsLeft { get; set; }

    /// <summary>Последняя оплаченная дата (в случае не персональных тренировок)</summary>
    public DateTime? LastPaidDate { get; set; }

    /// <summary> Нужна оплата </summary>
    public bool PaymentRequired { get; set; }
}