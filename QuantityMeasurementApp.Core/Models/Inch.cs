namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Represents a measurement in inches.
    /// Supports value-based equality comparison.
    /// </summary>
    public class Inch
    {
        private readonly double _value;

        /// <summary>
        /// Initializes a new instance of Inch with the given value.
        /// </summary>
        /// <param name="value">The numerical value in inches.</param>
        public Inch(double value)
        {
            _value = value;
        }

        /// <summary>
        /// Compares this Inch instance with another object for equality.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Inch other = (Inch)obj;
            return _value.CompareTo(other._value) == 0;
        }

        /// <summary>
        /// Returns hash code based on the inch value.
        /// </summary>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}