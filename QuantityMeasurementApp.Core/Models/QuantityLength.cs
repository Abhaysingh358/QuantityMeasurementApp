/// <summary> 
/// Description
/// UC4 extends UC3 by introducing Yards and Centimeters as additional length units 
/// to the QuantityLength class. This use case demonstrates how the generic 
/// Quantity class design scales effortlessly to accommodate new units without code duplication. 
/// Yards will be added to the LengthUnit enum with the appropriate conversion factor (1 yard = 3 feet) 
/// and (1cm = 0.393701in), and all equality comparisons will work seamlessly across feet, inches, yards, and cms.
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
     // Converts value to base unit (feet)

     /// <summary>
     /// internal working of below method in modern csharp 
     /// private double ConvertToBaseUnit()
    // {
    //     if (_unit == LengthUnit.Feet)
    //     {
    //         return _value;
    //     }
    //     else if (_unit == LengthUnit.Inch)
    //     {
    //         return _value / 12.0;
    //     }
    //     else if (_unit == LengthUnit.Yard)
    //     {
    //         return _value * 3.0;
    //     }
    //     else if (_unit == LengthUnit.Centimeter)
    //     {
    //         return _value / 30.48;
    //     }
    //     else
    //     {
    //         throw new ArgumentException("Invalid unit");
    //     }
    // }
     /// </summary>
     /// <returns></returns>
     /// <exception cref="ArgumentException"></exception>
        private double ConvertToBaseUnit()
        {
            return _unit switch
        {
            LengthUnit.Feet => _value,
            LengthUnit.Inch => _value / 12.0,
            LengthUnit.Yard => _value * 3.0,
            LengthUnit.Centimeter => _value / 30.48,
            _ => throw new ArgumentException($"Invalid unit: {_unit}")
        };
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

             return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < 0.0001;
        }

        // It Returns hash code based on value and unit.

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    }
}
