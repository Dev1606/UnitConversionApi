namespace UnitConversionApi.Models
{
    /// <summary>
    /// Represents the result of a unit conversion.
    /// </summary>
    public class ConversionResponse
    {
        /// <summary>
        /// The original value provided.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The original unit.
        /// </summary>
        public string FromUnit { get; set; } = string.Empty;

        /// <summary>
        /// The target unit.
        /// </summary>
        public string ToUnit { get; set; } = string.Empty;

        /// <summary>
        /// The converted result.
        /// </summary>
        public double Result { get; set; }
    }
}
