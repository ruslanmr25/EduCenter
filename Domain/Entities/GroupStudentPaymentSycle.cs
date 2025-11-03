using System;

namespace Domain.Entities;

public class GroupStudentPaymentSycle : BaseEntity
{
    public int Id { get; set; }

    public bool IsActive { get; set; } = true;
    public int StudentId { get; set; }

    public Student Student { get; set; }

    public int GroupId { get; set; }

    public Group Group { get; set; }

    public decimal Price { get; set; }

    public List<StudentPayment> StudentPayments { get; set; }
}
