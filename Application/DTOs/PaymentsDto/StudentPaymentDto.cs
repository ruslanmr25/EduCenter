using System;
using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Entities;

namespace Application.DTOs.PaymentsDto
{
    public class StudentPaymentDto
    {
        [Required(ErrorMessage = "Boshlanish sanasi kiritilishi shart.")]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "Tugash sanasi kiritilishi shart.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "To‘lov sikli ID kiritilishi shart.")]
        [Exsist<GroupStudentPaymentSycle>]
        public int GroupStudentPaymentSycleId { get; set; }

        [Required(ErrorMessage = "To‘lov summasi kiritilishi shart.")]
        [Range(0, int.MaxValue, ErrorMessage = "To‘lov summasi manfiy bo‘lishi mumkin emas.")]
        public int Amount { get; set; }
    }
}
