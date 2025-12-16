namespace Frontend.DTOs.Payments;

public class StudentPaymentDto
{
    public DateOnly BeginDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int GroupStudentPaymentSycleId { get; set; }

    public int Amount { get; set; }
}
