using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Enums
{
    /// <summary>
    /// Represents a length unit with conversion responsibility.
    /// Implements IMeasurable interface — Single Responsibility Principle.
    /// Base unit is Feet.
    /// UC10 — Wrapper class pattern replaces enum to allow interface implementation.
    /// </summary>
    public class LengthUnit : IMeasurable
    {
        // Conversion factor relative to base unit (feet)
        private readonly double _conversionFactor;
        private readonly string _unitName;

        // Private constructor — only static instances allowed
        private LengthUnit(double conversionFactor, string unitName)
        {
            _conversionFactor = conversionFactor;
            _unitName = unitName;
        }

        // Static instances — replaces enum constants
        // Feet = 1.0 (base unit)
        public static readonly LengthUnit Feet = new LengthUnit(1.0, "Feet");
        // Inch = 1/12 (1 inch = 1/12 foot)
        public static readonly LengthUnit Inch = new LengthUnit(1.0 / 12.0, "Inch");
        // Yard = 3.0 (1 yard = 3 feet)
        public static readonly LengthUnit Yard = new LengthUnit(3.0, "Yard");
        // Centimeter = 1/30.48 (1 cm = 1/30.48 foot)
        public static readonly LengthUnit Centimeter = new LengthUnit(1.0 / 30.48, "Centimeter");

        /// <summary>
        /// Converts a value in this unit to base unit (feet).
        /// Formula: value * conversionFactor
        /// </summary>
        public double ConvertToBaseUnit(double value)
        {
            // Multiply by conversion factor to get feet
            return value * _conversionFactor;
        }

        /// <summary>
        /// Converts a value from base unit (feet) to this unit.
        /// Formula: baseValue / conversionFactor
        /// </summary>
        public double ConvertFromBaseUnit(double baseValue)
        {
            // Divide by conversion factor to get this unit
            return baseValue / _conversionFactor;
        }

        /// <summary>
        /// Returns readable name of this unit.
        /// </summary>
        public string GetUnitName()
        {
            return _unitName;
        }

        /// <summary>
        /// Returns string representation.
        /// </summary>
        public override string ToString()
        {
            return _unitName;
        }
    }
}