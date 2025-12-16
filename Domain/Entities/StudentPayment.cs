namespace Domain.Entities;

public class StudentPayment : BaseEntity
{
    public int Id { get; set; }

    public DateTime BeginDate { get; set; }

    public DateTime EndDate { get; set; }

    public GroupStudentPaymentSycle? GroupStudentPaymentSycle { get; set; }

    public int GroupStudentPaymentSycleId { get; set; }

    public int Amount { get; set; }
}
