///<summary> 
/// Description
/// UC5 extends UC4 by providing explicit conversion operations between length units 
/// (e.g., feet to inches, yards to inches, centimeters to feet). 
/// Instead of only comparing equality, the Quantity Length API exposes a conversion method 
/// that returns a numeric value converted from a source unit to a target unit using the centralized conversion factors.
/// </summary>

using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// UC5 — Adds ConvertTo() method and NaN/Infinite validation.
    /// Represents a length measurement with a value and unit.
    /// Eliminates code duplication from Feet and Inch classes — DRY principle.
    /// Supports cross-unit equality by converting to base unit (feet).
    /// Supports unit conversion by converting to base unit then to target unit.
    /// </summary>
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        /// <summary>
        /// Initializes a new instance of QuantityLength.
        /// Validates that unit is a defined enum value.
        /// Validates that value is a finite number — rejects NaN and Infinity.
        /// </summary>
        /// <param name="value">The numerical value of the measurement.</param>
        /// <param name="unit">The unit of the measurement.</param>
        public QuantityLength(double value, LengthUnit unit)
        {
            // Enum.IsDefined() checks whether a variable, parameter, or keyword
            // exists in the current environment and type.
            // It is used to specify an object type at compile time from System.Type.
            // This ensures only valid LengthUnit values are accepted.
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }

            // double.IsFinite() checks whether the value is a finite number.
            // It rejects NaN (Not a Number) and Infinity (-infinity, +infinity).
            // NaN is produced by operations like 0.0 / 0.0.
            // Infinity is produced by operations like 1.0 / 0.0.
            if (!double.IsFinite(value))
            {
                throw new ArgumentException($"Value must be finite: {value}");
            }

            _value = value;
            _unit = unit;
        }

        /// <summary>
        /// Converts value to base unit (feet) for comparison.
        /// All units are converted relative to feet as base unit.
        /// Feet  =  value × 1.0      (already in base unit)
        /// Inch  = value / 12.0     (12 inches = 1 foot)
        /// Yard  = value × 3.0      (1 yard = 3 feet)
        /// Centimeter  = value / 30.48    (30.48 cm = 1 foot)
        /// </summary>
        /// <returns>Value converted to feet (base unit).</returns>
        private double ConvertToBaseUnit()
        {
            if (_unit == LengthUnit.Feet)
            {
                // Feet is the base unit — no conversion needed
                return _value;
            }
            else if (_unit == LengthUnit.Inch)
            {
                // 1 foot = 12 inches, so divide by 12 to get feet
                return _value / 12.0;
            }
            else if (_unit == LengthUnit.Yard)
            {
                // 1 yard = 3 feet, so multiply by 3 to get feet
                return _value * 3.0;
            }
            else if (_unit == LengthUnit.Centimeter)
            {
                // 1 foot = 30.48 centimeters, so divide by 30.48 to get feet
                return _value / 30.48;
            }
            else
            {
                throw new ArgumentException($"Invalid unit: {_unit}");
            }
        }

        /// <summary>
        /// Converts this length measurement to the target unit.
        /// Returns a new QuantityLength instance — immutability preserved.
        /// Conversion formula:
        /// Step 1 — Convert source value to base unit (feet).
        /// Step 2 — Convert base unit value to target unit.
        /// </summary>
        /// <param name="targetUnit">The unit to convert to.</param>
        /// <returns>New QuantityLength instance in the target unit.</returns>
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            // Validate that target unit is a defined enum value
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            {
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            }

            // Step 1 — Convert source value to base unit (feet)
            double baseValue = ConvertToBaseUnit();

            // Step 2 — Convert base unit value to target unit
            double convertedValue;

            if (targetUnit == LengthUnit.Feet)
            {
                // Feet is base unit — no conversion needed
                convertedValue = baseValue;
            }
            else if (targetUnit == LengthUnit.Inch)
            {
                // 1 foot = 12 inches, so multiply by 12 to get inches
                convertedValue = baseValue * 12.0;
            }
            else if (targetUnit == LengthUnit.Yard)
            {
                // 1 yard = 3 feet, so divide by 3 to get yards
                convertedValue = baseValue / 3.0;
            }
            else if (targetUnit == LengthUnit.Centimeter)
            {
                // 1 foot = 30.48 centimeters, so multiply by 30.48 to get centimeters
                convertedValue = baseValue * 30.48;
            }
            else
            {
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            }

            // Round to 2 decimal places for consistent precision
            return new QuantityLength(Math.Round(convertedValue, 2), targetUnit);
        }

        /// <summary>
        /// Compares this QuantityLength with another object for equality.
        /// Converts both values to base unit before comparison.
        /// Uses epsilon (0.0001) for floating point precision.
        /// This ensures that equivalent measurements in different units
        /// are considered equal ( 1 foot == 12 inches).
        /// </summary>
        public override bool Equals(object? obj)
        {
            // Same reference check — if both point to same object they are equal
            if (this == obj)
            {
                return true;
            }

            // Null and type check — null or different type is never equal
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // Cast to QuantityLength and compare base unit values
            QuantityLength other = (QuantityLength)obj;

            // Use epsilon for floating point precision comparison
            // Math.Abs() returns the absolute difference between two values
            // If the difference is less than epsilon, they are considered equal
            return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < 0.0001;
        }

        /// <summary>
        /// Returns hash code based on base unit value.
        /// Objects that are equal must have the same hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        /// <summary>
        /// Returns human readable representation of the measurement.
        /// Overrides default ToString() from Object class.
        /// Useful for debugging and logging.
        /// Example: "1.0 Feet", "12.0 Inch"
        /// </summary>
        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}