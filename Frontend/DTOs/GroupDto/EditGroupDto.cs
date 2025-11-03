using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Frontend.DTOs.GroupDto;

public class EditGroupDto
{
    [Required(ErrorMessage = "Guruh nomi kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Nom 100 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Guruh narxi kiritilishi kerak")]
    [Range(10000, int.MaxValue, ErrorMessage = "Musbat son bo‘lishi kerak.")]
    public int GroupPrice { get; set; }

    [Required(ErrorMessage = "Fan tanlanishi kerak.")]
    public int ScienceId { get; set; }

    [Required(ErrorMessage = "O‘qituvchi tanlanishi kerak.")]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "Dars kunlari tanlanishi kerak.")]
    public List<DaysOfWeek> Days { get; set; } = [];

    [Required(ErrorMessage = "Dars vaqtlari tanlanishi kerak.")]
    public List<TimeOnly> Times { get; set; } = [];
}
