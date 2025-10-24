using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.GroupDTOs;

public class UpdateGroupActiveStateDto
{
    [Required(ErrorMessage = "Active fieldi kiritilmagan")]
    public bool IsActive { get; set; }
}
