using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Enums
{
    /// <summary>
    /// Represents a weight unit with conversion responsibility.
    /// Implements IMeasurable interface — Single Responsibility Principle.
    /// Base unit is Kilogram.
    /// UC10 — Wrapper class pattern replaces enum to allow interface implementation.
    /// </summary>
    public class WeightUnit : IMeasurable
    {
        // Conversion factor relative to base unit (kilogram)
        private readonly double _conversionFactor;
        private readonly string _unitName;

        // Private constructor — only static instances allowed
        private WeightUnit(double conversionFactor, string unitName)
        {
            _conversionFactor = conversionFactor;
            _unitName = unitName;
        }

        // Static instances — replaces enum constants
        // Kilogram = 1.0 (base unit)
        public static readonly WeightUnit Kilogram = new WeightUnit(1.0, "Kilogram");
        // Gram = 0.001 (1 gram = 0.001 kilogram)
        public static readonly WeightUnit Gram = new WeightUnit(0.001, "Gram");
        // Pound = 0.453592 (1 pound = 0.453592 kilogram)
        public static readonly WeightUnit Pound = new WeightUnit(0.453592, "Pound");

        /// <summary>
        /// Converts a value in this unit to base unit (kilogram).
        /// Formula: value * conversionFactor
        /// </summary>
        public double ConvertToBaseUnit(double value)
        {
            // Multiply by conversion factor to get kilograms
            return value * _conversionFactor;
        }

        /// <summary>
        /// Converts a value from base unit (kilogram) to this unit.
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