using System;
using Microsoft.AspNetCore.Mvc;
using UnitConversionApi.Models;
using UnitConversionApi.Services;

namespace UnitConversionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        /// <summary>
        /// Converts a value from one unit to another.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="from">The source unit.</param>
        /// <param name="to">The target unit.</param>
        /// <returns>The conversion result.</returns>
        [HttpGet("convert")]
        public IActionResult Convert([FromQuery] double value, [FromQuery] string from, [FromQuery] string to)
        {
            try
            {
                var result = _conversionService.Convert(value, from, to);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Returns 400 Bad Request for invalid units or incompatible conversions
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                // Returns 500 Internal Server Error for unexpected issues
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
