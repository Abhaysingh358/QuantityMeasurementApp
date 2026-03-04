using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for Generic Quantity class — UC10
    /// </summary>
    [TestClass]
    public class GenericQuantityTest
    {
        /// <summary>
        /// testGenericQuantity_LengthOperations_Equality() –
        /// Quantity(1.0, FEET).equals(Quantity(12.0, INCHES)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenCompared_ShouldReturnTrue()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(feetValue, inchValue);
        }

        /// <summary>
        /// testGenericQuantity_WeightOperations_Equality() –
        /// Quantity(1.0, KILOGRAM).equals(Quantity(1000.0, GRAM)) should return true.
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndThousandGrams_WhenCompared_ShouldReturnTrue()
        {
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> gValue = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.AreEqual(kgValue, gValue);
        }

        /// <summary>
        /// testGenericQuantity_LengthOperations_Conversion() –
        /// Quantity(1.0, FEET).ConvertTo(INCHES) should return Quantity(12.0, INCHES).
        /// </summary>
        [TestMethod]
        public void GivenOneFeet_WhenConvertedToInches_ShouldReturnTwelve()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = feetValue.ConvertTo(LengthUnit.Inch);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testGenericQuantity_WeightOperations_Conversion() –
        /// Quantity(1.0, KILOGRAM).ConvertTo(GRAM) should return Quantity(1000.0, GRAM).
        /// </summary>
        [TestMethod]
        public void GivenOneKilogram_WhenConvertedToGram_ShouldReturnThousand()
        {
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> result = kgValue.ConvertTo(WeightUnit.Gram);
            Quantity<WeightUnit> expected = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testGenericQuantity_LengthOperations_Addition() –
        /// Quantity(1.0, FEET).Add(Quantity(12.0, INCHES), FEET) should return Quantity(2.0, FEET).
        /// </summary>
        [TestMethod]
        public void GivenOneFeetAndTwelveInches_WhenAdded_ShouldReturnTwoFeet()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inchValue = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = feetValue.Add(inchValue, LengthUnit.Feet);
            Quantity<LengthUnit> expected = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testGenericQuantity_WeightOperations_Addition() –
        /// Quantity(1.0, KILOGRAM).Add(Quantity(1000.0, GRAM), KILOGRAM) should return Quantity(2.0, KILOGRAM).
        /// </summary>
        [TestMethod]
        public void GivenOneKilogramAndThousandGrams_WhenAdded_ShouldReturnTwoKilogram()
        {
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> gValue = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Quantity<WeightUnit> result = kgValue.Add(gValue, WeightUnit.Kilogram);
            Quantity<WeightUnit> expected = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// testCrossCategoryPrevention_LengthVsWeight() –
        /// Quantity(1.0, FEET).equals(Quantity(1.0, KILOGRAM)) should return false.
        /// </summary>
        [TestMethod]
        public void GivenLengthAndWeight_WhenCompared_ShouldReturnFalse()
        {
            Quantity<LengthUnit> feetValue = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<WeightUnit> kgValue = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(feetValue.Equals(kgValue));
        }

        /// <summary>
        /// testGenericQuantity_ConstructorValidation_NullUnit() –
        /// Quantity(1.0, null) should throw ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullUnit_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Quantity<LengthUnit>(1.0, null));
        }

        /// <summary>
        /// testGenericQuantity_ConstructorValidation_InvalidValue() –
        /// Quantity(NaN, FEET) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenNaNValue_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));
        }

        /// <summary>
        /// testGenericQuantity_ConstructorValidation_InfiniteValue() –
        /// Quantity(Infinity, KILOGRAM) should throw ArgumentException.
        /// </summary>
        [TestMethod]
        public void GivenInfiniteValue_WhenCreatingQuantity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity<WeightUnit>(double.PositiveInfinity, WeightUnit.Kilogram));
        }

        /// <summary>
        /// testBackwardCompatibility_AllUC1Through9Tests() –
        /// All UC1-UC9 functionality preserved with generic design.
        /// </summary>
        [TestMethod]
        public void GivenAllUnitCombinations_WhenTestedWithGenericClass_ShouldWork()
        {
            // Length tests — UC1 to UC8
            Assert.AreEqual(
                new Quantity<LengthUnit>(1.0, LengthUnit.Yard),
                new Quantity<LengthUnit>(3.0, LengthUnit.Feet));
            Assert.AreEqual(
                new Quantity<LengthUnit>(36.0, LengthUnit.Inch),
                new Quantity<LengthUnit>(1.0, LengthUnit.Yard));
            Assert.AreEqual(
                new Quantity<LengthUnit>(1.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(12.0, LengthUnit.Inch));

            // Weight tests — UC9
            Assert.AreEqual(
                new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(1000.0, WeightUnit.Gram));
            Assert.AreEqual(
                new Quantity<WeightUnit>(0.001, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(1.0, WeightUnit.Gram));
        }

        /// <summary>
        /// testHashCode_GenericQuantity_Consistency() –
        /// Equal quantities have same hash code.
        /// </summary>
        [TestMethod]
        public void GivenEqualQuantities_WhenHashCodeCompared_ShouldBeEqual()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
        }

        /// <summary>
        /// testEquals_GenericQuantity_ContractPreservation() –
        /// Reflexive, symmetric, transitive properties hold.
        /// </summary>
        [TestMethod]
        public void GivenQuantities_WhenEqualityContractChecked_ShouldHoldAllProperties()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> c = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);

            // Reflexive
            Assert.AreEqual(a, a);
            // Symmetric
            Assert.AreEqual(a, b);
            Assert.AreEqual(b, a);
            // Transitive
            Assert.AreEqual(a, c);
        }

        /// <summary>
        /// testImmutability_GenericQuantity() –
        /// Operations return new instances — originals unchanged.
        /// </summary>
        [TestMethod]
        public void GivenQuantity_WhenOperationsPerformed_ShouldReturnNewInstances()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> converted = original.ConvertTo(LengthUnit.Inch);
            Quantity<LengthUnit> added = original.Add(
                new Quantity<LengthUnit>(1.0, LengthUnit.Feet));

            // Original unchanged
            Assert.AreEqual(new Quantity<LengthUnit>(1.0, LengthUnit.Feet), original);
            // New instances returned
            Assert.AreEqual(new Quantity<LengthUnit>(12.0, LengthUnit.Inch), converted);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), added);
        }

        /// <summary>
        /// testScalability_NewUnitEnumIntegration() –
        /// Pattern scales to new categories without changes.
        /// </summary>
        [TestMethod]
        public void GivenGenericDesign_WhenNewCategoryAdded_ShouldWorkSeamlessly()
        {
            Quantity<LengthUnit> length = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<WeightUnit> weight = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);

            // Both use same Quantity<T> class — no duplication
            Assert.IsNotNull(length);
            Assert.IsNotNull(weight);
            Assert.IsFalse(length.Equals(weight));
        }
    }
}