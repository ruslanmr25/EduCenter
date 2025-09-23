using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthDto;

public class LoginDto
{
    [Required]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
}
