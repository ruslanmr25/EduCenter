using Application.Attributes;
using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.GroupDTOs;

public class NewGroupDto
{
    [Required(ErrorMessage = "Nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Nom uzunligi 100 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "ScienceId kiritilishi shart.")]
    [Range(1, int.MaxValue, ErrorMessage = "ScienceId musbat son bo‘lishi kerak.")]
    [Exsist<Science>]
    public int ScienceId { get; set; }

    [Required(ErrorMessage = "TeacherId kiritilishi shart.")]
    [Range(1, int.MaxValue, ErrorMessage = "TeacherId musbat son bo‘lishi kerak.")]
    [Exsist<User>]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "Guruh narxi kiritilishi kerak")]
    [Range(10000, int.MaxValue, ErrorMessage = "Musbat son bo‘lishi kerak.")]
    public int GroupPrice { get; set; }

    [Required(ErrorMessage = "Kunlar kiritilishi shart.")]
    [MinLength(1, ErrorMessage = "Hech bo‘lmaganda bitta kun tanlanishi kerak.")]
    public List<DaysOfWeek> Days { get; set; } = new();

    [Required(ErrorMessage = "Dars vaqtlari kiritilishi shart.")]
    [MinLength(1, ErrorMessage = "Hech bo‘lmaganda bitta vaqt bo‘lishi kerak.")]
    public List<TimeOnly> Times { get; set; } = new();
}
