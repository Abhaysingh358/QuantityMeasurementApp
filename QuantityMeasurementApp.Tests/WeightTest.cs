using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for weight measurement equality, conversion, and addition — UC9
    /// </summary>
    [TestClass]
    public class WeightTest
    {
        private const double Epsilon = 0.0001;

        /// <summary>
        /// testEquality_KilogramToKilogram_SameValue() –
        /// Quantity(1.0, KILOGRAM).equals(Quantity(1.0, KILOGRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenTwoKilogramValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            QuantityWeight first = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight second = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// testEquality_KilogramToKilogram_DifferentValue() –
        /// Quantity(1.0, KILOGRAM).equals(Quantity(2.0, KILOGRAM)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenTwoKilogramValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            QuantityWeight first = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight second = new QuantityWeight(2.0, WeightUnit.Kilogram);
            Assert.IsFalse(first.Equals(second));
        }

        /// <summary>
        /// testEquality_KilogramToGram_EquivalentValue() –
        /// Quantity(1.0, KILOGRAM).equals(Quantity(1000.0, GRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndThousandGrams_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            Assert.AreEqual(kgValue, gValue);
        }

        /// <summary>
        /// testEquality_GramToKilogram_EquivalentValue() –
        /// Quantity(1000.0, GRAM).equals(Quantity(1.0, KILOGRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenThousandGramsAndOneKilogram_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(gValue, kgValue);
        }

        /// <summary>
        /// testEquality_WeightVsLength_Incompatible() –
        /// Quantity(1.0, KILOGRAM).equals(Quantity(1.0, FEET)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenKilogramAndFeet_WhenCompared_ShouldReturnFalse()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityLength feetValue = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(kgValue.Equals(feetValue));
        }

        /// <summary>
        /// testEquality_NullComparison() –
        /// Quantity(1.0, KILOGRAM).equals(null) should return false.
        /// </summary>
        [TestMethod]
        public void GivenKilogramValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(kgValue.Equals(null));
        }

        /// <summary>
        /// testEquality_SameReference() –
        /// A weight object equals itself (reflexive property).
        /// </summary>
        [TestMethod]
        public void GivenKilogramValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(kgValue, kgValue);
        }

        /// <summary>
        /// testEquality_NullUnit() –
        /// Quantity(1.0, invalid unit) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenInvalidUnit_WhenCreatingWeight_ShouldThrowException()
        {
           Assert.Throws<ArgumentNullException>(() =>
                new QuantityWeight(1.0, null));
        }

        /// <summary>
        /// testEquality_TransitiveProperty() –
        /// If A equals B and B equals C, then A equals C.
        /// </summary>
        [TestMethod]
        public void GivenKilogramGramKilogram_WhenTransitiveCheck_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            QuantityWeight kgValue2 = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(kgValue, gValue);
            Assert.AreEqual(gValue, kgValue2);
            Assert.AreEqual(kgValue, kgValue2);
        }

        /// <summary>
        /// testEquality_ZeroValue() –
        /// Quantity(0.0, KILOGRAM).equals(Quantity(0.0, GRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenZeroKilogramAndZeroGram_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(0.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(0.0, WeightUnit.Gram);
            Assert.AreEqual(kgValue, gValue);
        }

        /// <summary>
        /// testEquality_NegativeWeight() –
        /// Quantity(-1.0, KILOGRAM).equals(Quantity(-1000.0, GRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenNegativeKilogramAndNegativeGram_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(-1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(-1000.0, WeightUnit.Gram);
            Assert.AreEqual(kgValue, gValue);
        }

        /// <summary>
        /// testEquality_LargeWeightValue() –
        /// Quantity(1000000.0, GRAM).equals(Quantity(1000.0, KILOGRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenLargeGramAndKilogram_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight gValue = new QuantityWeight(1000000.0, WeightUnit.Gram);
            QuantityWeight kgValue = new QuantityWeight(1000.0, WeightUnit.Kilogram);
            Assert.AreEqual(gValue, kgValue);
        }

        /// <summary>
        /// testEquality_SmallWeightValue() –
        /// Quantity(0.001, KILOGRAM).equals(Quantity(1.0, GRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenSmallKilogramAndOneGram_WhenCompared_ShouldReturnTrue()
        {
            QuantityWeight kgValue = new QuantityWeight(0.001, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1.0, WeightUnit.Gram);
            Assert.AreEqual(kgValue, gValue);
        }

        /// <summary>
        /// testConversion_PoundToKilogram() –
        /// Quantity(2.20462, POUND).convertTo(KILOGRAM) should return ~1.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenTwoPointTwoPounds_WhenConvertedToKilogram_ShouldReturnOne()
        {
            QuantityWeight poundValue = new QuantityWeight(2.20462, WeightUnit.Pound);
            QuantityWeight result = poundValue.ConvertTo(WeightUnit.Kilogram);
            QuantityWeight expected = new QuantityWeight(1.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_KilogramToPound() –
        /// Quantity(1.0, KILOGRAM).convertTo(POUND) should return ~2.20462 POUND.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogram_WhenConvertedToPound_ShouldReturnTwoPointTwo()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight result = kgValue.ConvertTo(WeightUnit.Pound);
            QuantityWeight expected = new QuantityWeight(2.20462, WeightUnit.Pound);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_SameUnit() –
        /// Quantity(5.0, KILOGRAM).convertTo(KILOGRAM) should return 5.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenFiveKilogram_WhenConvertedToSameUnit_ShouldReturnFive()
        {
            QuantityWeight kgValue = new QuantityWeight(5.0, WeightUnit.Kilogram);
            QuantityWeight result = kgValue.ConvertTo(WeightUnit.Kilogram);
            QuantityWeight expected = new QuantityWeight(5.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_ZeroValue() –
        /// Quantity(0.0, KILOGRAM).convertTo(GRAM) should return 0.0 GRAM.
        /// </summary>
        [TestMethod]
        public void GivenZeroKilogram_WhenConvertedToGram_ShouldReturnZero()
        {
            QuantityWeight kgValue = new QuantityWeight(0.0, WeightUnit.Kilogram);
            QuantityWeight result = kgValue.ConvertTo(WeightUnit.Gram);
            QuantityWeight expected = new QuantityWeight(0.0, WeightUnit.Gram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_NegativeValue() –
        /// Quantity(-1.0, KILOGRAM).convertTo(GRAM) should return -1000.0 GRAM.
        /// </summary>
        [TestMethod]
        public void GivenNegativeOneKilogram_WhenConvertedToGram_ShouldReturnNegativeThousand()
        {
            QuantityWeight kgValue = new QuantityWeight(-1.0, WeightUnit.Kilogram);
            QuantityWeight result = kgValue.ConvertTo(WeightUnit.Gram);
            QuantityWeight expected = new QuantityWeight(-1000.0, WeightUnit.Gram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_RoundTrip() –
        /// Quantity(1.5, KILOGRAM).convertTo(GRAM).convertTo(KILOGRAM) should return ~1.5.
        /// </summary>
        [TestMethod]
        public void GivenKilogramValue_WhenRoundTripConversion_ShouldReturnOriginalValue()
        {
            QuantityWeight kgValue = new QuantityWeight(1.5, WeightUnit.Kilogram);
            QuantityWeight converted = kgValue.ConvertTo(WeightUnit.Gram);
            QuantityWeight roundTrip = converted.ConvertTo(WeightUnit.Kilogram);
            Assert.AreEqual(kgValue, roundTrip);
        }

        /// <summary>
        /// testAddition_SameUnit_KilogramPlusKilogram() –
        /// Quantity(1.0, KILOGRAM).add(Quantity(2.0, KILOGRAM)) should return 3.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndTwoKilogram_WhenAdded_ShouldReturnThreeKilogram()
        {
            QuantityWeight first = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight second = new QuantityWeight(2.0, WeightUnit.Kilogram);
            QuantityWeight result = first.Add(second);
            QuantityWeight expected = new QuantityWeight(3.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_KilogramPlusGram() –
        /// Quantity(1.0, KILOGRAM).add(Quantity(1000.0, GRAM)) should return 2.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndThousandGrams_WhenAdded_ShouldReturnTwoKilogram()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            QuantityWeight result = kgValue.Add(gValue);
            QuantityWeight expected = new QuantityWeight(2.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_PoundPlusKilogram() –
        /// Quantity(2.20462, POUND).add(Quantity(1.0, KILOGRAM)) should return ~4.40924 POUND.
        /// </summary>
        [TestMethod]
        public void GivenPoundAndKilogram_WhenAdded_ShouldReturnCorrectPounds()
        {
            QuantityWeight poundValue = new QuantityWeight(2.20462, WeightUnit.Pound);
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight result = poundValue.Add(kgValue);
            QuantityWeight expected = new QuantityWeight(4.40924, WeightUnit.Pound);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Kilogram() –
        /// Quantity(1.0, KILOGRAM).add(Quantity(1000.0, GRAM), GRAM) should return 2000.0 GRAM.
        /// </summary>
        [TestMethod]
        public void GivenKilogramAndGram_WhenAddedWithGramTarget_ShouldReturnTwoThousandGrams()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            QuantityWeight result = kgValue.Add(gValue, WeightUnit.Gram);
            QuantityWeight expected = new QuantityWeight(2000.0, WeightUnit.Gram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_Commutativity() –
        /// Add(A, B) equals Add(B, A) in base unit.
        /// </summary>
        [TestMethod]
        public void GivenKilogramAndGram_WhenAddedBothWays_ShouldBeCommutative()
        {
            QuantityWeight kgValue = new QuantityWeight(1.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(1000.0, WeightUnit.Gram);
            QuantityWeight result1 = kgValue.Add(gValue, WeightUnit.Kilogram);
            QuantityWeight result2 = gValue.Add(kgValue, WeightUnit.Kilogram);
            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// testAddition_WithZero() –
        /// Quantity(5.0, KILOGRAM).add(Quantity(0.0, GRAM)) should return 5.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenFiveKilogramAndZeroGram_WhenAdded_ShouldReturnFiveKilogram()
        {
            QuantityWeight kgValue = new QuantityWeight(5.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(0.0, WeightUnit.Gram);
            QuantityWeight result = kgValue.Add(gValue);
            QuantityWeight expected = new QuantityWeight(5.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NegativeValues() –
        /// Quantity(5.0, KILOGRAM).add(Quantity(-2000.0, GRAM)) should return 3.0 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenFiveKilogramAndNegativeTwoThousandGrams_WhenAdded_ShouldReturnThreeKilogram()
        {
            QuantityWeight kgValue = new QuantityWeight(5.0, WeightUnit.Kilogram);
            QuantityWeight gValue = new QuantityWeight(-2000.0, WeightUnit.Gram);
            QuantityWeight result = kgValue.Add(gValue);
            QuantityWeight expected = new QuantityWeight(3.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_LargeValues() –
        /// Quantity(1e6, KILOGRAM).add(Quantity(1e6, KILOGRAM)) should return 2e6 KILOGRAM.
        /// </summary>
        [TestMethod]
        public void GivenLargeKilogramValues_WhenAdded_ShouldReturnCorrectSum()
        {
            QuantityWeight first = new QuantityWeight(1000000.0, WeightUnit.Kilogram);
            QuantityWeight second = new QuantityWeight(1000000.0, WeightUnit.Kilogram);
            QuantityWeight result = first.Add(second);
            QuantityWeight expected = new QuantityWeight(2000000.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }
    }
}