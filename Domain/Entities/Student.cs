namespace Domain.Entities;

public class Student : BaseEntity
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string SecondPhoneNumber { get; set; } = string.Empty;

    public List<GroupStudentPaymentSycle> GroupStudentPaymentSycles { get; set; } = [];

    public int CenterId { get; set; }

    public Center? Center { get; set; }
}
