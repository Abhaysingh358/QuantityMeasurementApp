using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), $"Invalid unit: {unit}");

            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be finite: {value}");

            _value = value;
            _unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return _unit.ConvertToBaseUnit(_value);
        }

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException($"Invalid target unit: {targetUnit}");

            double baseValue = _unit.ConvertToBaseUnit(_value);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
            return new QuantityLength(Math.Round(convertedValue, 2), targetUnit);
        }

        private QuantityLength AddInTargetUnit(QuantityLength other, LengthUnit targetUnit)
        {
            double sum = _unit.ConvertToBaseUnit(_value) + other._unit.ConvertToBaseUnit(other._value);
            double result = targetUnit.ConvertFromBaseUnit(sum);
            return new QuantityLength(Math.Round(result, 2), targetUnit);
        }

        public QuantityLength Add(QuantityLength other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return AddInTargetUnit(other, _unit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (targetUnit == null)
                throw new ArgumentException($"Invalid unit: {targetUnit}");
            return AddInTargetUnit(other, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            QuantityLength other = (QuantityLength)obj;
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