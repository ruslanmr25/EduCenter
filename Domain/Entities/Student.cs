using System;

namespace Domain.Entities;

public class Student : BaseEntity
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string PhoneNumber { get; set; }

    public string SecondPhoneNumber { get; set; }

    public List<Group> Groups { get; set; }

    public List<GroupStudentPaymentSycle> GroupStudentPaymentSycles { get; set; }
}
