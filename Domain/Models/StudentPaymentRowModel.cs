using Domain.Entities;

namespace Domain.Models;

public class StudentPaymentRowModel
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;

    public List<StudentPayment> StudentPayments { get; set; } = new List<StudentPayment>();

    public DateTime JoinedAt { get; set; }
}
