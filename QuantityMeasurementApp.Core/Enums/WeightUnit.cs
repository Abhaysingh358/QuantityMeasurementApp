namespace QuantityMeasurementApp.Core.Enums
{
    // Supported Weight units
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    /// <summary>
    /// Extension class for WeightUnit enum.
    /// Responsible for unit conversion logic — Single Responsibility Principle.
    /// Base unit is Kilogram.
    /// Kilogram  = 1.0
    /// Gram   =  0.001 (1g = 0.001 kg)
    /// Pound  =  0.453592 (1lb = 0.453592 kg)
    /// </summary>
    public static class WeightUnitExtensions
    {
        /// <summary>
        /// Converts a value in this unit to base unit (kilogram).
        /// </summary>
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            if (unit == WeightUnit.Kilogram)
            {
                // Kilogram is base unit — no conversion needed
                return value;
            }
            else if (unit == WeightUnit.Gram)
            {
                // 1000 grams = 1 kilogram, so divide by 1000
                return value / 1000.0;
            }
            else if (unit == WeightUnit.Pound)
            {
                // 1 pound = 0.453592 kilograms
                return value * 0.453592;
            }
            else
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }
        }

        /// <summary>
        /// Converts a value from base unit (kilogram) to this unit.
        /// </summary>
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            if (unit == WeightUnit.Kilogram)
            {
                // Kilogram is base unit — no conversion needed
                return baseValue;
            }
            else if (unit == WeightUnit.Gram)
            {
                // 1 kilogram = 1000 grams, so multiply by 1000
                return baseValue * 1000.0;
            }
            else if (unit == WeightUnit.Pound)
            {
                // 1 kilogram = 2.20462 pounds
                return baseValue / 0.453592;
            }
            else
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }
        }
    }
}