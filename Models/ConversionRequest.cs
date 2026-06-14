namespace UnitConversionApi.Models
{
    /// <summary>
    /// Represents a request to convert a value from one unit to another.
    /// </summary>
    public class ConversionRequest
    {
        /// <summary>
        /// The value to be converted.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The unit of the provided value (e.g., meter, kilogram, celsius).
        /// </summary>
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// The target unit for the conversion (e.g., foot, pound, fahrenheit).
        /// </summary>
        public string To { get; set; } = string.Empty;
    }
}
