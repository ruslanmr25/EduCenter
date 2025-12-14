using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace UI.DTOs;

public class CreateGroupDto
{
    [Required(ErrorMessage = "Guruh nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Nom 100 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fan tanlanishi kerak.")]
    [Range(1, int.MaxValue, ErrorMessage = "Fan tanlanishi kerak.")]
    public int ScienceId { get; set; }

    [Required(ErrorMessage = "Guruh narxi kiritilishi kerak")]
    [Range(1, int.MaxValue, ErrorMessage = "Musbat son bo‘lishi kerak.")]
    public int GroupPrice { get; set; }

    [Required(ErrorMessage = "O‘qituvchi tanlanishi kerak.")]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "Dars kunlari tanlanishi kerak.")]
    [MinLength(1, ErrorMessage = "Hech bo'lmaganda bir kun tanlanishi kerak")]
    public IEnumerable<DaysOfWeek> Days { get; set; } = new List<DaysOfWeek>();

    [Required(ErrorMessage = "Dars vaqtlari tanlanishi kerak.")]
    public List<TimeOnly> Times { get; set; } = new List<TimeOnly>();
}
