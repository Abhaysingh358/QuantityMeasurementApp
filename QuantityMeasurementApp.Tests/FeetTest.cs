using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests;

// Test cases for Feet equality — UC1

[TestClass]
public sealed class Test1
{
    // Test 1: Same value should be equal
    /* testEquality_SameValue() – 
    Verifies that two numerical values of 1.0 ft are considered equal.
    Tests: equals() returns true when comparing 1.0 ft with 1.0 ft. */

    /// <summary> 
    /// We just follow the 'GivenWhenThen' naming pattern instead of the Java-style names — 
    /// that's better practice in .NET.
    /// </summary>

    [TestMethod]
    public void GivenTwoFeetValues_WhenBothAreEqual_ShouldReturnTrue()
    {
        Feet firstValue = new Feet(1.0);
        Feet secondValue = new Feet(1.0);

        Assert.AreEqual(firstValue , secondValue);
    }

    // Test 2: Different values should not be equal
    /// <summary>
    /// estEquality_DifferentValue() – 
    /// Verifies that two numerical values of 1.0 ft and 2.0 ft are not equal.
    /// Tests: equals() returns false when comparing 1.0 ft with 2.0 ft.
    /// </summary>
   // Test 2: Different values should not be equal
    [TestMethod]
    public void GivenTwoFeetValues_WhenBothAreDifferent_ShouldReturnFalse()
    {
        Feet firstValue = new Feet(1.0);
        Feet secondValue = new Feet(2.0);
        Assert.IsFalse(firstValue.Equals(secondValue));
    }

    /// <summary> 
    /// testEquality_NullComparison() – 
    ///Verifies that a numerical value is not equal to null.
    ///Tests: equals() returns false when comparing a value with null.
    /// </summary>
    [TestMethod]
    public void GivenTwoFeetValue_WhenComparedWithNull_ShouldReturnFalse()
    {
        Feet firstValue = new Feet(1.0);
        Assert.AreNotEqual(firstValue , null);
    }

    
    /// <summary> 
    /// testEquality_NonNumericInput() – 
    /// Verifies that non-numeric inputs are handled appropriately.
    /// Tests: equals() returns false when comparing a numeric value with a non-numeric input.
    /// </summary>
    [TestMethod]
     public void GivenFeetValue_WhenComparedWithDifferentType_ShouldReturnFalse()
    {
        Feet firstValue = new Feet(1.0);
        string nonNumeric = "1.0";
        Assert.AreNotEqual((object)firstValue , (object)nonNumeric);
    }

    // Test 5: Same reference should be equal (reflexive)
    /// <summary> 
    /// testEquality_SameReference() – 
    /// Verifies that a numerical value is equal to itself (reflexive property).
    /// Tests: equals() returns true when comparing a value with itself.
    /// </summary>
    [TestMethod]
    public void GivenFeetValue_WhenCompareWithSameReference_ShouldReturnTrue()
    {
        Feet firstValue = new Feet(1.0);
        Assert.AreEqual(firstValue , firstValue);
    }

}
