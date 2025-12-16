using System.ComponentModel.DataAnnotations;

namespace UI.DTOs;

public class CreateStudentDto
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid second phone number format.")]
    public string SecondPhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one group must be selected.")]
    [MinLength(1, ErrorMessage = "Select at least one group.")]
    public IEnumerable<int> Groups { get; set; } = new List<int>();
}
