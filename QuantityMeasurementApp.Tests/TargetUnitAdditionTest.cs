using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for addition with explicit target unit — UC7
    /// </summary>
    [TestClass]
    public class TargetUnitAdditionTest
    {
        /// <summary>
        /// testAddition_ExplicitTargetUnit_Feet() –
        /// Add(1.0 FEET, 12.0 INCHES, FEET) should return 2.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedWithFeetTarget_ShouldReturnTwoFeet()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Feet);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Inches() –
        /// Add(1.0 FEET, 12.0 INCHES, INCHES) should return 24.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedWithInchesTarget_ShouldReturnTwentyFourInches()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(24.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Yards() –
        /// Add(1.0 FEET, 12.0 INCHES, YARDS) should return ~0.67 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedWithYardsTarget_ShouldReturnCorrectYards()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(0.67, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Centimeters() –
        /// Add(1.0 INCHES, 1.0 INCHES, CENTIMETERS) should return ~5.08 CENTIMETERS.
        /// </summary>
        [TestMethod]
        public void GivenTwoInches_WhenAddedWithCentimetersTarget_ShouldReturnFivePointZeroEightCentimeters()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Centimeter);
            QuantityLength expected = new QuantityLength(5.08, LengthUnit.Centimeter);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_SameAsFirstOperand() –
        /// Add(2.0 YARDS, 3.0 FEET, YARDS) should return 3.0 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenYardsAndFeet_WhenAddedWithYardsTarget_ShouldReturnThreeYards()
        {
            QuantityLength firstValue = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength secondValue = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(3.0, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_SameAsSecondOperand() –
        /// Add(2.0 YARDS, 3.0 FEET, FEET) should return 9.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenYardsAndFeet_WhenAddedWithFeetTarget_ShouldReturnNineFeet()
        {
            QuantityLength firstValue = new QuantityLength(2.0, LengthUnit.Yard);
            QuantityLength secondValue = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Feet);
            QuantityLength expected = new QuantityLength(9.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Commutativity() –
        /// Add(1.0 FEET, 12.0 INCHES, YARDS) should equal Add(12.0 INCHES, 1.0 FEET, YARDS).
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedBothWaysWithYardsTarget_ShouldBeCommutative()
        {
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength inchValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result1 = feetValue.Add(inchValue, LengthUnit.Yard);
            QuantityLength result2 = inchValue.Add(feetValue, LengthUnit.Yard);
            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_WithZero() –
        /// Add(5.0 FEET, 0.0 INCHES, YARDS) should return ~1.67 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndZeroInches_WhenAddedWithYardsTarget_ShouldReturnCorrectYards()
        {
            QuantityLength firstValue = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(0.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(1.67, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_NegativeValues() –
        /// Add(5.0 FEET, -2.0 FEET, INCHES) should return 36.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndNegativeTwoFeet_WhenAddedWithInchesTarget_ShouldReturnThirtySixInches()
        {
            QuantityLength firstValue = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(-2.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(36.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_NullTargetUnit() –
        /// Add(1.0 FEET, 12.0 INCHES, invalid unit) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenTargetUnitIsInvalid_ShouldThrowException()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.Throws<ArgumentException>(() =>
                firstValue.Add(secondValue, (LengthUnit)999));
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_LargeToSmallScale() –
        /// Add(1000.0 FEET, 500.0 FEET, INCHES) should return 18000.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenLargeFeetValues_WhenAddedWithInchesTarget_ShouldReturnCorrectInches()
        {
            QuantityLength firstValue = new QuantityLength(1000.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(500.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(18000.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_SmallToLargeScale() –
        /// Add(12.0 INCHES, 12.0 INCHES, YARDS) should return ~0.67 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenTwelveInchesAndTwelveInches_WhenAddedWithYardsTarget_ShouldReturnCorrectYards()
        {
            QuantityLength firstValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(0.67, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_AllUnitCombinations() –
        /// Comprehensive test covering all unit pairs with multiple target units.
        /// </summary>
        [TestMethod]
        public void GivenAllUnitCombinations_WhenAdded_ShouldReturnCorrectResults()
        {
            // Feet + Inches → Yards
            QuantityLength result1 = new QuantityLength(3.0, LengthUnit.Feet)
                .Add(new QuantityLength(36.0, LengthUnit.Inch), LengthUnit.Yard);
            Assert.AreEqual(new QuantityLength(2.0, LengthUnit.Yard), result1);

            // Yards + Feet → Inches
            QuantityLength result2 = new QuantityLength(1.0, LengthUnit.Yard)
                .Add(new QuantityLength(3.0, LengthUnit.Feet), LengthUnit.Inch);
            Assert.AreEqual(new QuantityLength(72.0, LengthUnit.Inch), result2);

            // Centimeters + Inches → Feet
            QuantityLength result3 = new QuantityLength(30.48, LengthUnit.Centimeter)
                .Add(new QuantityLength(12.0, LengthUnit.Inch), LengthUnit.Feet);
            Assert.AreEqual(new QuantityLength(2.0, LengthUnit.Feet), result3);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_PrecisionTolerance() –
        /// Conversion results within epsilon tolerance.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterAndInch_WhenAddedWithFeetTarget_ShouldBeWithinEpsilon()
        {
            QuantityLength firstValue = new QuantityLength(30.48, LengthUnit.Centimeter);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue, LengthUnit.Feet);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }
    }
}