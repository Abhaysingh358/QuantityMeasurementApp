/// <summary> Description
/// UC3 is designed to overcome the Disadvantage of using Feet and Inches which starts violating the DRY principle, 
/// where both Feet and Inches classes contain nearly identical code, having the same constructor pattern, 
/// Identical equals() method implementation.
/// This Use Case refactors the existing Feet and Inches classes into a single generic Quantity Length class-
///  that eliminates code duplication while maintaining all functionality from UC1 and UC2.
/// The Quantity Length class represents any measurement with a value and unit type, 
/// applying the DRY (Don't Repeat Yourself) principle. This reduces maintenance burden and makes-
///  the codebase more scalable for adding new units in the future.
/// </summary>

using System.Runtime.CompilerServices;
using QuantityMeasurementApp.Core.Enums;
 namespace QuantityMeasurementApp.Core.Models
{
     /// <summary>
    /// Represents a length measurement with a value and unit.
    /// Eliminates code duplication from Feet and Inch classes — DRY principle.
    /// </summary>
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        //Initializes a new  instance  of QuantityLength

        public QuantityLength(double value, LengthUnit unit)
        {
             if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException($"Invalid unit: {unit}");
                // The isDefined() function, is a built-in function used in various 
                // programming contexts (notably Adobe ColdFusion/CFML, .NET, 
                // and scripting tools) to check whether a variable, parameter, or keyword exists in the current environment
                // and type of used to specify an  object type at compile time  from System.Type
            _value = value;
            _unit = unit;

        }

        // convertion method
         private double ConvertToBaseUnit()
        {
            return _value / (double)_unit;
        }

        // Compares this QuantityLength with another object for equality.

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            QuantityLength other = (QuantityLength)obj;

             return ConvertToBaseUnit().CompareTo(other.ConvertToBaseUnit()) == 0;
        }

        // It Returns hash code based on value and unit.

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    }
}
