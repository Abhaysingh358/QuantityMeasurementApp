///<summary> 
/* Description : 
UC7 extends UC6 by providing flexibility in specifying the unit for the addition result.
Instead of defaulting to the unit of the first operand, this use case allows the caller to 
explicitly specify any supported unit as the target unit for the result. 
This provides greater flexibility in use cases where the result must be expressed in 
a specific unit regardless of the operands
' units. For example, adding 1 foot and 12 inches with a target unit of yards should yield approximately 0.667 yards. */

/// </summary>

using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }

            if (!double.IsFinite(value))
            {
                throw new ArgumentException($"Value must be finite: {value}");
            }

            _value = value;
            _unit = unit;
        }

       private double ConvertToBaseUnit()
        {
            // Delegate to LengthUnit extension method — Single Responsibility Principle
            return _unit.ConvertToBaseUnit(_value);
        }

        
       public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            throw new ArgumentException($"Invalid target unit: {targetUnit}");

            // Step 1 — Convert to base unit using LengthUnit extension method
            double baseValue = _unit.ConvertToBaseUnit(_value);

            // Step 2 — Convert from base unit to target unit
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityLength(Math.Round(convertedValue, 2), targetUnit);
        }


        // Private helper — used by both Add methods (DRY)
        private QuantityLength AddInTargetUnit(QuantityLength other, LengthUnit targetUnit)
{
    // Convert both to base unit using LengthUnit extension method
    double sum = _unit.ConvertToBaseUnit(_value) + other._unit.ConvertToBaseUnit(other._value);

    // Convert sum to target unit
    double result = targetUnit.ConvertFromBaseUnit(sum);

    return new QuantityLength(Math.Round(result, 2), targetUnit);
}
        // UC6 — result in unit of first operand
        public QuantityLength Add(QuantityLength other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return AddInTargetUnit(other, _unit);
        }

        // UC7 — result in explicit target unit
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException($"Invalid unit: {targetUnit}");
                
            return AddInTargetUnit(other, targetUnit);
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