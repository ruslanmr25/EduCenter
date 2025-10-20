namespace Frontend.DTOs.CenterDTO;

using System.ComponentModel.DataAnnotations;

public class CreateCenterDto
{
    [Required(ErrorMessage = "Markaz nomi kiritilishi shart")]
    [StringLength(100, ErrorMessage = "Markaz nomi 100 ta belgidan oshmasligi kerak")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Markaz administratori tanlanishi shart")]
    [Range(1, int.MaxValue, ErrorMessage = "Markaz administratori ID noto‘g‘ri kiritilgan")]
    public int CenterAdminId { get; set; }
}
