using QuantityMeasurementApp.Models.Enums;
using QuantityMeasurementApp.Models.Interfaces;
using QuantityMeasurementApp.Models.Exceptions;
using QuantityMeasurementApp.Business.Helpers;
using QuantityMeasurementApp.Business.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for volume measurement equality, conversion, and addition — UC11
    /// </summary>
    [TestClass]
    public class VolumeTest
    {
        private const double Epsilon = 0.0001;

        //  EQUALITY TESTS 

        /// <summary>
        /// testEquality_LitreToLitre_SameValue() –
        /// Quantity(1.0, LITRE).equals(Quantity(1.0, LITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenTwoLitreValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// testEquality_LitreToLitre_DifferentValue() –
        /// Quantity(1.0, LITRE).equals(Quantity(2.0, LITRE)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenTwoLitreValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.IsFalse(first.Equals(second));
        }

        /// <summary>
        /// testEquality_LitreToMillilitre_EquivalentValue() –
        /// Quantity(1.0, LITRE).equals(Quantity(1000.0, MILLILITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndThousandMillilitres_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(litreValue, mlValue);
        }

        /// <summary>
        /// testEquality_MillilitreToLitre_EquivalentValue() –
        /// Quantity(1000.0, MILLILITRE).equals(Quantity(1.0, LITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenThousandMillilitresAndOneLitre_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(mlValue, litreValue);
        }

        /// <summary>
        /// testEquality_LitreToGallon_EquivalentValue() –
        /// Quantity(1.0, LITRE).equals(Quantity(~0.264172, GALLON)) should return true (within epsilon).
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndEquivalentGallon_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> gallonValue = new Quantity<VolumeUnit>(0.264172, VolumeUnit.Gallon);
            Assert.AreEqual(litreValue, gallonValue);
        }

        /// <summary>
        /// testEquality_GallonToLitre_EquivalentValue() –
        /// Quantity(1.0, GALLON).equals(Quantity(3.78541, LITRE)) should return true (within epsilon).
        /// </summary>
        [TestMethod]
        public void GivenOneGallonAndEquivalentLitres_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> gallonValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.AreEqual(gallonValue, litreValue);
        }

        /// <summary>
        /// testEquality_VolumeVsLength_Incompatible() –
        /// Quantity(1.0, LITRE).equals(Quantity(1.0, FEET)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenVolumeAndLength_WhenCompared_ShouldReturnFalse()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Assert.IsFalse(litreValue.Equals(feetValue));
        }

        /// <summary>
        /// testEquality_VolumeVsWeight_Incompatible() –
        /// Quantity(1.0, LITRE).equals(Quantity(1.0, KILOGRAM)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenVolumeAndWeight_WhenCompared_ShouldReturnFalse()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(litreValue.Equals(kgValue));
        }

        /// <summary>
        /// testEquality_NullComparison() –
        /// Quantity(1.0, LITRE).equals(null) should return false.
        /// </summary>
        [TestMethod]
        public void GivenLitreValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.IsFalse(litreValue.Equals(null));
        }

        /// <summary>
        /// testEquality_SameReference() –
        /// A volume object equals itself (reflexive property).
        /// </summary>
        [TestMethod]
        public void GivenLitreValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(litreValue, litreValue);
        }

        /// <summary>
        /// testEquality_NullUnit() –
        /// Quantity(1.0, null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullUnit_WhenCreatingVolumeQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Quantity<VolumeUnit>(1.0, null));
        }

        /// <summary>
        /// testEquality_TransitiveProperty() –
        /// If A equals B and B equals C, then A equals C.
        /// </summary>
        [TestMethod]
        public void GivenLitreMillilitreLitre_WhenTransitiveCheck_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> a = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> b = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> c = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(a, b);
            Assert.AreEqual(b, c);
            Assert.AreEqual(a, c);
        }

        /// <summary>
        /// testEquality_ZeroValue() –
        /// Quantity(0.0, LITRE).equals(Quantity(0.0, MILLILITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenZeroLitreAndZeroMillilitre_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Assert.AreEqual(litreValue, mlValue);
        }

        /// <summary>
        /// testEquality_NegativeVolume() –
        /// Quantity(-1.0, LITRE).equals(Quantity(-1000.0, MILLILITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenNegativeLitreAndNegativeMillilitre_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(-1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(-1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(litreValue, mlValue);
        }

        /// <summary>
        /// testEquality_LargeVolumeValue() –
        /// Quantity(1000000.0, MILLILITRE).equals(Quantity(1000.0, LITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenLargeMillilitreAndEquivalentLitre_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Litre);
            Assert.AreEqual(mlValue, litreValue);
        }

        /// <summary>
        /// testEquality_SmallVolumeValue() –
        /// Quantity(0.001, LITRE).equals(Quantity(1.0, MILLILITRE)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenSmallLitreAndEquivalentMillilitre_WhenCompared_ShouldReturnTrue()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(0.001, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Millilitre);
            Assert.AreEqual(litreValue, mlValue);
        }

        //  VOLUME UNIT METHOD TESTS 

        /// <summary>
        /// testVolumeUnitEnum_LitreConstant() –
        /// VolumeUnit.Litre.ConvertToBaseUnit(1.0) should return 1.0.
        /// </summary>
        [TestMethod]
        public void GivenLitreUnit_WhenConvertToBaseUnit_ShouldReturnSameValue()
        {
            double result = VolumeUnit.Litre.ConvertToBaseUnit(1.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testVolumeUnitEnum_MillilitreConstant() –
        /// VolumeUnit.Millilitre.ConvertToBaseUnit(1.0) should return 0.001.
        /// </summary>
        [TestMethod]
        public void GivenMillilitreUnit_WhenConvertToBaseUnit_ShouldReturnCorrectFactor()
        {
            double result = VolumeUnit.Millilitre.ConvertToBaseUnit(1.0);
            Assert.AreEqual(0.001, result, Epsilon);
        }

        /// <summary>
        /// testVolumeUnitEnum_GallonConstant() –
        /// VolumeUnit.Gallon.ConvertToBaseUnit(1.0) should return 3.78541.
        /// </summary>
        [TestMethod]
        public void GivenGallonUnit_WhenConvertToBaseUnit_ShouldReturnThreePointSevenEight()
        {
            double result = VolumeUnit.Gallon.ConvertToBaseUnit(1.0);
            Assert.AreEqual(3.78541, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_LitreToLitre() –
        /// VolumeUnit.Litre.ConvertToBaseUnit(5.0) should return 5.0.
        /// </summary>
        [TestMethod]
        public void GivenFiveLitres_WhenConvertToBaseUnit_ShouldReturnFive()
        {
            double result = VolumeUnit.Litre.ConvertToBaseUnit(5.0);
            Assert.AreEqual(5.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_MillilitreToLitre() –
        /// VolumeUnit.Millilitre.ConvertToBaseUnit(1000.0) should return 1.0.
        /// </summary>
        [TestMethod]
        public void GivenThousandMillilitres_WhenConvertToBaseUnit_ShouldReturnOneLitre()
        {
            double result = VolumeUnit.Millilitre.ConvertToBaseUnit(1000.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertToBaseUnit_GallonToLitre() –
        /// VolumeUnit.Gallon.ConvertToBaseUnit(1.0) should return 3.78541.
        /// </summary>
        [TestMethod]
        public void GivenOneGallon_WhenConvertToBaseUnit_ShouldReturnThreePointSevenEightLitres()
        {
            double result = VolumeUnit.Gallon.ConvertToBaseUnit(1.0);
            Assert.AreEqual(3.78541, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_LitreToLitre() –
        /// VolumeUnit.Litre.ConvertFromBaseUnit(2.0) should return 2.0.
        /// </summary>
        [TestMethod]
        public void GivenTwoLitreBase_WhenConvertFromBaseUnit_ShouldReturnTwo()
        {
            double result = VolumeUnit.Litre.ConvertFromBaseUnit(2.0);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_LitreToMillilitre() –
        /// VolumeUnit.Millilitre.ConvertFromBaseUnit(1.0) should return 1000.0.
        /// </summary>
        [TestMethod]
        public void GivenOneLitreBase_WhenConvertFromBaseUnitToMillilitre_ShouldReturnThousand()
        {
            double result = VolumeUnit.Millilitre.ConvertFromBaseUnit(1.0);
            Assert.AreEqual(1000.0, result, Epsilon);
        }

        /// <summary>
        /// testConvertFromBaseUnit_LitreToGallon() –
        /// VolumeUnit.Gallon.ConvertFromBaseUnit(3.78541) should return ~1.0.
        /// </summary>
        [TestMethod]
        public void GivenThreePointSevenEightLitreBase_WhenConvertFromBaseUnitToGallon_ShouldReturnOne()
        {
            double result = VolumeUnit.Gallon.ConvertFromBaseUnit(3.78541);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        //  CONVERSION TESTS 

        /// <summary>
        /// testConversion_LitreToMillilitre() –
        /// Quantity(1.0, LITRE).ConvertTo(MILLILITRE) should return Quantity(1000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneLitre_WhenConvertedToMillilitre_ShouldReturnThousand()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = litreValue.ConvertTo(VolumeUnit.Millilitre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_MillilitreToLitre() –
        /// Quantity(1000.0, MILLILITRE).ConvertTo(LITRE) should return Quantity(1.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenThousandMillilitres_WhenConvertedToLitre_ShouldReturnOne()
        {
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = mlValue.ConvertTo(VolumeUnit.Litre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_GallonToLitre() –
        /// Quantity(1.0, GALLON).ConvertTo(LITRE) should return Quantity(3.78541, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneGallon_WhenConvertedToLitre_ShouldReturnThreePointSevenEight()
        {
            Quantity<VolumeUnit> gallonValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Quantity<VolumeUnit> result = gallonValue.ConvertTo(VolumeUnit.Litre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_LitreToGallon() –
        /// Quantity(3.78541, LITRE).ConvertTo(GALLON) should return Quantity(~1.0, GALLON).
        /// </summary>
        [TestMethod]
        public void GivenThreePointSevenEightLitres_WhenConvertedToGallon_ShouldReturnOne()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = litreValue.ConvertTo(VolumeUnit.Gallon);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_MillilitreToGallon() –
        /// Quantity(1000.0, MILLILITRE).ConvertTo(GALLON) should return Quantity(~0.264172, GALLON).
        /// </summary>
        [TestMethod]
        public void GivenThousandMillilitres_WhenConvertedToGallon_ShouldReturnCorrectValue()
        {
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = mlValue.ConvertTo(VolumeUnit.Gallon);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(0.26417, VolumeUnit.Gallon);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_SameUnit() –
        /// Quantity(5.0, LITRE).ConvertTo(LITRE) should return Quantity(5.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenFiveLitres_WhenConvertedToSameUnit_ShouldReturnFive()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = litreValue.ConvertTo(VolumeUnit.Litre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_ZeroValue() –
        /// Quantity(0.0, LITRE).ConvertTo(MILLILITRE) should return Quantity(0.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenZeroLitres_WhenConvertedToMillilitre_ShouldReturnZero()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(0.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = litreValue.ConvertTo(VolumeUnit.Millilitre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_NegativeValue() –
        /// Quantity(-1.0, LITRE).ConvertTo(MILLILITRE) should return Quantity(-1000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenNegativeOneLitre_WhenConvertedToMillilitre_ShouldReturnNegativeThousand()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(-1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = litreValue.ConvertTo(VolumeUnit.Millilitre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(-1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testConversion_RoundTrip() –
        /// Quantity(1.5, LITRE).ConvertTo(MILLILITRE).ConvertTo(LITRE) should return ~1.5 LITRE.
        /// </summary>
        [TestMethod]
        public void GivenLitreValue_WhenRoundTripConversion_ShouldReturnOriginalValue()
        {
            Quantity<VolumeUnit> original = new Quantity<VolumeUnit>(1.5, VolumeUnit.Litre);
            Quantity<VolumeUnit> converted = original.ConvertTo(VolumeUnit.Millilitre);
            Quantity<VolumeUnit> roundTrip = converted.ConvertTo(VolumeUnit.Litre);
            Assert.AreEqual(original, roundTrip);
        }

        // ADDITION TESTS 

        /// <summary>
        /// testAddition_SameUnit_LitrePlusLitre() –
        /// Quantity(1.0, LITRE).Add(Quantity(2.0, LITRE)) should return Quantity(3.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndTwoLitres_WhenAdded_ShouldReturnThreeLitres()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_SameUnit_MillilitrePlusMillilitre() –
        /// Quantity(500.0, MILLILITRE).Add(Quantity(500.0, MILLILITRE)) should return Quantity(1000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenFiveHundredMlAndFiveHundredMl_WhenAdded_ShouldReturnThousandMl()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = first.Add(second);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_LitrePlusMillilitre() –
        /// Quantity(1.0, LITRE).Add(Quantity(1000.0, MILLILITRE)) should return Quantity(2.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndThousandMl_WhenAdded_ShouldReturnTwoLitres()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = litreValue.Add(mlValue);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_MillilitrePlusLitre() –
        /// Quantity(1000.0, MILLILITRE).Add(Quantity(1.0, LITRE)) should return Quantity(2000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenThousandMlAndOneLitre_WhenAdded_ShouldReturnTwoThousandMl()
        {
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = mlValue.Add(litreValue);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_CrossUnit_GallonPlusLitre() –
        /// Quantity(1.0, GALLON).Add(Quantity(3.78541, LITRE)) should return Quantity(~2.0, GALLON).
        /// </summary>
        [TestMethod]
        public void GivenOneGallonAndEquivalentLitres_WhenAdded_ShouldReturnTwoGallons()
        {
            Quantity<VolumeUnit> gallonValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = gallonValue.Add(litreValue);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2.0, VolumeUnit.Gallon);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Litre() –
        /// Quantity(1.0, LITRE).Add(Quantity(1000.0, MILLILITRE), LITRE) should return Quantity(2.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndThousandMl_WhenAddedWithLitreTarget_ShouldReturnTwoLitres()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = litreValue.Add(mlValue, VolumeUnit.Litre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Millilitre() –
        /// Quantity(1.0, LITRE).Add(Quantity(1000.0, MILLILITRE), MILLILITRE) should return Quantity(2000.0, MILLILITRE).
        /// </summary>
        [TestMethod]
        public void GivenOneLitreAndThousandMl_WhenAddedWithMlTarget_ShouldReturnTwoThousandMl()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = litreValue.Add(mlValue, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_ExplicitTargetUnit_Gallon() –
        /// Quantity(3.78541, LITRE).Add(Quantity(3.78541, LITRE), GALLON) should return Quantity(~2.0, GALLON).
        /// </summary>
        [TestMethod]
        public void GivenTwoEquivalentLitres_WhenAddedWithGallonTarget_ShouldReturnTwoGallons()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second, VolumeUnit.Gallon);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2.0, VolumeUnit.Gallon);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_Commutativity() –
        /// Add(A, B, target) equals Add(B, A, target).
        /// </summary>
        [TestMethod]
        public void GivenLitreAndMl_WhenAddedBothWays_ShouldBeCommutative()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result1 = litreValue.Add(mlValue, VolumeUnit.Litre);
            Quantity<VolumeUnit> result2 = mlValue.Add(litreValue, VolumeUnit.Litre);
            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// testAddition_WithZero() –
        /// Quantity(5.0, LITRE).Add(Quantity(0.0, MILLILITRE)) should return Quantity(5.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenFiveLitresAndZeroMl_WhenAdded_ShouldReturnFiveLitres()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = litreValue.Add(mlValue);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_NegativeValues() –
        /// Quantity(5.0, LITRE).Add(Quantity(-2000.0, MILLILITRE)) should return Quantity(3.0, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenFiveLitresAndNegativeTwoThousandMl_WhenAdded_ShouldReturnThreeLitres()
        {
            Quantity<VolumeUnit> litreValue = new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> mlValue = new Quantity<VolumeUnit>(-2000.0, VolumeUnit.Millilitre);
            Quantity<VolumeUnit> result = litreValue.Add(mlValue);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_LargeValues() –
        /// Quantity(1e6, LITRE).Add(Quantity(1e6, LITRE)) should return Quantity(2e6, LITRE).
        /// </summary>
        [TestMethod]
        public void GivenLargeLitreValues_WhenAdded_ShouldReturnCorrectSum()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(1000000.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(2000000.0, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testAddition_SmallValues() –
        /// Quantity(0.001, LITRE).Add(Quantity(0.002, LITRE)) should return ~0.003 LITRE.
        /// </summary>
        [TestMethod]
        public void GivenSmallLitreValues_WhenAdded_ShouldReturnCorrectSum()
        {
            Quantity<VolumeUnit> first = new Quantity<VolumeUnit>(0.001, VolumeUnit.Litre);
            Quantity<VolumeUnit> second = new Quantity<VolumeUnit>(0.002, VolumeUnit.Litre);
            Quantity<VolumeUnit> result = first.Add(second);
            Quantity<VolumeUnit> expected = new Quantity<VolumeUnit>(0.003, VolumeUnit.Litre);
            Assert.AreEqual(expected, result);
        }

        // CONSTRUCTOR VALIDATION TESTS 

        /// <summary>
        /// Quantity(NaN, LITRE) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenNaNValue_WhenCreatingVolumeQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<VolumeUnit>(double.NaN, VolumeUnit.Litre));
        }

        /// <summary>
        /// Quantity(Infinity, LITRE) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenInfiniteValue_WhenCreatingVolumeQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<VolumeUnit>(double.PositiveInfinity, VolumeUnit.Litre));
        }

        // BACKWARD COMPATIBILITY & SCALABILITY TESTS 

        /// <summary>
        /// testBackwardCompatibility_AllUC1Through10Tests() –
        /// All UC1-UC10 functionality preserved with volume additions.
        /// </summary>
        [TestMethod]
        public void GivenAllPreviousUCFunctionality_WhenTestedAfterUC11_ShouldStillWork()
        {
            // Length tests — UC1 to UC8
            Assert.AreEqual(
                new Quantity<LengthUnit>(1.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(12.0, LengthUnit.Inch));
            Assert.AreEqual(
                new Quantity<LengthUnit>(1.0, LengthUnit.Yard),
                new Quantity<LengthUnit>(3.0, LengthUnit.Feet));
            Assert.AreEqual(
                new Quantity<LengthUnit>(36.0, LengthUnit.Inch),
                new Quantity<LengthUnit>(1.0, LengthUnit.Yard));

            // Weight tests — UC9
            Assert.AreEqual(
                new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(1000.0, WeightUnit.Gram));
            Assert.AreEqual(
                new Quantity<WeightUnit>(0.001, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(1.0, WeightUnit.Gram));
        }

        /// <summary>
        /// testGenericQuantity_VolumeOperations_Consistency() –
        /// Generic Quantity works identically for volume as for length and weight.
        /// </summary>
        [TestMethod]
        public void GivenGenericQuantity_WhenUsedWithVolumeUnit_ShouldWorkSeamlessly()
        {
            Quantity<VolumeUnit> volume = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<LengthUnit> length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<WeightUnit> weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);

            Assert.IsNotNull(volume);
            Assert.IsFalse(volume.Equals(length));
            Assert.IsFalse(volume.Equals(weight));
        }

        /// <summary>
        /// testScalability_VolumeIntegration() –
        /// Volume integrates seamlessly — no changes to existing classes needed.
        /// </summary>
        [TestMethod]
        public void GivenVolumeUnit_WhenTestedWithAllOperations_ShouldWorkWithoutModifications()
        {
            // Equality
            Assert.AreEqual(
                new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre));

            // Conversion
            Assert.AreEqual(
                new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre),
                new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre));

            // Addition
            Assert.AreEqual(
                new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre)
                    .Add(new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre), VolumeUnit.Litre));
        }

        /// <summary>
        /// testHashCode_VolumeQuantity_Consistency() –
        /// Equal volume quantities have same hash code.
        /// </summary>
        [TestMethod]
        public void GivenEqualVolumeQuantities_WhenHashCodeCompared_ShouldBeEqual()
        {
            Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
            Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
        }
    }
}