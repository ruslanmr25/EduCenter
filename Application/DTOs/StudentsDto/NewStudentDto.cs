using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.StudentsDto;

public class NewStudentDto
{
    [Required(ErrorMessage = "Ism va familiya kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Ism va familiya uzunligi 100 ta belgidan oshmasin.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon raqam kiritilishi shart.")]
    //     [Phone(ErrorMessage = "Telefon raqam noto‘g‘ri formatda.")]
    [StringLength(15, ErrorMessage = "Telefon raqam uzunligi 15 ta belgidan oshmasin.")]
    public string PhoneNumber { get; set; } = string.Empty;

    //     [Phone(ErrorMessage = "Telefon raqam noto‘g‘ri formatda.")]
    [StringLength(15, ErrorMessage = "Ikkinchi telefon raqam uzunligi 15 ta belgidan oshmasin.")]
    public string SecondPhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Guruhlar ro‘yxati bo‘sh bo‘lishi mumkin emas.")]
    [MinLength(1, ErrorMessage = "Kamida bitta guruh tanlanishi shart.")]
    public List<int> Groups { get; set; } = new();
}
