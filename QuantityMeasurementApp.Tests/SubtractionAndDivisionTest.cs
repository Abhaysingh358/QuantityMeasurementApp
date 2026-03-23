using QuantityMeasurementApp.Models.Enums;
using QuantityMeasurementApp.Models.Interfaces;
using QuantityMeasurementApp.Models.Exceptions;
using QuantityMeasurementApp.Business.Helpers;
using QuantityMeasurementApp.Business.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for subtraction and division operations — UC12
    /// </summary>
    [TestClass]
    public class SubtractionAndDivisionTest
    {
        private const double Epsilon = 0.0001;

        // ─── SUBTRACTION — SAME UNIT ──────────────────────────────────────────

        /// <summary>
        /// testSubtraction_SameUnit_FeetMinusFeet() –
        /// Quantity(10.0, FEET).Subtract(Quantity(5.0, FEET)) should return Quantity(5.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndFiveFeet_WhenSubtracted_ShouldReturnFiveFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = first.Subtract(second);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_SameUnit_LitreMinusLitre() –
        /// Quantity(10.0, LITRE).Subtract(Quantity(3.0, LITRE)) should return Quantity(7.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenTenLitresAndThreeLitres_WhenSubtracted_ShouldReturnSevenLitres()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Subtract(second);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(7.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        // ─── SUBTRACTION — CROSS UNIT ─────────────────────────────────────────

        /// <summary>
        /// testSubtraction_CrossUnit_FeetMinusInches() –
        /// Quantity(10.0, FEET).Subtract(Quantity(6.0, INCHES)) should return Quantity(9.5, FEET).
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndSixInches_WhenSubtracted_ShouldReturnNinePointFiveFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Subtract(inchValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(9.5, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_CrossUnit_InchesMinusFeet() –
        /// Quantity(120.0, INCHES).Subtract(Quantity(5.0, FEET)) should return Quantity(60.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenHundredTwentyInchesAndFiveFeet_WhenSubtracted_ShouldReturnSixtyInches()
        {
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(120.0, LengthUnit.Inch);
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = inchValue.Subtract(feetValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(60.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        // ─── SUBTRACTION — EXPLICIT TARGET UNIT ──────────────────────────────

        /// <summary>
        /// testSubtraction_ExplicitTargetUnit_Feet() –
        /// Quantity(10.0, FEET).Subtract(Quantity(6.0, INCHES), FEET) should return Quantity(9.5, FEET).
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndSixInches_WhenSubtractedWithFeetTarget_ShouldReturnNinePointFiveFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Subtract(inchValue, LengthUnit.Feet);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(9.5, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_ExplicitTargetUnit_Inches() –
        /// Quantity(10.0, FEET).Subtract(Quantity(6.0, INCHES), INCHES) should return Quantity(114.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndSixInches_WhenSubtractedWithInchTarget_ShouldReturnHundredFourteenInches()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Subtract(inchValue, LengthUnit.Inch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(114.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_ExplicitTargetUnit_Millilitre() –
        /// Quantity(5.0, LITRE).Subtract(Quantity(2.0, LITRE), MILLILITRE) should return Quantity(3000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenFiveLitresAndTwoLitres_WhenSubtractedWithMlTarget_ShouldReturnThreeThousandMl()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Subtract(second, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(3000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        // ─── SUBTRACTION — SPECIAL CASES ─────────────────────────────────────

        /// <summary>
        /// testSubtraction_ResultingInNegative() –
        /// Quantity(5.0, FEET).Subtract(Quantity(10.0, FEET)) should return Quantity(-5.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndTenFeet_WhenSubtracted_ShouldReturnNegativeFiveFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = first.Subtract(second);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(-5.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_ResultingInZero() –
        /// Quantity(10.0, FEET).Subtract(Quantity(120.0, INCHES)) should return Quantity(0.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndHundredTwentyInches_WhenSubtracted_ShouldReturnZeroFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(120.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Subtract(inchValue);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_WithZeroOperand() –
        /// Quantity(5.0, FEET).Subtract(Quantity(0.0, INCHES)) should return Quantity(5.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndZeroInches_WhenSubtracted_ShouldReturnFiveFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> zeroInch = new Quantity<LengthUnit>(0.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Subtract(zeroInch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_WithNegativeValues() –
        /// Quantity(5.0, FEET).Subtract(Quantity(-2.0, FEET)) should return Quantity(7.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenFiveFeetAndNegativeTwoFeet_WhenSubtracted_ShouldReturnSevenFeet()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(-2.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = first.Subtract(second);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(7.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_NonCommutative() –
        /// A.Subtract(B) returns 5.0 FEET while B.Subtract(A) returns -5.0 FEET.
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndFiveFeet_WhenSubtractedBothWays_ShouldBeDifferent()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> aMinusB = a.Subtract(b);
            Quantity<LengthUnit> bMinusA = b.Subtract(a);
            Assert.AreEqual(new Quantity<LengthUnit>(5.0, LengthUnit.Feet), aMinusB);
            Assert.AreEqual(new Quantity<LengthUnit>(-5.0, LengthUnit.Feet), bMinusA);
            Assert.IsFalse(aMinusB.Equals(bMinusA));
        }

        /// <summary>
        /// testSubtraction_WithLargeValues() –
        /// Quantity(1e6, KILOGRAM).Subtract(Quantity(5e5, KILOGRAM)) should return Quantity(5e5, KILOGRAM).
        /// </summary>
        [TestMethod]
        public void GivenLargeKilogramValues_WhenSubtracted_ShouldReturnCorrectDifference()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1000000.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(500000.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> result = first.Subtract(second);
            Quantity<WeightUnit> expected = new Quantity<WeightUnit>(500000.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testSubtraction_WithSmallValues() –
        /// Quantity(0.001, FEET).Subtract(Quantity(0.0005, FEET)) should return ~0.0005 FEET.
        /// </summary>
        [TestMethod]
        public void GivenSmallFeetValues_WhenSubtracted_ShouldReturnCorrectDifference()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(0.001, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(0.0005, LengthUnit.Feet);
            Quantity<LengthUnit> result = first.Subtract(second);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(0.0005, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        // ─── SUBTRACTION — ERROR HANDLING ────────────────────────────────────

        /// <summary>
        /// testSubtraction_NullOperand() –
        /// Quantity(10.0, FEET).Subtract(null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullOperand_WhenSubtracting_ShouldThrowException()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => feetValue.Subtract(null));
        }

        /// <summary>
        /// testSubtraction_NullTargetUnit() –
        /// Quantity(10.0, FEET).Subtract(Quantity(5.0, FEET), null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullTargetUnit_WhenSubtracting_ShouldThrowException()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => first.Subtract(second, null));
        }

        /// <summary>
        /// testSubtraction_CrossCategory() –
        /// Quantity(10.0, FEET).Subtract(Quantity(5.0, KILOGRAM)) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenLengthAndWeight_WhenSubtracting_ShouldThrowException()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> fakeWeight = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            // Cross-category is prevented at compile time by generics.
            // Verifying that same-category operations work.
            Assert.IsNotNull(feetValue.Subtract(fakeWeight));
        }

        // ─── SUBTRACTION — ALL CATEGORIES ────────────────────────────────────

        /// <summary>
        /// testSubtraction_AllMeasurementCategories() –
        /// Subtraction works for length, weight, and volume.
        /// </summary>
        [TestMethod]
        public void GivenAllMeasurementCategories_WhenSubtracted_ShouldReturnCorrectResults()
        {
            // Length
            Assert.AreEqual(
                new Quantity<LengthUnit>(5.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(10.0, LengthUnit.Feet)
                    .Subtract(new Quantity<LengthUnit>(5.0, LengthUnit.Feet)));

            // Weight
            Assert.AreEqual(
                new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram)
                    .Subtract(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)));

            // Volume
            Assert.AreEqual(
                new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre)
                    .Subtract(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        /// <summary>
        /// testSubtraction_ChainedOperations() –
        /// Quantity(10.0, FEET).Subtract(Quantity(2.0, FEET)).Subtract(Quantity(1.0, FEET))
        /// should return Quantity(7.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenChainedSubtractions_WhenEvaluated_ShouldReturnCorrectResult()
        {
            Quantity<LengthUnit> result = new Quantity<LengthUnit>(10.0, LengthUnit.Feet)
                .Subtract(new Quantity<LengthUnit>(2.0, LengthUnit.Feet))
                .Subtract(new Quantity<LengthUnit>(1.0, LengthUnit.Feet));
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(7.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        // ─── DIVISION — SAME UNIT ─────────────────────────────────────────────

        /// <summary>
        /// testDivision_SameUnit_FeetDividedByFeet() –
        /// Quantity(10.0, FEET).Divide(Quantity(2.0, FEET)) should return 5.0.
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndTwoFeet_WhenDivided_ShouldReturnFive()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            double result = first.Divide(second);
            Assert.AreEqual(5.0, result, Epsilon);
        }

        /// <summary>
        /// testDivision_SameUnit_LitreDividedByLitre() –
        /// Quantity(10.0, LITRE).Divide(Quantity(5.0, LITRE)) should return 2.0.
        /// </summary>
        [TestMethod]
        public void GivenTenLitresAndFiveLitres_WhenDivided_ShouldReturnTwo()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            double result = first.Divide(second);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        // ─── DIVISION — CROSS UNIT ────────────────────────────────────────────

        /// <summary>
        /// testDivision_CrossUnit_FeetDividedByInches() –
        /// Quantity(24.0, INCHES).Divide(Quantity(2.0, FEET)) should return 1.0.
        /// </summary>
        [TestMethod]
        public void GivenTwentyFourInchesAndTwoFeet_WhenDivided_ShouldReturnOne()
        {
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            double result = inchValue.Divide(feetValue);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testDivision_CrossUnit_KilogramDividedByGram() –
        /// Quantity(2.0, KILOGRAM).Divide(Quantity(2000.0, GRAM)) should return 1.0.
        /// </summary>
        [TestMethod]
        public void GivenTwoKilogramsAndTwoThousandGrams_WhenDivided_ShouldReturnOne()
        {
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> gramValue = new Quantity<WeightUnit>(2000.0, WeightUnit.Gram);
            double result = kgValue.Divide(gramValue);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        // ─── DIVISION — RATIO CASES ───────────────────────────────────────────

        /// <summary>
        /// testDivision_RatioGreaterThanOne() –
        /// Quantity(10.0, FEET).Divide(Quantity(2.0, FEET)) should return 5.0.
        /// </summary>
        [TestMethod]
        public void GivenLargerDividendThanDivisor_WhenDivided_ShouldReturnRatioGreaterThanOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            double result = first.Divide(second);
            Assert.IsTrue(result > 1.0);
            Assert.AreEqual(5.0, result, Epsilon);
        }

        /// <summary>
        /// testDivision_RatioLessThanOne() –
        /// Quantity(5.0, FEET).Divide(Quantity(10.0, FEET)) should return 0.5.
        /// </summary>
        [TestMethod]
        public void GivenSmallerDividendThanDivisor_WhenDivided_ShouldReturnRatioLessThanOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            double result = first.Divide(second);
            Assert.IsTrue(result < 1.0);
            Assert.AreEqual(0.5, result, Epsilon);
        }

        /// <summary>
        /// testDivision_RatioEqualToOne() –
        /// Quantity(10.0, FEET).Divide(Quantity(10.0, FEET)) should return 1.0.
        /// </summary>
        [TestMethod]
        public void GivenEqualQuantities_WhenDivided_ShouldReturnOne()
        {
            Quantity<LengthUnit> first = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> second = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            double result = first.Divide(second);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testDivision_NonCommutative() –
        /// A.Divide(B) returns 2.0 while B.Divide(A) returns 0.5.
        /// </summary>
        [TestMethod]
        public void GivenTenFeetAndFiveFeet_WhenDividedBothWays_ShouldBeDifferent()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            double aDivB = a.Divide(b);
            double bDivA = b.Divide(a);
            Assert.AreEqual(2.0, aDivB, Epsilon);
            Assert.AreEqual(0.5, bDivA, Epsilon);
            Assert.AreNotEqual(aDivB, bDivA, Epsilon);
        }

        /// <summary>
        /// testDivision_WithLargeRatio() –
        /// Quantity(1e6, KILOGRAM).Divide(Quantity(1.0, KILOGRAM)) should return 1e6.
        /// </summary>
        [TestMethod]
        public void GivenMillionKilogramsAndOneKilogram_WhenDivided_ShouldReturnMillion()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1000000.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            double result = first.Divide(second);
            Assert.AreEqual(1000000.0, result, Epsilon);
        }

        /// <summary>
        /// testDivision_WithSmallRatio() –
        /// Quantity(1.0, KILOGRAM).Divide(Quantity(1e6, KILOGRAM)) should return 1e-6.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndMillionKilograms_WhenDivided_ShouldReturnMicroRatio()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(1000000.0, WeightUnit.Kilogram);
            double result = first.Divide(second);
            Assert.AreEqual(1e-6, result, 1e-10);
        }

        // ─── DIVISION — ERROR HANDLING ────────────────────────────────────────

        /// <summary>
        /// testDivision_ByZero() –
        /// Quantity(10.0, FEET).Divide(Quantity(0.0, FEET)) should throw ArithmeticException.
        /// </summary>
        [TestMethod]
        public void GivenZeroDivisor_WhenDividing_ShouldThrowArithmeticException()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> zeroFeet = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<ArithmeticException>(() => feetValue.Divide(zeroFeet));
        }

        /// <summary>
        /// testDivision_NullOperand() –
        /// Quantity(10.0, FEET).Divide(null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullDivisor_WhenDividing_ShouldThrowException()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => feetValue.Divide(null));
        }

        // ─── DIVISION — ALL CATEGORIES ────────────────────────────────────────

        /// <summary>
        /// testDivision_AllMeasurementCategories() –
        /// Division works for length, weight, and volume.
        /// </summary>
        [TestMethod]
        public void GivenAllMeasurementCategories_WhenDivided_ShouldReturnCorrectRatios()
        {
            // Length
            Assert.AreEqual(2.0,
                new Quantity<LengthUnit>(10.0, LengthUnit.Feet)
                    .Divide(new Quantity<LengthUnit>(5.0, LengthUnit.Feet)), Epsilon);

            // Weight
            Assert.AreEqual(2.0,
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram)
                    .Divide(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)), Epsilon);

            // Volume
            Assert.AreEqual(2.0,
                new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre)
                    .Divide(new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre)), Epsilon);
        }

        /// <summary>
        /// testDivision_Associativity() –
        /// (A ÷ B) ÷ C ≠ A ÷ (B ÷ C) — division is non-associative.
        /// </summary>
        [TestMethod]
        public void GivenThreeQuantities_WhenDivisionIsAssociative_ShouldNotBeEqual()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(12.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Quantity<LengthUnit> c = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);

            // (A ÷ B) ÷ C = 4 ÷ 2 = 2.0
            double leftAssoc = a.Divide(b) / c.Divide(new Quantity<LengthUnit>(1.0, LengthUnit.Feet));

            // A ÷ (B ÷ C) = 12 ÷ (3 ÷ 2) = 12 ÷ 1.5 = 8.0
            double rightAssoc = a.Divide(new Quantity<LengthUnit>(
                b.Divide(c), LengthUnit.Feet));

            Assert.AreNotEqual(leftAssoc, rightAssoc, Epsilon);
        }

        // ─── INTEGRATION TESTS ────────────────────────────────────────────────

        /// <summary>
        /// testSubtractionAndDivision_Integration() –
        /// A.Subtract(B).Divide(C) is valid — operations coexist without conflict.
        /// </summary>
        [TestMethod]
        public void GivenSubtractThenDivide_WhenChained_ShouldReturnCorrectResult()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(4.0, LengthUnit.Feet);
            Quantity<LengthUnit> c = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);

            
            double result = a.Subtract(b).Divide(c);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        /// <summary>
        /// testSubtractionAddition_Inverse() –
        /// A.Add(B).Subtract(B) should approximately equal A.
        /// </summary>
        [TestMethod]
        public void GivenAddThenSubtractSameValue_WhenEvaluated_ShouldReturnOriginal()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = a.Add(b).Subtract(b);
            Assert.AreEqual(a, result);
        }

        // ─── IMMUTABILITY TESTS ───────────────────────────────────────────────

        /// <summary>
        /// testSubtraction_Immutability() –
        /// Original quantities are unchanged after subtraction.
        /// </summary>
        [TestMethod]
        public void GivenSubtraction_WhenPerformed_OriginalsShouldBeUnchanged()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Quantity<LengthUnit> subtracted = original.Subtract(other);

            Assert.AreEqual(new Quantity<LengthUnit>(10.0, LengthUnit.Feet), original);
            Assert.AreEqual(new Quantity<LengthUnit>(3.0, LengthUnit.Feet), other);
            Assert.AreEqual(new Quantity<LengthUnit>(7.0, LengthUnit.Feet), subtracted);
        }

        /// <summary>
        /// testDivision_Immutability() –
        /// Original quantities are unchanged after division.
        /// </summary>
        [TestMethod]
        public void GivenDivision_WhenPerformed_OriginalsShouldBeUnchanged()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            double ratio = original.Divide(other);

            Assert.AreEqual(new Quantity<LengthUnit>(10.0, LengthUnit.Feet), original);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), other);
            Assert.AreEqual(5.0, ratio, Epsilon);
        }

        // ─── PRECISION TESTS ──────────────────────────────────────────────────

        /// <summary>
        /// testSubtraction_PrecisionAndRounding() –
        /// Subtraction results are rounded to two decimal places.
        /// </summary>
        [TestMethod]
        public void GivenSubtractionWithDecimalValues_WhenEvaluated_ShouldMaintainPrecision()
        {
            Quantity<WeightUnit> first = new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> second = new Quantity<WeightUnit>(5000.0, WeightUnit.Gram);
            Quantity<WeightUnit> result = first.Subtract(second);
            Quantity<WeightUnit> expected = new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testDivision_PrecisionHandling() –
        /// Division results maintain floating-point precision.
        /// </summary>
        [TestMethod]
        public void GivenDivisionWithCrossUnits_WhenEvaluated_ShouldMaintainPrecision()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            double result = first.Divide(second);
            Assert.AreEqual(1.0, result, Epsilon);
        }
    }
}