using System;
using System.ComponentModel.DataAnnotations;
using Application.Attributes;

namespace Application.DTOs.PaymentsDto
{
    public class StudentPaymentDto
    {
        [Required(ErrorMessage = "Boshlanish sanasi kiritilishi shart.")]
        [Display(Name = "Boshlanish sanasi")]
        public DateOnly BeginDate { get; set; }

        [Required(ErrorMessage = "Tugash sanasi kiritilishi shart.")]
        [Display(Name = "Tugash sanasi")]
        [CompareDate(
            nameof(BeginDate),
            ErrorMessage = "Tugash sanasi boshlanish sanasidan keyin bo‘lishi kerak."
        )]
        public DateOnly EndDate { get; set; }

        [Required(ErrorMessage = "To‘lov sikli ID kiritilishi shart.")]
        [Display(Name = "To‘lov sikli ID")]
        public int GroupStudentPaymentSycleId { get; set; }

        [Required(ErrorMessage = "To‘lov summasi kiritilishi shart.")]
        [Range(0, int.MaxValue, ErrorMessage = "To‘lov summasi manfiy bo‘lishi mumkin emas.")]
        [Display(Name = "To‘lov summasi")]
        public int Amount { get; set; }
    }
}
