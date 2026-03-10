namespace QuantityMeasurementApp.Core.Interfaces
{
    /// <summary>
    /// Interface for all measurement units.
    /// Defines contract for unit conversion logic.
    /// Implemented by LengthUnit, WeightUnit, and future unit enums.
    /// Single Responsibility — each unit class owns its conversion logic.
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>
        /// Converts a value in this unit to base unit.
        /// Example: Inch.ConvertToBaseUnit(12.0) returns 1.0 (feet)
        /// </summary>
        double ConvertToBaseUnit(double value);

        /// <summary>
        /// Converts a value from base unit to this unit.
        /// Example: Inch.ConvertFromBaseUnit(1.0) returns 12.0 (inches)
        /// </summary>
        double ConvertFromBaseUnit(double baseValue);

        /// <summary>
        /// Returns readable name of this unit.
        /// Example: "Feet", "Kilogram"
        /// </summary>
        string GetUnitName();

        // Default: all units support arithmetic. TemperatureUnit overrides this to return false.
        bool SupportsArithmetic() => true;

        // Default: does nothing. TemperatureUnit overrides this to throw an exception.
        void ValidateOperationSupport(string operation) { }
    }
}