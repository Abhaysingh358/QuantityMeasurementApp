using QuantityMeasurementApp.Models.Enums;
using QuantityMeasurementApp.Models.Interfaces;
using QuantityMeasurementApp.Models.Exceptions;
using QuantityMeasurementApp.Business.Helpers;
using QuantityMeasurementApp.Business.Services;

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
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_SameUnit_InchPlusInch() –
        /// Add(6.0 INCHES, 6.0 INCHES) should return 12.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenSixInchesAndSixInches_WhenAdded_ShouldReturnTwelveInches()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_FeetPlusInches() –
        /// Add(1.0 FEET, 12.0 INCHES) should return 2.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAdded_ShouldReturnTwoFeet()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_InchPlusFeet() –
        /// Add(12.0 INCHES, 1.0 FEET) should return 24.0 INCHES.
        /// </summary>
        [TestMethod]
        public void GivenTwelveInchesAndOneFeet_WhenAdded_ShouldReturnTwentyFourInches()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_YardPlusFeet() –
        /// Add(1.0 YARDS, 3.0 FEET) should return 2.0 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenOneYardAndThreeFeet_WhenAdded_ShouldReturnTwoYards()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2.0, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_CentimeterPlusInch() –
        /// Add(2.54 CENTIMETERS, 1.0 INCHES) should return ~5.08 CENTIMETERS.
        /// </summary>
        [TestMethod]
        public void GivenTwoPointFiveFourCentimetersAndOneInch_WhenAdded_ShouldReturnFivePointZEightCentimeters()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(2.54, LengthUnit.Centimeter);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(5.08, LengthUnit.Centimeter);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_Commutativity() –
        /// Add(1.0 FEET, 12.0 INCHES) should equal Add(12.0 INCHES, 1.0 FEET) in base unit.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedBothWays_ShouldBeCommutative()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result1 = feetValue.Add(inchValue);
            Quantity<LengthUnit> result2 = inchValue.Add(feetValue);
            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// testAddition_WithZero() –
        /// Add(5.0 FEET, 0.0 INCHES) should return 5.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndZeroInches_WhenAdded_ShouldReturnFiveFeet()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(0.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NegativeValues() –
        /// Add(5.0 FEET, -2.0 FEET) should return 3.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndNegativeTwoFeet_WhenAdded_ShouldReturnThreeFeet()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(-2.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NullSecondOperand() –
        /// Add(1.0 FEET, null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndNullValue_WhenAdded_ShouldThrowException()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => firstValue.Add(null));
        }

        /// <summary>
        /// testAddition_LargeValues() –
        /// Add(1e6 FEET, 1e6 FEET) should return 2e6 FEET.
        /// </summary>
        [TestMethod]
        public void GivenLargeFeetValues_WhenAdded_ShouldReturnCorrectSum()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1000000.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(1000000.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = firstValue.Add(secondValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2000000.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

    /// <summary>
    /// testAddition_SmallValues() –
    /// Add(0.001 FEET, 0.002 FEET) should return ~0.003 FEET within epsilon.
    /// </summary>
    [TestMethod]
    public void GivenSmallFeetValues_WhenAdded_ShouldReturnCorrectSum()
    {
        Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(0.1, LengthUnit.Feet);
        Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(0.2, LengthUnit.Feet);
        Quantity<LengthUnit> result = firstValue.Add(secondValue);
        Quantity<LengthUnit> expected = new Quantity<LengthUnit>(0.3, LengthUnit.Feet);
        Assert.AreEqual(expected, result);
    }
    }
}