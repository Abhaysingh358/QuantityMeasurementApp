using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Models
{
    public class Quantity<T> where T : IMeasurable
    {
        //  PRIVATE ENUM 

        /// <summary>
        /// UC13 — Private enum using lambda expressions (DoubleBinaryOperator pattern).
        /// Each constant holds a Func(double, double) => double that defines the operation.
        /// Compute() executes the stored lambda with the two base-unit values.
        /// Adding a new operation = new constant only. No changes elsewhere.
        /// </summary>
        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

        /// <summary>
        /// UC13 — Executes the arithmetic operation on two base-unit values.
        /// Lambda dispatch — mirrors Java's DoubleBinaryOperator pattern in C#.
        /// Division-by-zero is validated here before computing.
        /// </summary>
        private static double Compute(ArithmeticOperation operation, double a, double b)
        {
            switch (operation)
            {
                case ArithmeticOperation.Add:
                    return a + b;
                case ArithmeticOperation.Subtract:
                    return a - b;
                case ArithmeticOperation.Divide:
                    if (b == 0)
                        throw new ArithmeticException("Cannot divide by zero quantity");
                    return a / b;
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}");
            }
        }

        //  FIELDS 

        private readonly double _value;
        private readonly T _unit;

        //  CONSTRUCTOR 

        public Quantity(double value, T unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit), "Unit cannot be null");

            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be finite: {value}");

            _value = value;
            _unit = unit;
        }

        //  PRIVATE HELPERS 

        private double ToBaseValue() => _unit.ConvertToBaseUnit(_value);

        /// <summary>
        /// UC13 — Step 2: Centralized validation helper.
        /// Validates null, same category, finite value, optional target unit.
        /// Called by ALL operations — validation logic defined ONCE.
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
        /// Converts both operands to base unit, calls Compute() via enums,
        /// returns raw base-unit result.
        /// Conversion logic defined ONCE — DRY Followed.
        /// </summary>
        private double PerformBaseArithmetic(Quantity<T> other, ArithmeticOperation operation)
        {
            double thisBase = ToBaseValue();
            double otherBase = other.ToBaseValue();
            return Compute(operation, thisBase, otherBase);
        }

        //  CONVERSION 

        public Quantity<T> ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            double baseValue = ToBaseValue();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Quantity<T>(Math.Round(converted, 5), targetUnit);
        }

        // ADDITION 

        /// <summary>UC13 — Step 4: Implicit target unit.</summary>
        public Quantity<T> Add(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double result = _unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), _unit);
        }

        /// UC13 — Step 4: Explicit target unit.
        public Quantity<T> Add(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double result = targetUnit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        // SUBTRACTION

        /// <summary>UC13 — Step 4: Implicit target unit.</summary>
        public Quantity<T> Subtract(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, _unit, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double result = _unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), _unit);
        }

        /// UC13 — Step 4: Explicit target unit.
        public Quantity<T> Subtract(Quantity<T> other, T targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double result = targetUnit.ConvertFromBaseUnit(baseResult);
            return new Quantity<T>(Math.Round(result, 5), targetUnit);
        }

        // DIVISION 

        /// UC13 — Step 4: Returns dimensionless scalar.
        public double Divide(Quantity<T> other)
        {
            ValidateArithmeticOperands(other, default, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
        }

        // EQUALITY & HASH 

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