using System;
using System.ComponentModel.DataAnnotations;

namespace UI.DTOs;

public class CreateTeacherDto
{
    [Required(ErrorMessage = "To‘liq ism kiritilishi shart.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "Ism uzunligi 3 dan 100 tagacha bo‘lishi kerak."
    )]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Foydalanuvchi nomi kiritilishi shart.")]
    [StringLength(
        50,
        MinimumLength = 4,
        ErrorMessage = "Username uzunligi 4 dan 50 tagacha bo‘lishi kerak."
    )]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parol kiritilishi shart.")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Parol uzunligi kamida 6 ta belgidan iborat bo‘lishi kerak."
    )]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
