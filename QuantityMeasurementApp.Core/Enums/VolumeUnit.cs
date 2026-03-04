using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Enums
{
    /// <summary>
    /// Represents a volume unit with conversion responsibility.
    /// Implements IMeasurable interface — Single Responsibility Principle.
    /// Base unit is Litre.
    /// UC11 — Mirrors LengthUnit and WeightUnit pattern exactly.
    /// </summary>
    public class VolumeUnit : IMeasurable
    {
        // Conversion factor relative to base unit (litre)
        private readonly double _conversionFactor;
        private readonly string _unitName;

        // Private constructor — only static instances allowed
        private VolumeUnit(double conversionFactor, string unitName)
        {
            _conversionFactor = conversionFactor;
            _unitName = unitName;
        }

        // Static instances — replaces enum constants
        // Litre = 1.0 (base unit)
        public static readonly VolumeUnit Litre = new VolumeUnit(1.0, "Litre");
        // Millilitre = 0.001 (1 mL = 0.001 L)
        public static readonly VolumeUnit Millilitre = new VolumeUnit(0.001, "Millilitre");
        // Gallon = 3.78541 (1 gallon = 3.78541 L)
        public static readonly VolumeUnit Gallon = new VolumeUnit(3.78541, "Gallon");

        /// <summary>
        /// Converts a value in this unit to base unit (litre).
        /// Formula: value * conversionFactor
        /// </summary>
        public double ConvertToBaseUnit(double value)
        {
            return value * _conversionFactor;
        }

        /// <summary>
        /// Converts a value from base unit (litre) to this unit.
        /// Formula: baseValue / conversionFactor
        /// </summary>
        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / _conversionFactor;
        }

        public string GetUnitName()
        {
            return _unitName;
        }

        public override string ToString()
        {
            return _unitName;
        }
    }
}