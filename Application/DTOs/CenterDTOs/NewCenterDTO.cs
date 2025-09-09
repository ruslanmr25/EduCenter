using System;
using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Domain.Entities;

namespace Application.DTOs.CenterDTOs;

public class NewCenterDTO
{
    [Required(ErrorMessage = "Nom kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Nom uzunligi 100 ta belgidan oshmasligi kerak.")]
    [MinLength(2, ErrorMessage = "Nom kamida 2 ta belgidan iborat boʻlishi kerak.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "AdminId kiritilishi shart.")]
    [Range(1, int.MaxValue, ErrorMessage = "AdminId 0 dan katta bo‘lishi kerak.")]
    [Exsist(typeof(User), "Id", ErrorMessage = "Berilgan Id bazada mavjud emas")]
    public int CenterAdminId { get; set; }
}
