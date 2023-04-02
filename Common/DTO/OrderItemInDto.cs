using System.ComponentModel.DataAnnotations;

namespace Common.DTO;

public class OrderItemInDto
{
    [Required]public int OrderId { get; set; }
    [Required, MaxLength(16000)] public string Name { get; set; }
    [Required, Range(1, 999999999999999)]public double Quantity { get; set; }
    [Required, MaxLength(16000)] public string Unit { get; set; }
}