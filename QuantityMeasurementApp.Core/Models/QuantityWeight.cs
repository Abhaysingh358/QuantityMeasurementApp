using QuantityMeasurementApp.Core.Enums;

namespace QuantityMeasurementApp.Core.Models
{
    public class QuantityWeight
    {
        private readonly double _value;
        private readonly WeightUnit _unit;

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), $"Invalid unit: {unit}");

            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be finite: {value}");

            _value = value;
            _unit = unit;
        }

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException($"Invalid target unit: {targetUnit}");

            double baseValue = _unit.ConvertToBaseUnit(_value);
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
            return new QuantityWeight(Math.Round(convertedValue, 5), targetUnit);
        }

        private QuantityWeight AddInTargetUnit(QuantityWeight other, WeightUnit targetUnit)
        {
            double sum = _unit.ConvertToBaseUnit(_value) + other._unit.ConvertToBaseUnit(other._value);
            double result = targetUnit.ConvertFromBaseUnit(sum);
            return new QuantityWeight(Math.Round(result, 5), targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return AddInTargetUnit(other, _unit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (targetUnit == null)
                throw new ArgumentException($"Invalid target unit: {targetUnit}");
            return AddInTargetUnit(other, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            QuantityWeight other = (QuantityWeight)obj;
            return Math.Abs(_unit.ConvertToBaseUnit(_value) - other._unit.ConvertToBaseUnit(other._value)) < 0.0001;
        }

        public override int GetHashCode()
        {
            return _unit.ConvertToBaseUnit(_value).GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}