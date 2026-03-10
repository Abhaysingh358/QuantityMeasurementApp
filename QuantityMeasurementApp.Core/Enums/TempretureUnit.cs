using QuantityMeasurementApp.Core.Interfaces;

namespace QuantityMeasurementApp.Core.Enums
{
    public class TemperatureUnit : IMeasurable
    {
        private readonly string _unitName;
        private readonly Func<double, double> _toCelsius;
        private readonly Func<double, double> _fromCelsius;

        // Lambda to indicate temperature does not support arithmetic
        private readonly Func<bool> _supportsArithmetic = () => false;

        private TemperatureUnit(string unitName, Func<double, double> toCelsius, Func<double, double> fromCelsius)
        {
            _unitName = unitName;
            _toCelsius = toCelsius;
            _fromCelsius = fromCelsius;
        }

        // Celsius is the base unit — identity function: celsius stays as celsius
        public static readonly TemperatureUnit Celsius = new TemperatureUnit(
            "Celsius",
            celsius => celsius,
            celsius => celsius);

        // Fahrenheit conversion formulas
        public static readonly TemperatureUnit Fahrenheit = new TemperatureUnit(
            "Fahrenheit",
            fahrenheit => (fahrenheit - 32) * 5.0 / 9.0,
            celsius => celsius * 9.0 / 5.0 + 32.0);

        // Kelvin conversion formulas
        public static readonly TemperatureUnit Kelvin = new TemperatureUnit(
            "Kelvin",
            kelvin => kelvin - 273.15,
            celsius => celsius + 273.15);

        public double ConvertToBaseUnit(double value)
        {
            return _toCelsius(value);
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            return _fromCelsius(baseValue);
        }

        public string GetUnitName()
        {
            return _unitName;
        }

        public double GetConversionFactor()
        {
            return 1.0;
        }

        // Override: temperature does not support arithmetic
        public bool SupportsArithmetic()
        {
            return _supportsArithmetic();
        }

        // Override: throw exception if arithmetic is attempted on temperature
        public void ValidateOperationSupport(string operation)
        {
            throw new InvalidOperationException(
                $"Temperature does not support {operation}. " +
                $"Only equality comparison and unit conversion are supported.");
        }

        public override string ToString()
        {
            return _unitName;
        }
    }
}