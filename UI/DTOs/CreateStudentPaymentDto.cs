using System.ComponentModel.DataAnnotations;

namespace UI.DTOs
{
    public class CreateStudentPaymentDto
    {
        [Required(ErrorMessage = "Boshlanish sanasi majburiy.")]
        [DataType(DataType.Date, ErrorMessage = "Boshlanish sanasi noto‘g‘ri formatda.")]
        public DateTime? BeginDate { get; set; }

        [Required(ErrorMessage = "Tugash sanasi majburiy.")]
        [DataType(DataType.Date, ErrorMessage = "Tugash sanasi noto‘g‘ri formatda.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Guruh-student to‘lov sikli ID majburiy.")]
        [Range(1, int.MaxValue, ErrorMessage = "Guruh-student to‘lov sikli ID noto‘g‘ri.")]
        public int GroupStudentPaymentSycleId { get; set; }

        [Required(ErrorMessage = "To‘lov summasi majburiy.")]
        [Range(1, int.MaxValue, ErrorMessage = "To‘lov summasi 0 dan katta bo‘lishi kerak.")]
        public int Amount { get; set; }
    }
}
