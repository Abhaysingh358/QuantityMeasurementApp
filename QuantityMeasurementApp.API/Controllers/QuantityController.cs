using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Models.DTO;
using QuantityMeasurementApp.Business.Interfaces;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        // Compare
        [HttpPost("compare")]
        public IActionResult Compare([FromBody] QuantityInputDTO input)
        {
            if (input?.First == null || input?.Second == null)
                return BadRequest("Input quantities cannot be null.");

            var result = _service.Compare(input.First, input.Second);
            return Ok(result);
        }

        // Convert
        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConvertRequestDTO request)
        {
            var result = _service.Convert(request.Input, request.TargetUnit);
            return Ok(result);
        }

        // Add
        [HttpPost("add")]
        public IActionResult Add([FromBody] QuantityInputDTO input)
        {
            var result = _service.Add(input.First, input.Second);
            return Ok(result);
        }

        //Subtract
        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] QuantityInputDTO input)
        {
            var result = _service.Subtract(input.First, input.Second);
            return Ok(result);
        }

        // Divide
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] QuantityInputDTO input)
        {
            var result = _service.Divide(input.First, input.Second);
            return Ok(result);
        }
    }
}