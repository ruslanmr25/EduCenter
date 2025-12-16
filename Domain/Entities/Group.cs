using Domain.Enums;

namespace Domain.Entities;

public class Group : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal GroupPrice { get; set; }

    public Center? Center { get; set; }

    public int CenterId { get; set; }

    public Science? Science { get; set; }

    public int ScienceId { get; set; }

    public User? Teacher { get; set; }

    public int TeacherId { get; set; }
    public bool IsActive { get; set; } = true;

    public List<DaysOfWeek> Days { get; set; } = [];

    public List<TimeOnly> Times { get; set; } = [];

    public List<GroupStudentPaymentSycle> GroupStudentPaymentSycles { get; set; } = [];
}
