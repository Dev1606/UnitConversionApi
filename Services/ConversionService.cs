using System;
using UnitConversionApi.Models;

namespace UnitConversionApi.Services
{
    public class ConversionService : IConversionService
    {
        // Length conversion factors to meters (base unit)
        private const double MeterToMeter = 1.0;
        private const double KilometerToMeter = 1000.0;
        private const double FootToMeter = 0.3048;
        private const double InchToMeter = 0.0254;

        // Weight conversion factors to kilograms (base unit)
        private const double KilogramToKilogram = 1.0;
        private const double GramToKilogram = 0.001;
        private const double PoundToKilogram = 0.45359237;

        public ConversionResponse Convert(double value, string fromUnit, string toUnit)
        {
            if (string.IsNullOrWhiteSpace(fromUnit))
                throw new ArgumentException("Source unit cannot be empty.", nameof(fromUnit));
            
            if (string.IsNullOrWhiteSpace(toUnit))
                throw new ArgumentException("Target unit cannot be empty.", nameof(toUnit));

            fromUnit = fromUnit.Trim().ToLowerInvariant();
            toUnit = toUnit.Trim().ToLowerInvariant();

            double result;

            if (IsLengthUnit(fromUnit) && IsLengthUnit(toUnit))
            {
                result = ConvertLength(value, fromUnit, toUnit);
            }
            else if (IsWeightUnit(fromUnit) && IsWeightUnit(toUnit))
            {
                result = ConvertWeight(value, fromUnit, toUnit);
            }
            else if (IsTemperatureUnit(fromUnit) && IsTemperatureUnit(toUnit))
            {
                result = ConvertTemperature(value, fromUnit, toUnit);
            }
            else
            {
                throw new ArgumentException($"Conversion from '{fromUnit}' to '{toUnit}' is not supported or units are incompatible.");
            }

            return new ConversionResponse
            {
                Value = value,
                FromUnit = fromUnit,
                ToUnit = toUnit,
                Result = Math.Round(result, 6) // Rounding for cleaner output
            };
        }

        private bool IsLengthUnit(string unit)
        {
            return unit is "meter" or "kilometer" or "foot" or "inch";
        }

        private bool IsWeightUnit(string unit)
        {
            return unit is "kilogram" or "gram" or "pound";
        }

        private bool IsTemperatureUnit(string unit)
        {
            return unit is "celsius" or "fahrenheit" or "kelvin";
        }

        private double ConvertLength(double value, string fromUnit, string toUnit)
        {
            // Convert to base unit (meters)
            double valueInMeters = fromUnit switch
            {
                "meter" => value * MeterToMeter,
                "kilometer" => value * KilometerToMeter,
                "foot" => value * FootToMeter,
                "inch" => value * InchToMeter,
                _ => throw new NotImplementedException()
            };

            // Convert from base unit (meters) to target unit
            return toUnit switch
            {
                "meter" => valueInMeters / MeterToMeter,
                "kilometer" => valueInMeters / KilometerToMeter,
                "foot" => valueInMeters / FootToMeter,
                "inch" => valueInMeters / InchToMeter,
                _ => throw new NotImplementedException()
            };
        }

        private double ConvertWeight(double value, string fromUnit, string toUnit)
        {
            // Convert to base unit (kilograms)
            double valueInKilograms = fromUnit switch
            {
                "kilogram" => value * KilogramToKilogram,
                "gram" => value * GramToKilogram,
                "pound" => value * PoundToKilogram,
                _ => throw new NotImplementedException()
            };

            // Convert from base unit (kilograms) to target unit
            return toUnit switch
            {
                "kilogram" => valueInKilograms / KilogramToKilogram,
                "gram" => valueInKilograms / GramToKilogram,
                "pound" => valueInKilograms / PoundToKilogram,
                _ => throw new NotImplementedException()
            };
        }

        private double ConvertTemperature(double value, string fromUnit, string toUnit)
        {
            if (fromUnit == toUnit) return value;

            // Convert to Celsius first
            double valueInCelsius = fromUnit switch
            {
                "celsius" => value,
                "fahrenheit" => (value - 32) * 5.0 / 9.0,
                "kelvin" => value - 273.15,
                _ => throw new NotImplementedException()
            };

            // Convert from Celsius to target unit
            return toUnit switch
            {
                "celsius" => valueInCelsius,
                "fahrenheit" => (valueInCelsius * 9.0 / 5.0) + 32,
                "kelvin" => valueInCelsius + 273.15,
                _ => throw new NotImplementedException()
            };
        }
    }
}
