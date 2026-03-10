using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Generic immutable quantity supporting equality, conversion,
    /// addition, subtraction, and division across all measurement categories.
    ///
    /// UC10 — Generic design with IMeasurable constraint.
    /// UC12 — Subtraction and division operations added.
    /// UC13 — Centralized arithmetic via private ArithmeticOperation enum and private helpers (DRY enforced).
    /// UC14 — ValidateOperationSupport() called in PerformBaseArithmetic before any arithmetic.
    ///         TemperatureUnit throws InvalidOperationException from its override.
    ///         ConvertTo() works for temperature via non-linear lambda path (no special-case needed).
    ///         LengthUnit, WeightUnit, VolumeUnit unaffected — inherit default no-op from IMeasurable.
    /// </summary>
    public class Quantity<T> where T : IMeasurable
    {
        /// <summary>
        /// UC13 — Private enum for arithmetic operation dispatch.
        /// ADD, SUBTRACT, DIVIDE each represent a specific arithmetic operation.
        /// Private: implementation detail, not part of public API.
        /// UC14 — operation.ToString() passed to ValidateOperationSupport() for clear error messages.
        /// </summary>
        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

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

        /// <summary>
        /// UC13 — Step 2: Centralized validation helper.
        /// Validates null operand, same measurement category, finite value, optional target unit.
        /// Called by ALL arithmetic operations — validation logic defined ONCE.
        /// </summary>
        private void ValidateArithmeticOperands(Quantity<T> other, T targetUnit, bool targetUnitRequired)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "Operand cannot be null");

            if (_unit.GetType() != other._unit.GetType())
                throw new ArgumentException(
                    $"Cannot operate on different measurement categories: " +
                    $"{_unit.GetType().Name} and {other._unit.GetType().Name}");

            if (!double.IsFinite(other._value))
                throw new ArgumentException($"Operand value must be finite: {other._value}");

            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit), "Target unit cannot be null");
        }

        /// <summary>
        /// UC13 — Step 3: Core arithmetic helper.
        /// UC14 — Calls this.unit.ValidateOperationSupport(operation.ToString()) FIRST.
        ///         TemperatureUnit throws InvalidOperationException before any arithmetic runs.
        ///         LengthUnit, WeightUnit, VolumeUnit inherit no-op default — unaffected.
        /// Converts both operands to base unit, dispatches operation, returns raw base-unit result.
        /// </summary>
        private double PerformBaseArithmetic(Quantity<T> other, ArithmeticOperation operation)
        {
            // UC14 — validate operation support before any arithmetic
            _unit.ValidateOperationSupport(operation.ToString());

            double thisBase = ToBaseValue();
            double otherBase = other.ToBaseValue();

            switch (operation)
            {
                case ArithmeticOperation.Add:
                    return thisBase + otherBase;
                case ArithmeticOperation.Subtract:
                    return thisBase - otherBase;
                case ArithmeticOperation.Divide:
                    if (otherBase == 0)
                        throw new ArithmeticException("Cannot divide by zero quantity");
                    return thisBase / otherBase;
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}");
            }
        }

        /// <summary>
        /// Converts this quantity to the specified target unit.
        /// UC14 — Works correctly for temperature without special-casing:
        ///         ConvertToBaseUnit() applies the stored _toCelsius lambda.
        ///         targetUnit.ConvertFromBaseUnit() applies the stored _fromCelsius lambda on target.
        ///         Example: 100°C to Fahrenheit: toCelsius(100) = 100, fromFahrenheit(100) = 212.
        /// </summary>
        public Quantity<T> ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = ToBaseValue();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Quantity<T>(Math.Round(converted, 5), targetUnit);
        }

        /// <summary>UC13 — Step 4: Implicit target unit (first operand's unit).</summary>
        public Quantity<T> Add(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double result = _unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), _unit);
        }

        /// <summary>UC13 — Step 4: Explicit target unit.</summary>
        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double result = targetUnit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        /// <summary>UC13 — Step 4: Implicit target unit (first operand's unit).</summary>
        public Quantity<T> Subtract(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double result = _unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), _unit);
        }

        /// <summary>UC13 — Step 4: Explicit target unit.</summary>
        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double result = targetUnit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        /// <summary>UC13 — Step 4: Returns dimensionless scalar.</summary>
        public double Divide(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, default, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
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