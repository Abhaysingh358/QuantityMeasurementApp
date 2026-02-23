///<summary> 
/// Description
/// UC6 extends UC5 by introducing addition operations between length measurements. 
/// This use case enables the Quantity Length API to add two lengths of potentially different units 
/// (but same category—length) and return the result in the unit of the first operand. 
/// Essentially adding another length to the current length. For example, adding 1 foot and 12 inches should yield 2 feet 
/// (based on the unit of the first operand).

/* Preconditions : 
Quantity Length class (from UC3/UC4/UC5) and LengthUnit enum exist with FEET, INCHES, YARDS, CENTIMETERS.
The conversionFactor for each LengthUnit is defined relative to a consistent base unit.
Two Quantity Length objects or raw values with their respective units are provided.
A target unit is the unit of the first operand.
All units belong to the same measurement category (length). */

/// </summary>

using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
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
                return _value;
            }
            else if (_unit == LengthUnit.Inch)
            {
                return _value / 12.0;
            }
            else if (_unit == LengthUnit.Yard)
            {
                return _value * 3.0;
            }
            else if (_unit == LengthUnit.Centimeter)
            {
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
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            {
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            }

            double baseValue = ConvertToBaseUnit();
            double convertedValue;

            if (targetUnit == LengthUnit.Feet)
            {
                convertedValue = baseValue;
            }
            else if (targetUnit == LengthUnit.Inch)
            {
                
                convertedValue = baseValue * 12.0;
            }
            else if (targetUnit == LengthUnit.Yard)
            {
                
                convertedValue = baseValue / 3.0;
            }
            else if (targetUnit == LengthUnit.Centimeter)
            {
                
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
        /// Adds two length measurements and returns result in unit of first operand.
        /// Step 1 — Convert both values to base unit (feet).
        /// Step 2 — Add the base unit values.
        /// Step 3 — Convert sum to unit of first operand.
        /// </summary>
        /// <param name="other">The second length measurement to add.</param>
        /// <returns>New QuantityLength with sum in unit of first operand.</returns>
    public QuantityLength Add(QuantityLength other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Cannot add null measurement");
        }

        // Step 1 — Convert both values to base unit (feet)
        double firstBaseValue = ConvertToBaseUnit();
        double secondBaseValue = other.ConvertToBaseUnit();

        // Step 2 — Add the base unit values
        double sumInBaseUnit = firstBaseValue + secondBaseValue;

        // Step 3 — Convert sum back to unit of first operand
        double convertedSum;

        if (_unit == LengthUnit.Feet)
        {
            // Result stays in feet — no conversion needed
            convertedSum = sumInBaseUnit;
        }
        else if (_unit == LengthUnit.Inch)
        {
            // Convert feet to inches — multiply by 12
            convertedSum = sumInBaseUnit * 12.0;
        }
        else if (_unit == LengthUnit.Yard)
        {
            // Convert feet to yards — divide by 3
            convertedSum = sumInBaseUnit / 3.0;
        }
        else if (_unit == LengthUnit.Centimeter)
        {
            // Convert feet to centimeters — multiply by 30.48
            convertedSum = sumInBaseUnit * 30.48;
        }
        else
        {
        throw new ArgumentException($"Invalid unit: {_unit}");
    }

    // Round to 2 decimal places for consistent precision
    return new QuantityLength(Math.Round(convertedSum, 2), _unit);
}

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            QuantityLength other = (QuantityLength)obj;

            // Use epsilon for floating point precision comparison
            // Math.Abs() returns the absolute difference between two values
            // If the difference is less than epsilon, they are considered equal
            return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < 0.0001;
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}