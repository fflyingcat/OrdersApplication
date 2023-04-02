using System.ComponentModel.DataAnnotations;

namespace Common.DTO;

public class OrderInDto
{
    [Required, MaxLength(16000)] public string Number { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public int ProviderId { get; set; }
}