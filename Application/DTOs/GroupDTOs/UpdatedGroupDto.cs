using System;
using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.GroupDTOs;

public class UpdatedGroupDto
{
    [StringLength(100, ErrorMessage = "Nom uzunligi 100 ta belgidan oshmasligi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Guruh narxi kiritilishi kerak")]
    [Range(10000, int.MaxValue, ErrorMessage = "Musbat son bo‘lishi kerak.")]
    public int GroupPrice { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "ScienceId musbat son bo‘lishi kerak.")]
    [Exsist<Science>]
    public int ScienceId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "TeacherId musbat son bo‘lishi kerak.")]
    [Exsist<User>]
    public int TeacherId { get; set; }

    [MinLength(1, ErrorMessage = "Hech bo‘lmaganda bitta kun tanlanishi kerak.")]
    public List<DaysOfWeek> Days { get; set; } = new();

    [Required(ErrorMessage = "Dars vaqtlari kiritilishi shart.")]
    [MinLength(1, ErrorMessage = "Hech bo‘lmaganda bitta vaqt bo‘lishi kerak.")]
    public List<TimeOnly> Times { get; set; } = new();
}
