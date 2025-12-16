using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOs.CenterDTO;

public class EditCenterDto
{
    [Required(ErrorMessage = "Markaz nomi kiritilishi shart")]
    [StringLength(100, ErrorMessage = "Markaz nomi 100 ta belgidan oshmasligi kerak")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Markaz administratori tanlanishi shart")]
    [Range(1, int.MaxValue, ErrorMessage = "Markaz administratori ID noto‘g‘ri kiritilgan")]
    public int CenterAdminId { get; set; }
}
