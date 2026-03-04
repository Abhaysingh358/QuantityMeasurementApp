using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Generic immutable quantity class supporting equality, conversion,
    /// addition, subtraction, and division across all measurement categories.
    /// UC10 — Generic design. UC12 — Subtraction and division added.
    /// </summary>
    public class Quantity<T> where T : IMeasurable
    {
        private readonly double _value;
        private readonly T _unit;

        /// <summary>
        /// Initializes a new Quantity with a value and unit.
        /// Validates that unit is non-null and value is finite.
        /// </summary>
        public Quantity(double value, T unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), "Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be finite: {value}");

            _value = value;
            _unit = unit;
        }

        /// <summary>
        /// Converts this quantity's value to the base unit.
        /// </summary>
        private double ToBaseValue() => _unit.ConvertToBaseUnit(_value);

        /// <summary>
        /// Validates that the other quantity is non-null and belongs to
        /// the same measurement category as this quantity.
        /// </summary>
        private void ValidateOperand(Quantity<T> other, string paramName)
        {
            if (other == null)
                throw new ArgumentNullException(paramName, "Operand cannot be null");

            if (_unit.GetType() != other._unit.GetType())
                throw new ArgumentException(
                    $"Cannot operate on different measurement categories: " +
                    $"{_unit.GetType().Name} and {other._unit.GetType().Name}");
        }

        // ─── CONVERSION ───────────────────────────────────────────────────────

        /// <summary>
        /// Converts this quantity to the specified target unit.
        /// Returns a new Quantity — immutability preserved.
        /// </summary>
        public Quantity<T> ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = ToBaseValue();
            double convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Quantity<T>(Math.Round(convertedValue, 5), targetUnit);
        }

        // ─── ADDITION ─────────────────────────────────────────────────────────

        /// <summary>
        /// Adds another quantity to this one. Result is in this quantity's unit.
        /// UC10 — implicit target unit.
        /// </summary>
        public Quantity<T> Add(Quantity<T> other)
        {
            ValidateOperand(other, nameof(other));
            return ArithmeticInTargetUnit(other, _unit, (a, b) => a + b);
        }

        /// <summary>
        /// Adds another quantity to this one. Result is in the specified target unit.
        /// UC10 — explicit target unit.
        /// </summary>
        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            ValidateOperand(other, nameof(other));
            if (targetUnit == null) throw new ArgumentNullException(nameof(targetUnit));
            return ArithmeticInTargetUnit(other, targetUnit, (a, b) => a + b);
        }

        // ─── SUBTRACTION ──────────────────────────────────────────────────────

        /// <summary>
        /// Subtracts another quantity from this one. Result is in this quantity's unit.
        /// UC12 — implicit target unit.
        /// Supports negative results (second operand larger than first).
        /// </summary>
        public Quantity<T> Subtract(Quantity<T> other)
        {
            ValidateOperand(other, nameof(other));
            return ArithmeticInTargetUnit(other, _unit, (a, b) => a - b);
        }

        /// <summary>
        /// Subtracts another quantity from this one. Result is in the specified target unit.
        /// UC12 — explicit target unit.
        /// </summary>
        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            ValidateOperand(other, nameof(other));
            if (targetUnit == null) throw new ArgumentNullException(nameof(targetUnit));
            return ArithmeticInTargetUnit(other, targetUnit, (a, b) => a - b);
        }

        // ─── DIVISION ─────────────────────────────────────────────────────────

        /// <summary>
        /// Divides this quantity by another, returning a dimensionless scalar ratio.
        /// UC12 — result > 1.0 means this is larger, < 1.0 means other is larger.
        /// Throws ArithmeticException if divisor is zero.
        /// </summary>
        public double Divide(Quantity<T> other)
        {
            ValidateOperand(other, nameof(other));

            double divisorBase = other.ToBaseValue();
            if (Math.Abs(divisorBase) < 1e-10)
                throw new ArithmeticException("Cannot divide by zero quantity");

            return ToBaseValue() / divisorBase;
        }

        // ─── PRIVATE HELPER ───────────────────────────────────────────────────

        /// <summary>
        /// Shared helper for all arithmetic operations (add, subtract).
        /// Converts both operands to base unit, applies the operation,
        /// then converts result to target unit. DRY principle.
        /// </summary>
        private Quantity<T> ArithmeticInTargetUnit(
            Quantity<T> other, T targetUnit, Func<double, double, double> operation)
        {
            double baseResult = operation(ToBaseValue(), other.ToBaseValue());
            double result = targetUnit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        // ─── EQUALITY & HASH ──────────────────────────────────────────────────

        /// <summary>
        /// Compares two quantities for equality by converting both to base unit.
        /// Uses epsilon tolerance for floating-point comparison.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Quantity<T> other = (Quantity<T>)obj;
            return Math.Abs(ToBaseValue() - other.ToBaseValue()) < 0.0001;
        }

        /// <summary>
        /// Returns hash code based on base unit value.
        /// Consistent with Equals — equal quantities have equal hash codes.
        /// </summary>
        public override int GetHashCode() => ToBaseValue().GetHashCode();

        public override string ToString() => $"{_value} {_unit.GetUnitName()}";
    }
}