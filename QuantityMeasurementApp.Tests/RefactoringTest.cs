using QuantityMeasurementApp.Models.Enums;
using QuantityMeasurementApp.Models.Interfaces;
using QuantityMeasurementApp.Models.Exceptions;
using QuantityMeasurementApp.Business.Helpers;
using QuantityMeasurementApp.Business.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for UC8 — Refactoring Unit Enum to Standalone with Conversion Responsibility
    /// </summary>
    [TestClass]
    public class RefactoringTest
    {
        private const double Epsilon = 0.0001;

        /// <summary>
        /// testLengthUnitEnum_FeetConstant() –
        /// Verifies LengthUnit.Feet is accessible and converts correctly.
        /// </summary>
        [TestMethod]
        public void GivenFeetUnit_WhenConvertToBaseUnit_ShouldReturnSameValue()
        {
            double result = LengthUnit.Feet.ConvertToBaseUnit(1.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testLengthUnitEnum_InchesConstant() –
        /// Verifies LengthUnit.Inch is accessible and has correct conversion factor ~0.0833.
        /// </summary>
        [TestMethod]
        public void GivenInchUnit_WhenConvertToBaseUnit_ShouldReturnCorrectFactor()
        {
            double result = LengthUnit.Inch.ConvertToBaseUnit(1.0);
            Assert.AreEqual(0.0833, result, Epsilon);
        }

        /// <summary>
        /// testLengthUnitEnum_YardsConstant() –
        /// Verifies LengthUnit.Yard is accessible and has correct conversion factor 3.0.
        /// </summary>
        [TestMethod]
        public void GivenYardUnit_WhenConvertToBaseUnit_ShouldReturnThree()
        {
            double result = LengthUnit.Yard.ConvertToBaseUnit(1.0);
            Assert.AreEqual(3.0, result, Epsilon);
        }

        /// <summary>
        /// testLengthUnitEnum_CentimetersConstant() –
        /// Verifies LengthUnit.Centimeter is accessible and has correct conversion factor ~0.0328.
        /// </summary>
        [TestMethod]
        public void GivenCentimeterUnit_WhenConvertToBaseUnit_ShouldReturnCorrectFactor()
        {
            double result = LengthUnit.Centimeter.ConvertToBaseUnit(1.0);
            Assert.AreEqual(0.0328, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_FeetToFeet() –
        /// Verifies LengthUnit.Feet.ConvertToBaseUnit(5.0) returns 5.0.
        /// </summary>
        [TestMethod]
        public void GivenFiveInFeet_WhenConvertToBaseUnit_ShouldReturnFive()
        {
            double result = LengthUnit.Feet.ConvertToBaseUnit(5.0);
            Assert.AreEqual(5.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_InchesToFeet() –
        /// Verifies LengthUnit.Inch.ConvertToBaseUnit(12.0) returns 1.0.
        /// </summary>
        [TestMethod]
        public void GivenTwelveInches_WhenConvertToBaseUnit_ShouldReturnOneFeet()
        {
            double result = LengthUnit.Inch.ConvertToBaseUnit(12.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_YardsToFeet() –
        /// Verifies LengthUnit.Yard.ConvertToBaseUnit(1.0) returns 3.0.
        /// </summary>
        [TestMethod]
        public void GivenOneYard_WhenConvertToBaseUnit_ShouldReturnThreeFeet()
        {
            double result = LengthUnit.Yard.ConvertToBaseUnit(1.0);
            Assert.AreEqual(3.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_CentimetersToFeet() –
        /// Verifies LengthUnit.Centimeter.ConvertToBaseUnit(30.48) returns ~1.0.
        /// </summary>
        [TestMethod]
        public void GivenThirtyPointFourEightCentimeters_WhenConvertToBaseUnit_ShouldReturnOneFeet()
        {
            double result = LengthUnit.Centimeter.ConvertToBaseUnit(30.48);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_FeetToFeet() –
        /// Verifies LengthUnit.Feet.ConvertFromBaseUnit(2.0) returns 2.0.
        /// </summary>
        [TestMethod]
        public void GivenTwoFeetBase_WhenConvertFromBaseUnit_ShouldReturnTwo()
        {
            double result = LengthUnit.Feet.ConvertFromBaseUnit(2.0);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_FeetToInches() –
        /// Verifies LengthUnit.Inch.ConvertFromBaseUnit(1.0) returns 12.0.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetBase_WhenConvertFromBaseUnitToInches_ShouldReturnTwelve()
        {
            double result = LengthUnit.Inch.ConvertFromBaseUnit(1.0);
            Assert.AreEqual(12.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_FeetToYards() –
        /// Verifies LengthUnit.Yard.ConvertFromBaseUnit(3.0) returns 1.0.
        /// </summary>
        [TestMethod]
        public void GivenThreeFeetBase_WhenConvertFromBaseUnitToYards_ShouldReturnOne()
        {
            double result = LengthUnit.Yard.ConvertFromBaseUnit(3.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_FeetToCentimeters() –
        /// Verifies LengthUnit.Centimeter.ConvertFromBaseUnit(1.0) returns ~30.48.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetBase_WhenConvertFromBaseUnitToCentimeters_ShouldReturnThirtyPointFourEight()
        {
            double result = LengthUnit.Centimeter.ConvertFromBaseUnit(1.0);
            Assert.AreEqual(30.48, result, Epsilon);
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_Equality() –
        /// Verifies Quantity(1.0, FEET).equals(Quantity(12.0, INCHES)) returns true.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenCompared_ShouldReturnTrue()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(feetValue, inchValue);
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_ConvertTo() –
        /// Verifies Quantity(1.0, FEET).ConvertTo(INCHES) returns Quantity(12.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenOneFeet_WhenConvertToInches_ShouldReturnTwelveInches()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = feetValue.ConvertTo(LengthUnit.Inch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_Add() –
        /// Verifies Quantity(1.0, FEET).Add(Quantity(12.0, INCHES)) returns Quantity(2.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAdded_ShouldReturnTwoFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Add(inchValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_AddWithTargetUnit() –
        /// Verifies Quantity(1.0, FEET).Add(Quantity(12.0, INCHES), YARDS) returns ~0.67 YARDS.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAddedWithYardsTarget_ShouldReturnCorrectYards()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Add(inchValue, LengthUnit.Yard);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(0.67, LengthUnit.Yard);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_NullUnit() –
        /// Verifies Quantity(1.0, invalid unit) throws ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenInvalidUnit_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Quantity<LengthUnit>(1.0, null));
        }

        /// <summary>
        /// testQuantity<LengthUnit>Refactored_InvalidValue() –
        /// Verifies Quantity(NaN, FEET) throws ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenNaNValue_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));
        }

        /// <summary>
        /// testBackwardCompatibility_UC1EqualityTests() –
        /// Verifies UC1 equality tests pass with refactored design.
        /// </summary>
        [TestMethod]
        public void GivenTwoFeetValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            Quantity<LengthUnit> firstValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> secondValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.AreEqual(firstValue, secondValue);
        }

        /// <summary>
        /// testBackwardCompatibility_UC5ConversionTests() –
        /// Verifies UC5 conversion tests pass with refactored design.
        /// </summary>
        [TestMethod]
        public void GivenOneYard_WhenConvertedToInches_ShouldReturnThirtySix()
        {
            Quantity<LengthUnit> yardValue = new Quantity<LengthUnit>(1.0, LengthUnit.Yard);
            Quantity<LengthUnit> result = yardValue.ConvertTo(LengthUnit.Inch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(36.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testBackwardCompatibility_UC6AdditionTests() –
        /// Verifies UC6 addition tests pass with refactored design.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAdded_ShouldReturnTwoFeetUC6()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Add(inchValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testBackwardCompatibility_UC7AdditionWithTargetUnitTests() –
        /// Verifies UC7 addition with target unit tests pass with refactored design.
        /// </summary>
        [TestMethod]
        public void GivenFeetAndInches_WhenAddedWithInchesTarget_ShouldReturnTwentyFourInches()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Add(inchValue, LengthUnit.Inch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testArchitecturalScalability_MultipleCategories() –
        /// Confirms refactored pattern supports multiple categories without coupling.
        /// </summary>
        [TestMethod]
        public void GivenLengthUnit_WhenCheckedForIndependence_ShouldNotDependOnQuantity()
        {
            // LengthUnit extension methods work independently of Quantity<LengthUnit>
            double feetResult = LengthUnit.Feet.ConvertToBaseUnit(1.0);
            double inchResult = LengthUnit.Inch.ConvertToBaseUnit(12.0);
            double yardResult = LengthUnit.Yard.ConvertToBaseUnit(1.0);
            double cmResult = LengthUnit.Centimeter.ConvertToBaseUnit(30.48);

            Assert.AreEqual(1.0, feetResult, Epsilon);
            Assert.AreEqual(1.0, inchResult, Epsilon);
            Assert.AreEqual(3.0, yardResult, Epsilon);
            Assert.AreEqual(1.0, cmResult, Epsilon);
        }

        /// <summary>
        /// testRoundTripConversion_RefactoredDesign() –
        /// Verifies round trip conversion preserves value within epsilon.
        /// </summary>
        [TestMethod]
        public void GivenFeetValue_WhenRoundTripConversion_ShouldPreserveOriginalValue()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> converted = original.ConvertTo(LengthUnit.Inch);
            Quantity<LengthUnit> roundTrip = converted.ConvertTo(LengthUnit.Feet);
            Assert.AreEqual(original, roundTrip);
        }

        /// <summary>
        /// testUnitImmutability() –
        /// Verifies LengthUnit enum constants are immutable and thread-safe.
        /// </summary>
        [TestMethod]
        public void GivenLengthUnitConstants_WhenAccessed_ShouldAlwaysReturnSameValues()
        {
            // Enum constants are immutable — calling multiple times returns same result
            double first = LengthUnit.Inch.ConvertToBaseUnit(12.0);
            double second = LengthUnit.Inch.ConvertToBaseUnit(12.0);
            double third = LengthUnit.Inch.ConvertToBaseUnit(12.0);

            Assert.AreEqual(first, second, Epsilon);
            Assert.AreEqual(second, third, Epsilon);
        }
    }
}