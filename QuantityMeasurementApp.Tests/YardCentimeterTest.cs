using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for Yard and Centimeter equality — UC4
    /// </summary>
    [TestClass]
    public class YardCentimeterTest
    {
        /// <summary>
        /// testEquality_YardToYard_SameValue() – 
        /// Verifies that Quantity(1.0, YARDS) equals Quantity(1.0, YARDS).
        /// Tests: equals() returns true for identical yard measurements.
        /// </summary>
        [TestMethod]
        public void GivenTwoYardValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.AreEqual(firstValue, secondValue);
        }

        /// <summary>
        /// testEquality_YardToYard_DifferentValue() –
        /// Verifies that Quantity(1.0, YARDS) does not equal Quantity(2.0, YARDS).
        /// Tests: equals() returns false for different yard measurements.
        /// </summary>
        [TestMethod]
        public void GivenTwoYardValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength secondValue = new QuantityLength(2.0, LengthUnit.Yard);
            Assert.IsFalse(firstValue.Equals(secondValue));
        }

        /// <summary>
        /// testEquality_YardToFeet_EquivalentValue() – 
        /// Verifies that Quantity(1.0, YARDS) equals Quantity(3.0, FEET).
        /// Tests: equals() returns true for equivalent yard-to-feet conversion.
        /// </summary>
        [TestMethod]
        public void GivenOneYardAndThreeFeet_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength feetValue = new QuantityLength(3.0, LengthUnit.Feet);
            Assert.AreEqual(yardValue, feetValue);
        }

        /// <summary>
        /// testEquality_FeetToYard_EquivalentValue() – 
        /// Verifies that Quantity(3.0, FEET) equals Quantity(1.0, YARDS).
        /// Tests: equals() returns true (tests symmetry of conversion).
        /// </summary>
        [TestMethod]
        public void GivenThreeFeetAndOneYard_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength feetValue = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.AreEqual(feetValue, yardValue);
        }

        /// <summary>
        /// testEquality_YardToInches_EquivalentValue() – 
        /// Verifies that Quantity(1.0, YARDS) equals Quantity(36.0, INCHES).
        /// Tests: equals() returns true for equivalent yard-to-inches conversion.
        /// </summary>
        [TestMethod]
        public void GivenOneYardAndThirtySixInches_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength inchValue = new QuantityLength(36.0, LengthUnit.Inch);
            Assert.AreEqual(yardValue, inchValue);
        }

        /// <summary>
        /// testEquality_InchesToYard_EquivalentValue() – 
        /// Verifies that Quantity(36.0, INCHES) equals Quantity(1.0, YARDS).
        /// Tests: equals() returns true (tests symmetry of conversion).
        /// </summary>
        [TestMethod]
        public void GivenThirtySixInchesAndOneYard_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength inchValue = new QuantityLength(36.0, LengthUnit.Inch);
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.AreEqual(inchValue, yardValue);
        }

        /// <summary>
        /// testEquality_YardToFeet_NonEquivalentValue() – 
        /// Verifies that Quantity(1.0, YARDS) does not equal Quantity(2.0, FEET).
        /// Tests: equals() returns false when conversions don't match.
        /// </summary>
        [TestMethod]
        public void GivenOneYardAndTwoFeet_WhenCompared_ShouldReturnFalse()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength feetValue = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.IsFalse(yardValue.Equals(feetValue));
        }

        /// <summary>
        /// testEquality_centimetersToInches_EquivalentValue() – 
        /// Verifies that Quantity(1.0, CENTIMETERS) equals Quantity(0.393701, INCHES).
        /// Tests: equals() returns true (tests symmetry of conversion).
        /// </summary>
        [TestMethod]
        public void GivenOneCentimeterAndEquivalentInches_WhenCompared_ShouldReturnTrue()
        {
            QuantityLength cmValue = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength inchValue = new QuantityLength(0.393701, LengthUnit.Inch);
            Assert.AreEqual(cmValue, inchValue);
        }

        /// <summary>
        /// testEquality_centimetersToFeet_NonEquivalentValue() – 
        /// Verifies that Quantity(1.0, CENTIMETERS) does not equal Quantity(1.0, FEET).
        /// Tests: equals() returns false when conversions don't match.
        /// </summary>
        [TestMethod]
        public void GivenOneCentimeterAndOneFeet_WhenCompared_ShouldReturnFalse()
        {
            QuantityLength cmValue = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(cmValue.Equals(feetValue));
        }

        /// <summary>
        /// testEquality_MultiUnit_TransitiveProperty() – 
        /// Verifies transitive property: if A equals B and B equals C, then A equals C.
        /// Example: Quantity(1.0, YARDS) equals Quantity(3.0, FEET) and Quantity(3.0, FEET) equals Quantity(36.0, INCHES), 
        /// therefore Quantity(1.0, YARDS) equals Quantity(36.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenYardFeetInches_WhenTransitiveCheck_ShouldReturnTrue()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength feetValue = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength inchValue = new QuantityLength(36.0, LengthUnit.Inch);
            Assert.AreEqual(yardValue, feetValue);
            Assert.AreEqual(feetValue, inchValue);
            Assert.AreEqual(yardValue, inchValue);
        }

        /// <summary>
        /// testEquality_YardWithNullUnit() – 
        /// Verifies that the null unit is handled appropriately.
        /// Tests: equals() returns false or throws an exception when the unit is null.
        /// </summary>
        [TestMethod]
        public void GivenYardValue_WhenUnitIsInvalid_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                    new QuantityLength(1.0, null));
        }

        /// <summary>
        /// testEquality_YardSameReference() – 
        /// Verifies that a Quantity yard object equals itself (reflexive property).
        /// Tests: equals() returns true when comparing an object with itself.
        /// </summary>
        [TestMethod]
        public void GivenYardValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.AreEqual(yardValue, yardValue);
        }

        /// <summary>
        /// testEquality_YardNullComparison() – 
        /// Verifies that a Quantity yard object is not equal to null.
        /// Tests: equals() returns false when comparing with null.
        /// </summary>
        [TestMethod]
        public void GivenYardValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            Assert.IsFalse(yardValue.Equals(null));
        }

        /// <summary>
        /// testEquality_CentimetersWithNullUnit() – 
        /// Verifies that the null unit is handled appropriately.
        /// Tests: equals() returns false or throws an exception when the unit is null.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterValue_WhenUnitIsInvalid_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                    new QuantityLength(1.0, null));
        }

        /// <summary>
        /// testEquality_CentimetersSameReference() – 
        /// Verifies that a Quantity centimeter object equals itself (reflexive property).
        /// Tests: equals() returns true when comparing an object with itself.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            QuantityLength cmValue = new QuantityLength(1.0, LengthUnit.Centimeter);
            Assert.AreEqual(cmValue, cmValue);
        }

        /// <summary>
        /// testEquality_CentimetersNullComparison() – 
        /// Verifies that a Quantity centimeter object is not equal to null.
        /// Tests: equals() returns false when comparing with null.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            QuantityLength cmValue = new QuantityLength(1.0, LengthUnit.Centimeter);
            Assert.IsFalse(cmValue.Equals(null));
        }

        /// <summary>
        /// testEquality_AllUnits_ComplexScenario() – 
        /// Verifies complex scenarios combining yards, feet, and inches.
        /// Example: Quantity(2.0, YARDS) equals Quantity(6.0, FEET) equals Quantity(72.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenTwoYards_WhenComparedWithEquivalents_ShouldReturnTrue()
        {
            QuantityLength yardValue = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength feetValue = new QuantityLength(6.0, LengthUnit.Feet);
            QuantityLength inchValue = new QuantityLength(72.0, LengthUnit.Inch);
            Assert.AreEqual(yardValue, feetValue);
            Assert.AreEqual(yardValue, inchValue);
        }
    }
}