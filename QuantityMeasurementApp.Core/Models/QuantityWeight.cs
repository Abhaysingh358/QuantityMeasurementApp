using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Represents a weight measurement with a value and unit.
    /// Mirrors QuantityLength design — scalable generic pattern.
    /// Supports equality, conversion, and addition operations.
    /// Base unit is Kilogram.
    /// </summary>
    public class QuantityWeight
    {
        private readonly double _value;
        private readonly WeightUnit _unit;

        /// <summary>
        /// Initializes a new instance of QuantityWeight.
        /// Validates that unit is a defined enum value.
        /// Validates that value is a finite number — rejects NaN and Infinity.
        /// </summary>
        public QuantityWeight(double value, WeightUnit unit)
        {
            // Validates unit is defined in enum
            if (!Enum.IsDefined(typeof(WeightUnit), unit))
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }

            // Validates value is finite — rejects NaN and Infinity
            if (!double.IsFinite(value))
            {
                throw new ArgumentException($"Value must be finite: {value}");
            }

            _value = value;
            _unit = unit;
        }

        /// <summary>
        /// Converts this weight to the target unit.
        /// Returns new QuantityWeight instance — immutability preserved.
        /// </summary>
        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            // Validate target unit
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
            {
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            }

            // Step 1 — Convert to base unit (kilogram)
            double baseValue = _unit.ConvertToBaseUnit(_value);

            // Step 2 — Convert from base unit to target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityWeight(Math.Round(convertedValue, 5), targetUnit);
        }

        /// <summary>
        /// Private helper — used by both Add methods (DRY principle).
        /// </summary>
        private QuantityWeight AddInTargetUnit(QuantityWeight other, WeightUnit targetUnit)
        {
            // Convert both to base unit and add
            double sum = _unit.ConvertToBaseUnit(_value) + other._unit.ConvertToBaseUnit(other._value);

            // Convert sum to target unit
            double result = targetUnit.ConvertFromBaseUnit(sum);

            return new QuantityWeight(Math.Round(result, 5), targetUnit);
        }

        /// <summary>
        /// UC6 equivalent — result in unit of first operand.
        /// </summary>
        public QuantityWeight Add(QuantityWeight other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return AddInTargetUnit(other, _unit);
        }

        /// <summary>
        /// UC7 equivalent — result in explicit target unit.
        /// </summary>
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            return AddInTargetUnit(other, targetUnit);
        }

        /// <summary>
        /// Compares this QuantityWeight with another object for equality.
        /// Converts both values to base unit before comparison.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            QuantityWeight other = (QuantityWeight)obj;

            // Use epsilon for floating point precision
            return Math.Abs(_unit.ConvertToBaseUnit(_value) - other._unit.ConvertToBaseUnit(other._value)) < 0.0001;
        }

        /// <summary>
        /// Returns hash code based on base unit value.
        /// </summary>
        public override int GetHashCode()
        {
            return _unit.ConvertToBaseUnit(_value).GetHashCode();
        }

        /// <summary>
        /// Returns human readable representation.
        /// </summary>
        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}