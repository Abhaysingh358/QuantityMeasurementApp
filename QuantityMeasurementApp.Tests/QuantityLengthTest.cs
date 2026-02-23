using System.Reflection;
using System.Reflection.Metadata;
using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;
namespace QuantityMeasurementApp.Test
{
    [TestClass]
    public class QuantityLengthTest
    {
        /// <summary>
        /// estEquality_FeetToFeet_SameValue() – 
        /// Verifies that Quantity(1.0, "feet") equals Quantity(1.0, "feet").
        /// Tests: equals() returns true for identical feet measurements.
        /// </summary>
        /// 
        [TestMethod]
        public void GivenTwoFeetValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.AreEqual(firstValue, secondValue);
        }

        /// <summary> testEquality_InchToInch_SameValue()
        /// Verifies that Quantity(1.0, "inch") equals Quantity(1.0, "inch").
        /// Tests: equals() returns true for identical inch measurements.</summary>
        
         // Test 2: Different feet values should not be equal
        [TestMethod]
        public void GivenTwoInchValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Inch);
            Assert.AreEqual(firstValue, secondValue);
        }
        /// <summary> 
        /// testEquality_NullComparison() – 
        /// Verifies that Quantity(1.0, "feet") equals Quantity(12.0, "inch").
        /// Tests: equals() returns true when feet and inches are equivalent.
        /// </summary>
       [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength inchValue = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.AreEqual(feetValue, inchValue);
        }
        ///<summary> 
        /// testEquality_InchToFeet_EquivalentValue() – 
        /// Verifies that Quantity(12.0, "inch") equals Quantity(1.0, "feet").
        ///Tests: equals() returns true (tests symmetry of conversion).
        // Test 4: Different inch values should not be equal 
        // </summary>
        [TestMethod]
        public void GivenTwelveInchesAndOneFeet_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength inchValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.AreEqual(inchValue, feetValue);
        }

        ///<summary> 
        /// testEquality_FeetToFeet_DifferentValue() – 
        /// Verifies that Quantity(1.0, "feet") does not equal Quantity(2.0, "feet").
        /// Tests: equals() returns false for different feet measurements.
        /// </summary>
        // test 5:
        [TestMethod]
        public void GivenTwoFeetValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(firstValue.Equals(secondValue));
        }

        /// <summary>
        /// testEquality_InchToInch_DifferentValue() – 
        /// Verifies that Quantity(1.0, "inch") does not equal Quantity(2.0, "inch").
        /// Tests: equals() returns false for different inch measurements.
        ///  </summary>
          [TestMethod]
        public void GivenTwoInchValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(2.0, LengthUnit.Inch);
            Assert.IsFalse(firstValue.Equals(secondValue));
        }

        /// <summary> 
        /// testEquality_InvalidUnit() – 
        /// Verifies that invalid unit types are rejected appropriately.
        /// Tests: Exception or validation error for unsupported units.
        /// </summary>
        // Test 7: Invalid unit — enum provides compile time safety
        // LengthUnit enum only allows Feet and Inch
        // Compiler rejects invalid units — no runtime test needed


        /// <summary> </summary>
        /// 
        /// <summary> 
        /// testEquality_NullUnit() – 
        /// Verifies that the null unit is handled appropriately.
        /// Tests: equals() returns false or throws an exception when the unit is null.
         /// </summary>
         /// Test 8: Null unit should throw ArgumentException
        [TestMethod]
        public void GivenQuantityLength_WhenUnitIsNull_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new QuantityLength(1.0, (LengthUnit)999));
        }

        // Test 9: Same reference should be equal (reflexive)
        [TestMethod]
        public void GivenQuantityLength_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.AreEqual(firstValue, firstValue);
        }

           // Test 10: Null comparison should return false
        [TestMethod]
        public void GivenQuantityLength_WhenComparedWithNull_ShouldReturnFalse()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(firstValue.Equals(null));
        }
    }
}