using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs;

public class TeacherDto
{
    [Required(ErrorMessage = "To'liq ism kiritilishi shart")]
    [StringLength(100, ErrorMessage = "To'liq ism 100 ta belgidan oshmasligi kerak")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Foydalanuvchi nomi kiritilishi shart")]
    [StringLength(
        50,
        MinimumLength = 3,
        ErrorMessage = "Foydalanuvchi nomi 3–50 ta belgidan iborat bo‘lishi kerak"
    )]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Parol kiritilishi shart")]
    [StringLength(
        100,
        MinimumLength = 6,
        ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak"
    )]
    [DataType(DataType.Password)]
    public required string Password { get; set; }



    
}
