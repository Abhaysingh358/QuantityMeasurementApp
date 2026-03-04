using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Models
{
    public class Quantity<T> where T : IMeasurable
    {
        private readonly double _value;
        private readonly T _unit;

        public Quantity(double value, T unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), "Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be finite: {value}");

            _value = value;
            _unit = unit;
        }

        private double ToBaseValue() => _unit.ConvertToBaseUnit(_value);

        public Quantity<T> ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = ToBaseValue();
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Quantity<T>(Math.Round(convertedValue, 5), targetUnit);
        }

        private Quantity<T> AddInTargetUnit(Quantity<T> other, T targetUnit)
        {
            double sum = ToBaseValue() + other.ToBaseValue();
            double result = targetUnit.ConvertFromBaseUnit(sum);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        public Quantity<T> Add(Quantity<T> other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return AddInTargetUnit(other, _unit);
        }

        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (targetUnit == null) throw new ArgumentNullException(nameof(targetUnit));
            return AddInTargetUnit(other, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Quantity<T> other = (Quantity<T>)obj;
            return Math.Abs(ToBaseValue() - other.ToBaseValue()) < 0.0001;
        }

        public override int GetHashCode() => ToBaseValue().GetHashCode();

        public override string ToString() => $"{_value} {_unit.GetUnitName()}";
    }
}