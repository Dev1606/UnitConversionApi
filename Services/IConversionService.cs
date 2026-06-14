using UnitConversionApi.Models;

namespace UnitConversionApi.Services
{
    /// <summary>
    /// Defines the contract for unit conversion services.
    /// </summary>
    public interface IConversionService
    {
        /// <summary>
        /// Converts a value from one unit to another.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The source unit.</param>
        /// <param name="toUnit">The target unit.</param>
        /// <returns>A ConversionResponse containing the result.</returns>
        ConversionResponse Convert(double value, string fromUnit, string toUnit);
    }
}
