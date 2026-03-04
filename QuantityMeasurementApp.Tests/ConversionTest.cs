using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for unit conversion — UC5
    /// </summary>
    [TestClass]
    public class ConversionTest
    {
        /// <summary>
        /// testConversion_FeetToInches() –
        /// convert(1.0, FEET, INCHES) should return 12.0.
        /// </summary>
        [TestMethod]
        public void GivenOneFeet_WhenConvertedToInches_ShouldReturnTwelve()
        {
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            QuantityLength result = feetValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_InchesToFeet() –
        /// convert(24.0, INCHES, FEET) should return 2.0.
        /// </summary>
        [TestMethod]
        public void GivenTwentyFourInches_WhenConvertedToFeet_ShouldReturnTwo()
        {
            QuantityLength inchValue = new QuantityLength(24.0, LengthUnit.Inch);
            QuantityLength result = inchValue.ConvertTo(LengthUnit.Feet);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_YardsToInches() –
        /// convert(1.0, YARDS, INCHES) should return 36.0.
        /// </summary>
        [TestMethod]
        public void GivenOneYard_WhenConvertedToInches_ShouldReturnThirtySix()
        {
            QuantityLength yardValue = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength result = yardValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(36.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_InchesToYards() –
        /// convert(72.0, INCHES, YARDS) should return 2.0.
        /// </summary>
        [TestMethod]
        public void GivenSeventyTwoInches_WhenConvertedToYards_ShouldReturnTwo()
        {
            QuantityLength inchValue = new QuantityLength(72.0, LengthUnit.Inch);
            QuantityLength result = inchValue.ConvertTo(LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_CentimetersToInches() –
        /// convert(2.54, CENTIMETERS, INCHES) should return ~1.0 within epsilon.
        /// </summary>
        [TestMethod]
        public void GivenTwoPointFiveFourCentimeters_WhenConvertedToInches_ShouldReturnOne()
        {
            QuantityLength cmValue = new QuantityLength(2.54, LengthUnit.Centimeter);
            QuantityLength result = cmValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(1.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_FeetToYard() –
        /// convert(6.0, FEET, YARDS) should return 2.0.
        /// </summary>
        [TestMethod]
        public void GivenSixFeet_WhenConvertedToYards_ShouldReturnTwo()
        {
            QuantityLength feetValue = new QuantityLength(6.0, LengthUnit.Feet);
            QuantityLength result = feetValue.ConvertTo(LengthUnit.Yard);
            QuantityLength expected = new QuantityLength(2.0, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_RoundTrip_PreservesValue() –
        /// convert(convert(v, A, B), B, A) should return original value.
        /// </summary>
        [TestMethod]
        public void GivenFeetValue_WhenRoundTripConversion_ShouldReturnOriginalValue()
        {
            QuantityLength original = new QuantityLength(5.0, LengthUnit.Feet);
            QuantityLength converted = original.ConvertTo(LengthUnit.Inch);
            QuantityLength roundTrip = converted.ConvertTo(LengthUnit.Feet);
            Assert.AreEqual(original, roundTrip);
        }

        /// <summary>
        /// testConversion_ZeroValue() –
        /// convert(0.0, FEET, INCHES) should return 0.0.
        /// </summary>
        [TestMethod]
        public void GivenZeroFeet_WhenConvertedToInches_ShouldReturnZero()
        {
            QuantityLength feetValue = new QuantityLength(0.0, LengthUnit.Feet);
            QuantityLength result = feetValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(0.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_NegativeValue() –
        /// convert(-1.0, FEET, INCHES) should return -12.0.
        /// </summary>
        [TestMethod]
        public void GivenNegativeOneFeet_WhenConvertedToInches_ShouldReturnNegativeTwelve()
        {
            QuantityLength feetValue = new QuantityLength(-1.0, LengthUnit.Feet);
            QuantityLength result = feetValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(-12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_InvalidUnit_Throws() –
        /// Passing invalid unit should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenInvalidUnit_WhenConverting_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
                feetValue.ConvertTo(null);
            });
        }

        /// <summary>
        /// testConversion_NaNOrInfinite_Throws() –
        /// Passing NaN or Infinity should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenNaNValue_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new QuantityLength(double.NaN, LengthUnit.Feet));
        }

        [TestMethod]
        public void GivenInfiniteValue_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new QuantityLength(double.PositiveInfinity, LengthUnit.Feet));
        }

        /// <summary>
        /// testConversion_PrecisionTolerance() –
        /// Conversion results within epsilon tolerance.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterValue_WhenConvertedToInches_ShouldBeWithinEpsilon()
        {
            QuantityLength cmValue = new QuantityLength(1.0, LengthUnit.Centimeter);
            QuantityLength result = cmValue.ConvertTo(LengthUnit.Inch);
            QuantityLength expected = new QuantityLength(0.39, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }
    }
}