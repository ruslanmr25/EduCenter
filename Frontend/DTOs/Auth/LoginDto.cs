using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username kiritilishi shart.")]
        [MaxLength(255, ErrorMessage = "Username 255 ta belgidan oshmasligi kerak.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parol kiritilishi shart.")]
        [MaxLength(255, ErrorMessage = "Parol 255 ta belgidan oshmasligi kerak.")]
        public string Password { get; set; } = string.Empty;
    }
}
