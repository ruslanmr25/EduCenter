using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.SciencesDto;

public class CreateScienceDto
{
    [Required(ErrorMessage = "Fan nomi kiritilishi shart")]
    [StringLength(100, ErrorMessage = "Fan nomi 100 ta belgidan oshmasligi kerak")]
    public string Name { get; set; } = string.Empty;
}
