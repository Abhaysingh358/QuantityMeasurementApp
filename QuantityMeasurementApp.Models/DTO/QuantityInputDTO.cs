using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementApp.Models.DTO;

public class QuantityInputDTO
{
    [Required]
    public QuantityDTO First { get; set; }

    [Required]
    public QuantityDTO Second { get; set; }
}
// This class is created because API can only take one body 