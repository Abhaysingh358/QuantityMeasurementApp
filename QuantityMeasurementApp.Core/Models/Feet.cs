namespace QuantityMeasurementApp.Core.Models
{
   /// <summary> 
   /// This is the Feet class to Measurement 
   /// Supports value-based equality comparision
   /// </summary>
   
   public class Feet
    {
        private readonly double _value;
         /// <summary>
        /// Initializes a new instance of Feet with the given value.
        /// </summary>
        /// <param name="value">The numerical value in feet.</param>
          public Feet(double value)
        {
            _value = value;
        }

        // Compares this Feet instance with another object for equality.

        public override bool Equals(object obj)
        {
            // Check if the object is the same reference (this == obj)
            if (this == obj)
            {
                return true;
            }

            // Checking  if the object is null or a different type
            if(obj ==  null || GetType() != obj.GetType())
            {
                return false;
            }

            // Cast to Feet type safely
            Feet other = (Feet)obj;

            // Compare double values using CompareTo() instead of == operator
            return _value.CompareTo(other._value) == 0;
        }

        // Returns hash code based on the feet value.
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

    }
}