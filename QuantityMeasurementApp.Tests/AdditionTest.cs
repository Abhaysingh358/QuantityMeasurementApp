using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for length addition — UC6
    /// </summary>
    [TestClass]
    public class AdditionTest
    {
        /// <summary>
        /// testAddition_SameUnit_FeetPlusFeet() –
        /// Add(1.0 FEET, 2.0 FEET) should return 3.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwoFeet_WhenAdded_ShouldReturnThreeFeet()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(2.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(3.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_SameUnit_InchPlusInch() –
        /// Add(6.0 INCHES, 6.0 INCHES) should return 12.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenSixInchesAndSixInches_WhenAdded_ShouldReturnTwelveInches()
        {
            QuantityLength firstValue = new QuantityLength(6.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(6.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_FeetPlusInches() –
        /// Add(1.0 FEET, 12.0 INCHES) should return 2.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAdded_ShouldReturnTwoFeet()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_InchPlusFeet() –
        /// Add(12.0 INCHES, 1.0 FEET) should return 24.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenTwelveInchesAndOneFeet_WhenAdded_ShouldReturnTwentyFourInches()
        {
            QuantityLength firstValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(24.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_YardPlusFeet() –
        /// Add(1.0 YARDS, 3.0 FEET) should return 2.0 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenOneYardAndThreeFeet_WhenAdded_ShouldReturnTwoYards()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength secondValue = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_CentimeterPlusInch() –
        /// Add(2.54 CENTIMETERS, 1.0 INCHES) should return ~5.08 CENTIMETERS.
        /// </summary>
        [TestMethod]
        public void GivenTwoPointFiveFourCentimetersAndOneInch_WhenAdded_ShouldReturnFivePointZEightCentimeters()
        {
            QuantityLength firstValue = new QuantityLength(2.54, LengthUnit.Centimeter);
            QuantityLength secondValue = new QuantityLength(1.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(5.08, LengthUnit.Centimeter);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_Commutativity() –
        /// Add(1.0 FEET, 12.0 INCHES) should equal Add(12.0 INCHES, 1.0 FEET) in base unit.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedBothWays_ShouldBeCommutative()
        {
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength inchValue = new QuantityLength(12.0, LengthUnit.Inch);
            QuantityLength result1 = feetValue.Add(inchValue);
            QuantityLength result2 = inchValue.Add(feetValue);
            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// testAddition_WithZero() –
        /// Add(5.0 FEET, 0.0 INCHES) should return 5.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndZeroInches_WhenAdded_ShouldReturnFiveFeet()
        {
            QuantityLength firstValue = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(0.0, LengthUnit.Inch);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(5.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NegativeValues() –
        /// Add(5.0 FEET, -2.0 FEET) should return 3.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndNegativeTwoFeet_WhenAdded_ShouldReturnThreeFeet()
        {
            QuantityLength firstValue = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(-2.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(3.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NullSecondOperand() –
        /// Add(1.0 FEET, null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndNullValue_WhenAdded_ShouldThrowException()
        {
            QuantityLength firstValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => firstValue.Add(null));
        }

        /// <summary>
        /// testAddition_LargeValues() –
        /// Add(1e6 FEET, 1e6 FEET) should return 2e6 FEET.
        /// </summary>
        [TestMethod]
        public void GivenLargeFeetValues_WhenAdded_ShouldReturnCorrectSum()
        {
            QuantityLength firstValue = new QuantityLength(1000000.0, LengthUnit.Feet);
            QuantityLength secondValue = new QuantityLength(1000000.0, LengthUnit.Feet);
            QuantityLength result = firstValue.Add(secondValue);
            QuantityLength expected = new QuantityLength(2000000.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

    /// <summary>
    /// testAddition_SmallValues() –
    /// Add(0.001 FEET, 0.002 FEET) should return ~0.003 FEET within epsilon.
    /// </summary>
    [TestMethod]
    public void GivenSmallFeetValues_WhenAdded_ShouldReturnCorrectSum()
    {
        QuantityLength firstValue = new QuantityLength(0.1, LengthUnit.Feet);
        QuantityLength secondValue = new QuantityLength(0.2, LengthUnit.Feet);
        QuantityLength result = firstValue.Add(secondValue);
        QuantityLength expected = new QuantityLength(0.3, LengthUnit.Feet);
        Assert.AreEqual(expected, result);
    }
    }
}