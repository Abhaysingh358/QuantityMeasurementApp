using QuantityMeasurementApp.Core.Models;
namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Test cases for Inch equality — UC2
    /// </summary>
    [TestClass]
    public class InchTest
    {
        // Test 1: Same value should be equal
        [TestMethod]
        public void GivenTwoInchValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            Inch firstValue = new Inch(1.0);
            Inch secondValue = new Inch(1.0);
            Assert.AreEqual(firstValue, secondValue);
        }

        // Test 2: Different values should not be equal
        [TestMethod]
        public void GivenTwoInchValues_WhenBothAreDifferent_ShouldReturnFalse()
        {
            Inch firstValue = new Inch(1.0);
            Inch secondValue = new Inch(2.0);
            Assert.IsFalse(firstValue.Equals(secondValue));
        }

        // Test 3: Null comparison should return false
        [TestMethod]
        public void GivenInchValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            Inch firstValue = new Inch(1.0);
            Assert.IsFalse(firstValue.Equals(null));
        }

        // Test 4: Non-numeric type should return false
        [TestMethod]
        public void GivenInchValue_WhenComparedWithDifferentType_ShouldReturnFalse()
        {
            Inch firstValue = new Inch(1.0);
            string nonNumeric = "1.0";
            Assert.AreNotEqual((object)firstValue, (object)nonNumeric);
        }

        // Test 5: Same reference should be equal (reflexive)
        [TestMethod]
        public void GivenInchValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            Inch firstValue = new Inch(1.0);
            Assert.AreEqual(firstValue, firstValue);
        }
    }
}