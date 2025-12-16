using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.SciencesDTOs;

public class UpdatedScienceDto
{
    [Required(ErrorMessage = "Nom kiritilishi shart.")]
    [StringLength(100, ErrorMessage = "Nom uzunligi 100 ta belgidan oshmasligi kerak.")]
    [MinLength(2, ErrorMessage = "Nom kamida 2 ta belgidan iborat boʻlishi kerak.")]
    public string Name { get; set; } = string.Empty;
}
