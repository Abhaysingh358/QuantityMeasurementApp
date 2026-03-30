using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementApp.Models.DTO;
public class ConvertRequestDTO
{
    [Required]
    public QuantityDTO Input { get; set; }

    [Required]
    public string TargetUnit { get; set; }
}