using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Interfaces;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Test
{
    [TestClass]
    public class TemperatureTest
    {
        private const double Epsilon = 0.0001;

        /// <summary>
        /// testTemperatureEquality_CelsiusToCelsius_SameValue()
        /// Verifies new Quantity(0.0, CELSIUS).equals(new Quantity(0.0, CELSIUS)) returns true.
        /// </summary>
        [TestMethod]
        public void GivenTwoCelsiusValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// testTemperatureEquality_FahrenheitToFahrenheit_SameValue()
        /// Verifies new Quantity(32.0, FAHRENHEIT).equals(new Quantity(32.0, FAHRENHEIT)) returns true.
        /// </summary>
        [TestMethod]
        public void GivenTwoFahrenheitValues_WhenBothAreEqual_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// testTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
        /// Verifies new Quantity(0.0, CELSIUS).equals(new Quantity(32.0, FAHRENHEIT)) returns true.
        /// </summary>
        [TestMethod]
        public void GivenZeroCelsiusAnd32Fahrenheit_WhenCompared_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(celsius, fahrenheit);
        }

        /// <summary>
        /// testTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
        /// Verifies new Quantity(100.0, CELSIUS).equals(new Quantity(212.0, FAHRENHEIT)) returns true.
        /// </summary>
        [TestMethod]
        public void Given100CelsiusAnd212Fahrenheit_WhenCompared_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(celsius, fahrenheit);
        }

        /// <summary>
        /// testTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
        /// Verifies new Quantity(-40.0, CELSIUS).equals(new Quantity(-40.0, FAHRENHEIT)) returns true.
        /// </summary>
        [TestMethod]
        public void GivenNegative40CelsiusAndNegative40Fahrenheit_WhenCompared_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(celsius, fahrenheit);
        }

        /// <summary>
        /// testTemperatureEquality_SymmetricProperty()
        /// Verifies symmetric property: if A equals B, then B equals A.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusAndFahrenheit_WhenSymmetryChecked_ShouldHoldBothWays()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(celsius, fahrenheit);
            Assert.AreEqual(fahrenheit, celsius);
        }

        /// <summary>
        /// testTemperatureEquality_ReflexiveProperty()
        /// Verifies temperature equals itself.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValue_WhenComparedWithSelf_ShouldReturnTrue()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Assert.AreEqual(celsius, celsius);
        }

        /// <summary>
        /// testTemperatureConversion_CelsiusToFahrenheit_VariousValues()
        /// Test multiple values: 50°C to 122°F, -20°C to -4°F, etc.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValues_WhenConvertedToFahrenheit_ShouldReturnCorrectResult()
        {
            Assert.AreEqual(
                new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit),
                new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius).ConvertTo(TemperatureUnit.Fahrenheit));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(122.0, TemperatureUnit.Fahrenheit),
                new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius).ConvertTo(TemperatureUnit.Fahrenheit));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(-4.0, TemperatureUnit.Fahrenheit),
                new Quantity<TemperatureUnit>(-20.0, TemperatureUnit.Celsius).ConvertTo(TemperatureUnit.Fahrenheit));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit),
                new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius).ConvertTo(TemperatureUnit.Fahrenheit));
        }

        /// <summary>
        /// testTemperatureConversion_FahrenheitToCelsius_VariousValues()
        /// Test reverse conversions with the same values.
        /// </summary>
        [TestMethod]
        public void GivenFahrenheitValues_WhenConvertedToCelsius_ShouldReturnCorrectResult()
        {
            Assert.AreEqual(
                new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius),
                new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit).ConvertTo(TemperatureUnit.Celsius));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius),
                new Quantity<TemperatureUnit>(122.0, TemperatureUnit.Fahrenheit).ConvertTo(TemperatureUnit.Celsius));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(-20.0, TemperatureUnit.Celsius),
                new Quantity<TemperatureUnit>(-4.0, TemperatureUnit.Fahrenheit).ConvertTo(TemperatureUnit.Celsius));

            Assert.AreEqual(
                new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius),
                new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit).ConvertTo(TemperatureUnit.Celsius));
        }

        /// <summary>
        /// testTemperatureConversion_RoundTrip_PreservesValue()
        /// Verify convert(convert(value, A, B), B, A) approximates value.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValue_WhenRoundTripConverted_ShouldPreserveOriginalValue()
        {
            Quantity<TemperatureUnit> original = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> roundTrip = original
                .ConvertTo(TemperatureUnit.Fahrenheit)
                .ConvertTo(TemperatureUnit.Celsius);
            Assert.AreEqual(original, roundTrip);
        }

        /// <summary>
        /// testTemperatureConversion_SameUnit()
        /// Verify converting to same unit returns unchanged value.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValue_WhenConvertedToCelsius_ShouldReturnSameValue()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Assert.AreEqual(celsius, celsius.ConvertTo(TemperatureUnit.Celsius));
        }

        /// <summary>
        /// testTemperatureConversion_ZeroValue()
        /// Verify conversion of zero works (0°C to °F = 32°F).
        /// </summary>
        [TestMethod]
        public void GivenZeroCelsius_WhenConvertedToFahrenheit_ShouldReturn32()
        {
            Quantity<TemperatureUnit> result = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius)
                .ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.AreEqual(new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit), result);
        }

        /// <summary>
        /// testTemperatureConversion_NegativeValues()
        /// Test negative temperature conversions.
        /// </summary>
        [TestMethod]
        public void GivenNegativeCelsius_WhenConverted_ShouldReturnCorrectFahrenheit()
        {
            Quantity<TemperatureUnit> result = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Celsius)
                .ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.AreEqual(new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.Fahrenheit), result);
        }

        /// <summary>
        /// testTemperatureConversion_LargeValues()
        /// Test very high temperatures (e.g., 1000°C).
        /// </summary>
        [TestMethod]
        public void GivenLargeCelsiusValue_WhenConverted_ShouldReturnCorrectFahrenheit()
        {
            Quantity<TemperatureUnit> result = new Quantity<TemperatureUnit>(1000.0, TemperatureUnit.Celsius)
                .ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.AreEqual(new Quantity<TemperatureUnit>(1832.0, TemperatureUnit.Fahrenheit), result);
        }

        /// <summary>
        /// testTemperatureUnsupportedOperation_Add()
        /// Verifies new Quantity(100.0, CELSIUS).add(new Quantity(50.0, CELSIUS)) throws InvalidOperationException.
        /// Tests error message clarity.
        /// </summary>
        [TestMethod]
        public void GivenTwoCelsiusValues_WhenAdded_ShouldThrowInvalidOperationException()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Assert.Throws<InvalidOperationException>(() => first.Add(second));
        }

        /// <summary>
        /// testTemperatureUnsupportedOperation_Subtract()
        /// Verifies subtract() throws InvalidOperationException.
        /// </summary>
        [TestMethod]
        public void GivenTwoCelsiusValues_WhenSubtracted_ShouldThrowInvalidOperationException()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Assert.Throws<InvalidOperationException>(() => first.Subtract(second));
        }

        /// <summary>
        /// testTemperatureUnsupportedOperation_Divide()
        /// Verifies divide() throws InvalidOperationException.
        /// </summary>
        [TestMethod]
        public void GivenTwoCelsiusValues_WhenDivided_ShouldThrowInvalidOperationException()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Assert.Throws<InvalidOperationException>(() => first.Divide(second));
        }

        /// <summary>
        /// testTemperatureUnsupportedOperation_ErrorMessage()
        /// Verifies error message explains why operation unsupported.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValues_WhenArithmeticAttempted_ShouldThrowWithClearMessage()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => first.Add(second));
            Assert.IsTrue(ex.Message.Contains("Temperature"));
        }

        /// <summary>
        /// testTemperatureVsLengthIncompatibility()
        /// Verifies new Quantity(100.0, CELSIUS).equals(new Quantity(100.0, FEET)) returns false.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureAndLength_WhenCompared_ShouldReturnFalse()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<LengthUnit> feet = new Quantity<LengthUnit>(100.0, LengthUnit.Feet);
            Assert.IsFalse(celsius.Equals(feet));
        }

        /// <summary>
        /// testTemperatureVsWeightIncompatibility()
        /// Verifies new Quantity(50.0, CELSIUS).equals(new Quantity(50.0, KILOGRAM)) returns false.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureAndWeight_WhenCompared_ShouldReturnFalse()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Quantity<WeightUnit> kg = new Quantity<WeightUnit>(50.0, WeightUnit.Kilogram);
            Assert.IsFalse(celsius.Equals(kg));
        }

        /// <summary>
        /// testTemperatureVsVolumeIncompatibility()
        /// Verifies new Quantity(25.0, CELSIUS).equals(new Quantity(25.0, LITRE)) returns false.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureAndVolume_WhenCompared_ShouldReturnFalse()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.Celsius);
            Quantity<VolumeUnit> litre = new Quantity<VolumeUnit>(25.0, VolumeUnit.Litre);
            Assert.IsFalse(celsius.Equals(litre));
        }

        /// <summary>
        /// testOperationSupportMethods_TemperatureUnitAddition()
        /// Verifies TemperatureUnit.CELSIUS.SupportsArithmetic() returns false.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusUnit_WhenSupportsArithmeticChecked_ShouldReturnFalse()
        {
            Assert.IsFalse(TemperatureUnit.Celsius.SupportsArithmetic());
        }

        /// <summary>
        /// testOperationSupportMethods_TemperatureUnitDivision()
        /// Verifies TemperatureUnit.FAHRENHEIT.SupportsArithmetic() returns false.
        /// </summary>
        [TestMethod]
        public void GivenFahrenheitUnit_WhenSupportsArithmeticChecked_ShouldReturnFalse()
        {
            Assert.IsFalse(TemperatureUnit.Fahrenheit.SupportsArithmetic());
        }

        /// <summary>
        /// testOperationSupportMethods_LengthUnitAddition()
        /// Verifies LengthUnit.FEET.SupportsArithmetic() returns true (inherited default).
        /// </summary>
        [TestMethod]
        public void GivenLengthUnit_WhenSupportsArithmeticChecked_ShouldReturnTrue()
        {
            IMeasurable lengthUnit = LengthUnit.Feet;
            Assert.IsTrue(lengthUnit.SupportsArithmetic());
        }

        /// <summary>
        /// testOperationSupportMethods_WeightUnitDivision()
        /// Verifies WeightUnit.KILOGRAM.SupportsArithmetic() returns true.
        /// </summary>
        [TestMethod]
        public void GivenWeightUnit_WhenSupportsArithmeticChecked_ShouldReturnTrue()
        {
            IMeasurable weightUnit = WeightUnit.Kilogram;
            Assert.IsTrue(weightUnit.SupportsArithmetic());
        }

        /// <summary>
        /// testIMeasurableInterface_Evolution_BackwardCompatible()
        /// Verify existing units work without modification. No breaking changes to interface.
        /// </summary>
        [TestMethod]
        public void GivenExistingUnits_WhenUsedAfterIMeasurableRefactor_ShouldWorkUnchanged()
        {
            Assert.AreEqual(
                new Quantity<LengthUnit>(2.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(1.0, LengthUnit.Feet).Add(new Quantity<LengthUnit>(12.0, LengthUnit.Inch)));

            Assert.AreEqual(
                new Quantity<WeightUnit>(15.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Add(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)));

            Assert.AreEqual(
                new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre).Add(new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre)));
        }

        /// <summary>
        /// testTemperatureUnit_NonLinearConversion()
        /// Verify that temperature conversions use formulas, not simple multiplication.
        /// Test complex conversion logic.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenConversionChecked_ShouldUseFormulaNotFactor()
        {
            double celsiusBase = TemperatureUnit.Celsius.ConvertToBaseUnit(100.0);
            double fahrenheitResult = TemperatureUnit.Fahrenheit.ConvertFromBaseUnit(celsiusBase);
            Assert.AreEqual(212.0, fahrenheitResult, Epsilon);

            // 0 * any factor = 0, but formula gives 32 — proves non-linear formula is used
            double zeroBase = TemperatureUnit.Celsius.ConvertToBaseUnit(0.0);
            double zeroFahrenheit = TemperatureUnit.Fahrenheit.ConvertFromBaseUnit(zeroBase);
            Assert.AreEqual(32.0, zeroFahrenheit, Epsilon);
        }

        /// <summary>
        /// testTemperatureUnit_AllConstants()
        /// Verify CELSIUS and FAHRENHEIT are accessible. Test enum structure.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenConstantsAccessed_ShouldNotBeNull()
        {
            Assert.IsNotNull(TemperatureUnit.Celsius);
            Assert.IsNotNull(TemperatureUnit.Fahrenheit);
            Assert.IsNotNull(TemperatureUnit.Kelvin);
        }

        /// <summary>
        /// testTemperatureUnit_NameMethod()
        /// Verify GetUnitName() returns correct names.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenGetUnitNameCalled_ShouldReturnCorrectName()
        {
            Assert.AreEqual("Celsius", TemperatureUnit.Celsius.GetUnitName());
            Assert.AreEqual("Fahrenheit", TemperatureUnit.Fahrenheit.GetUnitName());
            Assert.AreEqual("Kelvin", TemperatureUnit.Kelvin.GetUnitName());
        }

        /// <summary>
        /// testTemperatureUnit_ConversionFactor()
        /// Verify GetConversionFactor() returns 1.0 — temperature uses formulas, not a simple factor.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenConversionFactorChecked_ShouldReturn1()
        {
            Assert.AreEqual(1.0, TemperatureUnit.Celsius.GetConversionFactor(), Epsilon);
            Assert.AreEqual(1.0, TemperatureUnit.Fahrenheit.GetConversionFactor(), Epsilon);
            Assert.AreEqual(1.0, TemperatureUnit.Kelvin.GetConversionFactor(), Epsilon);
        }

        /// <summary>
        /// testTemperatureNullUnitValidation()
        /// Verify new Quantity(100.0, null) throws ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void GivenNullTemperatureUnit_WhenConstructed_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Quantity<TemperatureUnit>(100.0, null));
        }

        /// <summary>
        /// testTemperatureNullOperandValidation_InComparison()
        /// Verify equals(null) returns false.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Assert.IsFalse(celsius.Equals(null));
        }

        /// <summary>
        /// testTemperatureDifferentValuesInequality()
        /// Verify 50°C does not equal 100°C.
        /// </summary>
        [TestMethod]
        public void GivenTwoDifferentCelsiusValues_WhenCompared_ShouldReturnFalse()
        {
            Quantity<TemperatureUnit> first = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> second = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Assert.IsFalse(first.Equals(second));
        }

        /// <summary>
        /// testTemperatureBackwardCompatibility_UC1_Through_UC13()
        /// Run all existing test cases; verify temperature additions don't break other categories.
        /// </summary>
        [TestMethod]
        public void GivenAllPreviousCategories_WhenTestedAfterUC14_ShouldPassUnchanged()
        {
            Assert.AreEqual(5.0,
                new Quantity<LengthUnit>(10.0, LengthUnit.Feet).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)), Epsilon);

            Assert.AreEqual(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Subtract(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)));

            Assert.AreEqual(new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre).Subtract(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
        }

        /// <summary>
        /// testTemperatureConversionPrecision_Epsilon()
        /// Verify epsilon-based tolerance in conversion equality.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusValue_WhenConvertedToFahrenheit_ShouldBeWithinEpsilon()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(122.0, TemperatureUnit.Fahrenheit);
            Assert.AreEqual(celsius, fahrenheit);
        }

        /// <summary>
        /// testTemperatureConversionEdgeCase_VerySmallDifference()
        /// Test precision with very close temperatures.
        /// </summary>
        [TestMethod]
        public void GivenVeryCloseTemperatures_WhenCompared_ShouldRespectEpsilonTolerance()
        {
            Quantity<TemperatureUnit> a = new Quantity<TemperatureUnit>(100.00001, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> b = new Quantity<TemperatureUnit>(100.00001, TemperatureUnit.Celsius);
            Assert.AreEqual(a, b);
        }

        /// <summary>
        /// testTemperatureEnumImplementsIMeasurable()
        /// Verify TemperatureUnit implements IMeasurable interface correctly.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenCheckedForIMeasurable_ShouldImplementInterface()
        {
            Assert.IsInstanceOfType(TemperatureUnit.Celsius, typeof(IMeasurable));
            Assert.IsInstanceOfType(TemperatureUnit.Fahrenheit, typeof(IMeasurable));
            Assert.IsInstanceOfType(TemperatureUnit.Kelvin, typeof(IMeasurable));
        }

        /// <summary>
        /// testTemperatureDefaultMethodInheritance()
        /// Verify non-temperature units inherit default true values for operation support.
        /// </summary>
        [TestMethod]
        public void GivenExistingUnits_WhenSupportsArithmeticChecked_ShouldInheritDefaultTrue()
        {
            Assert.IsTrue(((IMeasurable)LengthUnit.Feet).SupportsArithmetic());
            Assert.IsTrue(((IMeasurable)LengthUnit.Inch).SupportsArithmetic());
            Assert.IsTrue(((IMeasurable)WeightUnit.Kilogram).SupportsArithmetic());
            Assert.IsTrue(((IMeasurable)WeightUnit.Gram).SupportsArithmetic());
            Assert.IsTrue(((IMeasurable)VolumeUnit.Litre).SupportsArithmetic());
            Assert.IsTrue(((IMeasurable)VolumeUnit.Millilitre).SupportsArithmetic());
        }

        /// <summary>
        /// testTemperatureCrossUnitAdditionAttempt()
        /// Verify adding different temperature units throws exception.
        /// </summary>
        [TestMethod]
        public void GivenCelsiusAndFahrenheit_WhenAdded_ShouldThrowInvalidOperationException()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.Fahrenheit);
            Assert.Throws<InvalidOperationException>(() => celsius.Add(fahrenheit));
        }

        /// <summary>
        /// testTemperatureValidateOperationSupport_MethodBehavior()
        /// Direct test: TemperatureUnit.CELSIUS.ValidateOperationSupport("addition") throws an exception.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenValidateOperationSupportCalled_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                TemperatureUnit.Celsius.ValidateOperationSupport("Add"));
            Assert.Throws<InvalidOperationException>(() =>
                TemperatureUnit.Fahrenheit.ValidateOperationSupport("Subtract"));
            Assert.Throws<InvalidOperationException>(() =>
                TemperatureUnit.Kelvin.ValidateOperationSupport("Divide"));
        }

        /// <summary>
        /// testTemperatureIntegrationWithGenericQuantity()
        /// Verify Quantity&lt;TemperatureUnit&gt; works seamlessly with generic class.
        /// </summary>
        [TestMethod]
        public void GivenTemperatureUnit_WhenUsedWithGenericQuantity_ShouldWorkSeamlessly()
        {
            Quantity<TemperatureUnit> celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.Celsius);
            Quantity<TemperatureUnit> fahrenheit = celsius.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.AreEqual(new Quantity<TemperatureUnit>(212.0, TemperatureUnit.Fahrenheit), fahrenheit);
        }
    }
}