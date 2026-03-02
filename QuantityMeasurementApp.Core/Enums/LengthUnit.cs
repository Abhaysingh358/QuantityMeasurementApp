namespace QuantityMeasurementApp.Core.Enums;
// Supported Length units
public enum LengthUnit
{
     Feet,
    Inch,
    Yard,
    Centimeter
}

      /// <summary>
    /// Extension class for LengthUnit enum.
    /// Responsible for unit conversion logic — Single Responsibility Principle.
    /// LengthUnit now owns conversion responsibility instead of QuantityLength.
    /// Base unit is Feet.
    /// </summary>
    
    public static class LengthUnitExtensions
    {
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            if (unit == LengthUnit.Feet)
            {
                // Feet is base unit — no conversion needed
                return value;
            }
            else if (unit == LengthUnit.Inch)
            {
                // 12 inches = 1 foot, so divide by 12
                return value / 12.0;
            }
            else if (unit == LengthUnit.Yard)
            {
                // 1 yard = 3 feet, so multiply by 3
                return value * 3.0;
            }
            else if (unit == LengthUnit.Centimeter)
            {
                // 30.48 cm = 1 foot, so divide by 30.48
                return value / 30.48;
            }
            else
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            if (unit == LengthUnit.Feet)
            {
                // Feet is base unit — no conversion needed
                return baseValue;
            }
            else if (unit == LengthUnit.Inch)
            {
                // 1 foot = 12 inches, so multiply by 12
                return baseValue * 12.0;
            }
            else if (unit == LengthUnit.Yard)
            {
                // 3 feet = 1 yard, so divide by 3
                return baseValue / 3.0;
            }
            else if (unit == LengthUnit.Centimeter)
            {
                // 1 foot = 30.48 cm, so multiply by 30.48
                return baseValue * 30.48;
            }
            else
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }
        }
    }


