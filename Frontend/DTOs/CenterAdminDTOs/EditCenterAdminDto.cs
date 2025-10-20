using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.CenterAdminDTOs;

public class EditCenterAdminDto
{
    [Required(ErrorMessage = "To'liq ism kiritilishi shart")]
    [StringLength(100, ErrorMessage = "To'liq ism 100 ta belgidan oshmasligi kerak")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Foydalanuvchi nomi kiritilishi shart")]
    [StringLength(
        50,
        MinimumLength = 3,
        ErrorMessage = "Foydalanuvchi nomi 3–50 ta belgidan iborat bo‘lishi kerak"
    )]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol kiritilishi shart")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak"
    )]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
