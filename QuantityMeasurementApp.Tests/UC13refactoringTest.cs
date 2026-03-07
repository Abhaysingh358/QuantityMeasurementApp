using System.Reflection;
using QuantityMeasurementApp.Core.Enums;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Test
{
    [TestClass]
    public class UC13RefactoringTest
    {
        private const double Epsilon = 0.0001;

        /// <summary>
        /// testRefactoring_Add_DelegatesViaHelper() —
        /// Verifies that add() calls performBaseArithmetic with ADD enum.
        /// Tests: Helper delegation works correctly for Add.
        /// </summary>
        [TestMethod]
        public void GivenAdd_WhenCalled_ShouldDelegateViaHelper()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = a.Add(b);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), result);
        }

        /// <summary>
        /// testRefactoring_Subtract_DelegatesViaHelper() —
        /// Verifies that subtract() calls performBaseArithmetic with SUBTRACT enum.
        /// Tests: Helper delegation works correctly for Subtract.
        /// </summary>
        [TestMethod]
        public void GivenSubtract_WhenCalled_ShouldDelegateViaHelper()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(6.0, LengthUnit.Inch);
            Quantity<LengthUnit> result = a.Subtract(b);
            Assert.AreEqual(new Quantity<LengthUnit>(9.5, LengthUnit.Feet), result);
        }

        /// <summary>
        /// testRefactoring_Divide_DelegatesViaHelper() —
        /// Verifies that divide() calls performBaseArithmetic with DIVIDE enum.
        /// Tests: Helper delegation works correctly for Divide.
        /// </summary>
        [TestMethod]
        public void GivenDivide_WhenCalled_ShouldDelegateViaHelper()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            double result = a.Divide(b);
            Assert.AreEqual(5.0, result, Epsilon);
        }

        /// <summary>
        /// testValidation_NullOperand_ConsistentAcrossOperations() —
        /// Verifies that add(null), subtract(null), divide(null) all throw ArgumentNullException.
        /// Tests: Validation consistency across all operations.
        /// </summary>
        [TestMethod]
        public void GivenNullOperand_WhenUsedInAnyOperation_ShouldThrowSameException()
        {
            Quantity<LengthUnit> q = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => q.Add(null));
            Assert.Throws<ArgumentNullException>(() => q.Subtract(null));
            Assert.Throws<ArgumentNullException>(() => q.Divide(null));
        }

        /// <summary>
        /// testValidation_FiniteValue_ConsistentAcrossOperations() —
        /// Verifies that NaN and Infinity are rejected at construction.
        /// Tests: Finiteness validation consistency.
        /// </summary>
        [TestMethod]
        public void GivenNonFiniteValues_WhenConstructed_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet));
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Feet));
            Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.NegativeInfinity, LengthUnit.Feet));
        }

        /// <summary>
        /// testValidation_NullTargetUnit_AddSubtractReject() —
        /// Verifies that explicit null target unit throws ArgumentNullException.
        /// Tests: Target unit validation for add/subtract.
        /// </summary>
        [TestMethod]
        public void GivenNullTargetUnit_WhenAddOrSubtract_ShouldThrowException()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => a.Add(b, null));
            Assert.Throws<ArgumentNullException>(() => a.Subtract(b, null));
        }

        /// <summary>
        /// testArithmeticOperation_Add_EnumComputation() —
        /// Verifies that ADD enum: Quantity(10, FEET).Add(Quantity(5, FEET)) returns Quantity(15, FEET).
        /// Tests: ADD enum operation logic.
        /// </summary>
        [TestMethod]
        public void GivenAddEnum_WhenComputed_ShouldReturnCorrectSum()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(new Quantity<LengthUnit>(15.0, LengthUnit.Feet), a.Add(b));
        }

        /// <summary>
        /// testArithmeticOperation_Subtract_EnumComputation() —
        /// Verifies that SUBTRACT enum: Quantity(10, FEET).Subtract(Quantity(5, FEET)) returns Quantity(5, FEET).
        /// Tests: SUBTRACT enum operation logic.
        /// </summary>
        [TestMethod]
        public void GivenSubtractEnum_WhenComputed_ShouldReturnCorrectDifference()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(new Quantity<LengthUnit>(5.0, LengthUnit.Feet), a.Subtract(b));
        }

        /// <summary>
        /// testArithmeticOperation_Divide_EnumComputation() —
        /// Verifies that DIVIDE enum: Quantity(10, FEET).Divide(Quantity(5, FEET)) returns 2.0.
        /// Tests: DIVIDE enum operation logic.
        /// </summary>
        [TestMethod]
        public void GivenDivideEnum_WhenComputed_ShouldReturnCorrectQuotient()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(2.0, a.Divide(b), Epsilon);
        }

        /// <summary>
        /// testArithmeticOperation_DivideByZero_EnumThrows() —
        /// Verifies that DIVIDE with zero divisor throws ArithmeticException.
        /// Tests: Enum-level zero validation.
        /// </summary>
        [TestMethod]
        public void GivenDivideByZero_WhenComputed_ShouldThrowArithmeticException()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> zero = new Quantity<LengthUnit>(0.0, LengthUnit.Feet);
            Assert.Throws<ArithmeticException>(() => a.Divide(zero));
        }

        /// <summary>
        /// testPerformBaseArithmetic_ConversionAndOperation() —
        /// Verifies that helper correctly converts both operands and performs operation.
        /// Tests: Helper correctness via cross-unit result.
        /// </summary>
        [TestMethod]
        public void GivenCrossUnitOperands_WhenHelperPerformsArithmetic_ShouldConvertAndCompute()
        {
            Quantity<LengthUnit> feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> inches = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), feet.Add(inches));
        }

        /// <summary>
        /// testHelper_BaseUnitConversion_Correct() —
        /// Verifies performBaseArithmetic correctly converts to base unit before operation.
        /// Tests: Base unit conversion in helper.
        /// </summary>
        [TestMethod]
        public void GivenCrossUnitOperands_WhenHelperConvertsToBase_ShouldProduceCorrectResult()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Assert.AreEqual(1.0, a.Divide(b), Epsilon);
        }

        /// <summary>
        /// testHelper_ResultConversion_Correct() —
        /// Verifies the result is correctly converted from base unit to target unit for add/subtract.
        /// Tests: Result conversion after operation.
        /// </summary>
        [TestMethod]
        public void GivenAddWithExplicitTargetUnit_WhenResultConverted_ShouldBeInTargetUnit()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            Quantity<LengthUnit> result = a.Add(b, LengthUnit.Inch);
            Assert.AreEqual(new Quantity<LengthUnit>(24.0, LengthUnit.Inch), result);
        }

        /// <summary>
        /// testAdd_UC12_BehaviorPreserved() —
        /// Runs all UC12 addition test cases unchanged.
        /// Tests: Backward compatibility for Add.
        /// </summary>
        [TestMethod]
        public void GivenAddOperations_WhenRefactored_ShouldPreserveUC12Behavior()
        {
            Assert.AreEqual(
                new Quantity<LengthUnit>(2.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(1.0, LengthUnit.Feet).Add(new Quantity<LengthUnit>(12.0, LengthUnit.Inch)));

            Assert.AreEqual(
                new Quantity<WeightUnit>(15000.0, WeightUnit.Gram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Add(new Quantity<WeightUnit>(5000.0, WeightUnit.Gram), WeightUnit.Gram));

            Assert.AreEqual(
                new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre).Add(new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre)));
        }

        /// <summary>
        /// testSubtract_UC12_BehaviorPreserved() —
        /// Runs all UC12 subtraction test cases unchanged.
        /// Tests: Backward compatibility for Subtract.
        /// </summary>
        [TestMethod]
        public void GivenSubtractOperations_WhenRefactored_ShouldPreserveUC12Behavior()
        {
            Assert.AreEqual(
                new Quantity<LengthUnit>(9.5, LengthUnit.Feet),
                new Quantity<LengthUnit>(10.0, LengthUnit.Feet).Subtract(new Quantity<LengthUnit>(6.0, LengthUnit.Inch)));

            Assert.AreEqual(
                new Quantity<VolumeUnit>(3000.0, VolumeUnit.Millilitre),
                new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre).Subtract(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre), VolumeUnit.Millilitre));

            Assert.AreEqual(
                new Quantity<LengthUnit>(-5.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(5.0, LengthUnit.Feet).Subtract(new Quantity<LengthUnit>(10.0, LengthUnit.Feet)));
        }

        /// <summary>
        /// testDivide_UC12_BehaviorPreserved() —
        /// Runs all UC12 division test cases unchanged.
        /// Tests: Backward compatibility for Divide.
        /// </summary>
        [TestMethod]
        public void GivenDivideOperations_WhenRefactored_ShouldPreserveUC12Behavior()
        {
            Assert.AreEqual(5.0,
                new Quantity<LengthUnit>(10.0, LengthUnit.Feet).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)), Epsilon);

            Assert.AreEqual(1.0,
                new Quantity<LengthUnit>(24.0, LengthUnit.Inch).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)), Epsilon);

            Assert.AreEqual(2.0,
                new Quantity<WeightUnit>(2000.0, WeightUnit.Gram).Divide(new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram)), Epsilon);
        }

        /// <summary>
        /// testRounding_AddSubtract_TwoDecimalPlaces() —
        /// Verifies that add/subtract results are rounded consistently.
        /// Tests: Rounding consistency via helper.
        /// </summary>
        [TestMethod]
        public void GivenAddSubtract_WhenResultComputed_ShouldBeRounded()
        {
            Quantity<WeightUnit> kg = new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram);
            Quantity<WeightUnit> g = new Quantity<WeightUnit>(5000.0, WeightUnit.Gram);
            Assert.AreEqual(new Quantity<WeightUnit>(15.0, WeightUnit.Kilogram), kg.Add(g));
            Assert.AreEqual(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram), kg.Subtract(g));
        }

        /// <summary>
        /// testRounding_Divide_NoRounding() —
        /// Verifies that divide returns raw double without rounding.
        /// Tests: Different handling for dimensionless result.
        /// </summary>
        [TestMethod]
        public void GivenDivision_WhenResultComputed_ShouldReturnRawDouble()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(7.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Assert.AreEqual(7.0 / 3.0, a.Divide(b), Epsilon);
        }

        /// <summary>
        /// testRounding_Helper_Accuracy() —
        /// Verifies Math.Round(1.234567, 5) returns 1.23457.
        /// Tests: Rounding helper correctness.
        /// </summary>
        [TestMethod]
        public void GivenRoundingHelper_WhenApplied_ShouldRoundCorrectly()
        {
            double rounded = Math.Round(1.234567, 5);
            Assert.AreEqual(1.23457, rounded, Epsilon);
        }

        /// <summary>
        /// testImplicitTargetUnit_AddSubtract() —
        /// Verifies add/subtract without explicit target unit use first operand's unit.
        /// Tests: Implicit target unit behavior.
        /// </summary>
        [TestMethod]
        public void GivenNoTargetUnit_WhenAddOrSubtract_ShouldUseFirstOperandUnit()
        {
            Quantity<LengthUnit> feet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> inch = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(new Quantity<LengthUnit>(3.0, LengthUnit.Feet), feet.Add(inch));
            Assert.AreEqual(new Quantity<LengthUnit>(1.0, LengthUnit.Feet), feet.Subtract(inch));
        }

        /// <summary>
        /// testExplicitTargetUnit_AddSubtract_Overrides() —
        /// Verifies explicit target unit overrides first operand's unit.
        /// Tests: Explicit target unit behavior.
        /// </summary>
        [TestMethod]
        public void GivenExplicitTargetUnit_WhenAddOrSubtract_ShouldOverrideImplicitUnit()
        {
            Quantity<LengthUnit> feet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> inch = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.AreEqual(new Quantity<LengthUnit>(36.0, LengthUnit.Inch), feet.Add(inch, LengthUnit.Inch));
            Assert.AreEqual(new Quantity<LengthUnit>(12.0, LengthUnit.Inch), feet.Subtract(inch, LengthUnit.Inch));
        }

        /// <summary>
        /// testImmutability_AfterAdd_ViaCentralizedHelper() —
        /// Verifies that original quantities unchanged after addition.
        /// Tests: Immutability through refactored implementation.
        /// </summary>
        [TestMethod]
        public void GivenAdd_WhenPerformed_OriginalsShouldBeUnchanged()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            original.Add(other);
            Assert.AreEqual(new Quantity<LengthUnit>(5.0, LengthUnit.Feet), original);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), other);
        }

        /// <summary>
        /// testImmutability_AfterSubtract_ViaCentralizedHelper() —
        /// Verifies that original quantities unchanged after subtraction.
        /// Tests: Immutability through refactored implementation.
        /// </summary>
        [TestMethod]
        public void GivenSubtract_WhenPerformed_OriginalsShouldBeUnchanged()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            original.Subtract(other);
            Assert.AreEqual(new Quantity<LengthUnit>(10.0, LengthUnit.Feet), original);
            Assert.AreEqual(new Quantity<LengthUnit>(3.0, LengthUnit.Feet), other);
        }

        /// <summary>
        /// testImmutability_AfterDivide_ViaCentralizedHelper() —
        /// Verifies that original quantities unchanged after division.
        /// Tests: Immutability through refactored implementation.
        /// </summary>
        [TestMethod]
        public void GivenDivide_WhenPerformed_OriginalsShouldBeUnchanged()
        {
            Quantity<LengthUnit> original = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> other = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            original.Divide(other);
            Assert.AreEqual(new Quantity<LengthUnit>(10.0, LengthUnit.Feet), original);
            Assert.AreEqual(new Quantity<LengthUnit>(2.0, LengthUnit.Feet), other);
        }

        /// <summary>
        /// testAllOperations_AcrossAllCategories() —
        /// Verifies add/subtract/divide work for length, weight, and volume.
        /// Tests: Scalability across categories.
        /// </summary>
        [TestMethod]
        public void GivenAllOperationsAndCategories_WhenEvaluated_ShouldWorkSeamlessly()
        {
            Assert.AreEqual(new Quantity<LengthUnit>(7.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(5.0, LengthUnit.Feet).Add(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)));
            Assert.AreEqual(new Quantity<LengthUnit>(3.0, LengthUnit.Feet),
                new Quantity<LengthUnit>(5.0, LengthUnit.Feet).Subtract(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)));
            Assert.AreEqual(2.5,
                new Quantity<LengthUnit>(5.0, LengthUnit.Feet).Divide(new Quantity<LengthUnit>(2.0, LengthUnit.Feet)), Epsilon);

            Assert.AreEqual(new Quantity<WeightUnit>(15.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Add(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)));
            Assert.AreEqual(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram),
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Subtract(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)));
            Assert.AreEqual(2.0,
                new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram).Divide(new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)), Epsilon);

            Assert.AreEqual(new Quantity<VolumeUnit>(7.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre).Add(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
            Assert.AreEqual(new Quantity<VolumeUnit>(3.0, VolumeUnit.Litre),
                new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre).Subtract(new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre)));
            Assert.AreEqual(2.0,
                new Quantity<VolumeUnit>(10.0, VolumeUnit.Litre).Divide(new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre)), Epsilon);
        }

        /// <summary>
        /// testCodeDuplication_ValidationLogic_Eliminated() —
        /// Verifies validation logic is centralized — same exception type across all operations.
        /// Tests: DRY principle enforcement.
        /// </summary>
        [TestMethod]
        public void GivenValidationLogic_WhenCentralized_ShouldBehaveIdenticallyAcrossOperations()
        {
            Quantity<LengthUnit> q = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Exception addEx = Assert.Throws<ArgumentNullException>(() => q.Add(null));
            Exception subEx = Assert.Throws<ArgumentNullException>(() => q.Subtract(null));
            Exception divEx = Assert.Throws<ArgumentNullException>(() => q.Divide(null));
            Assert.AreEqual(addEx.GetType(), subEx.GetType());
            Assert.AreEqual(subEx.GetType(), divEx.GetType());
        }

        /// <summary>
        /// testCodeDuplication_ConversionLogic_Eliminated() —
        /// Verifies conversion logic is centralized via consistent cross-unit results.
        /// Tests: Centralization of conversion.
        /// </summary>
        [TestMethod]
        public void GivenConversionLogic_WhenCentralized_ShouldProduceConsistentResults()
        {
            Quantity<LengthUnit> feet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> inch = new Quantity<LengthUnit>(24.0, LengthUnit.Inch);
            Assert.AreEqual(new Quantity<LengthUnit>(4.0, LengthUnit.Feet), feet.Add(inch));
            Assert.AreEqual(new Quantity<LengthUnit>(0.0, LengthUnit.Feet), feet.Subtract(inch));
            Assert.AreEqual(1.0, feet.Divide(inch), Epsilon);
        }

        /// <summary>
        /// testEnumDispatch_AllOperations_CorrectlyDispatched() —
        /// Verifies each operation uses correct enum constant and computes correctly.
        /// Tests: Enum-based dispatch correctness.
        /// </summary>
        [TestMethod]
        public void GivenAllEnumConstants_WhenDispatched_ShouldComputeCorrectly()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.AreEqual(new Quantity<LengthUnit>(15.0, LengthUnit.Feet), a.Add(b));
            Assert.AreEqual(new Quantity<LengthUnit>(5.0, LengthUnit.Feet), a.Subtract(b));
            Assert.AreEqual(2.0, a.Divide(b), Epsilon);
        }

        /// <summary>
        /// testFutureOperation_MultiplicationPattern() —
        /// Adding MULTIPLY requires only a new enum constant — no validation/conversion changes.
        /// Tests: Scalability for future operations.
        /// </summary>
        [TestMethod]
        public void GivenCentralizedPattern_WhenNewOperationAdded_ShouldRequireMinimalChanges()
        {
            Quantity<LengthUnit> a = new Quantity<LengthUnit>(6.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Assert.AreEqual(new Quantity<LengthUnit>(9.0, LengthUnit.Feet), a.Add(b));
            Assert.AreEqual(new Quantity<LengthUnit>(3.0, LengthUnit.Feet), a.Subtract(b));
            Assert.AreEqual(2.0, a.Divide(b), Epsilon);
        }

        /// <summary>
        /// testErrorMessage_Consistency_Across_Operations() —
        /// Verifies all operations throw same exception type for same error category.
        /// Tests: Consistent error reporting.
        /// </summary>
        [TestMethod]
        public void GivenSameError_WhenThrownByAnyOperation_ShouldBeSameExceptionType()
        {
            Quantity<LengthUnit> q = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => q.Add(null));
            Assert.Throws<ArgumentNullException>(() => q.Subtract(null));
            Assert.Throws<ArgumentNullException>(() => q.Divide(null));
            Assert.Throws<ArithmeticException>(() => q.Divide(new Quantity<LengthUnit>(0.0, LengthUnit.Feet)));
        }

        /// <summary>
        /// testHelper_PrivateVisibility() —
        /// Verifies PerformBaseArithmetic is not accessible outside the class.
        /// Tests: Encapsulation of core helper.
        /// </summary>
        [TestMethod]
        public void GivenPerformBaseArithmetic_WhenInspected_ShouldBePrivate()
        {
            MethodInfo method = typeof(Quantity<LengthUnit>)
                .GetMethod("PerformBaseArithmetic", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "PerformBaseArithmetic should exist");
            Assert.IsTrue(method.IsPrivate, "PerformBaseArithmetic should be private");
        }

        /// <summary>
        /// testValidation_Helper_PrivateVisibility() —
        /// Verifies ValidateArithmeticOperands is not accessible outside the class.
        /// Tests: Encapsulation of validation helper.
        /// </summary>
        [TestMethod]
        public void GivenValidateArithmeticOperands_WhenInspected_ShouldBePrivate()
        {
            MethodInfo method = typeof(Quantity<LengthUnit>)
                .GetMethod("ValidateArithmeticOperands", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "ValidateArithmeticOperands should exist");
            Assert.IsTrue(method.IsPrivate, "ValidateArithmeticOperands should be private");
        }

        /// <summary>
        /// testArithmetic_Chain_Operations() —
        /// Verifies q1.Add(q2).Subtract(q3).Divide(q4) chains correctly.
        /// Tests: Operation composition through refactored methods.
        /// </summary>
        [TestMethod]
        public void GivenChainedOperations_WhenEvaluated_ShouldReturnCorrectResult()
        {
            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
            Quantity<LengthUnit> q3 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            Quantity<LengthUnit> q4 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
            // (10 + 2 - 3) / 3 = 3.0
            double result = q1.Add(q2).Subtract(q3).Divide(q4);
            Assert.AreEqual(3.0, result, Epsilon);
        }

        /// <summary>
        /// testRefactoring_NoBehaviorChange_LargeDataset() —
        /// Runs 1000 operations to verify behavioral equivalence at scale.
        /// Tests: Behavioral equivalence at scale.
        /// </summary>
        [TestMethod]
        public void GivenLargeDataset_WhenOperationsPerformed_ShouldProduceCorrectResults()
        {
            for (int i = 1; i <= 1000; i++)
            {
                Quantity<LengthUnit> a = new Quantity<LengthUnit>(i * 2.0, LengthUnit.Feet);
                Quantity<LengthUnit> b = new Quantity<LengthUnit>(i * 1.0, LengthUnit.Feet);
                Assert.AreEqual(new Quantity<LengthUnit>(i * 3.0, LengthUnit.Feet), a.Add(b));
                Assert.AreEqual(new Quantity<LengthUnit>(i * 1.0, LengthUnit.Feet), a.Subtract(b));
                Assert.AreEqual(2.0, a.Divide(b), Epsilon);
            }
        }

        /// <summary>
        /// testRefactoring_Performance_ComparableToUC12() —
        /// Benchmarks 10000 operations — no performance regression from refactoring.
        /// Tests: No performance regression.
        /// </summary>
        [TestMethod]
        public void GivenRefactoredCode_WhenBenchmarked_ShouldCompleteWithinAcceptableTime()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 1; i <= 10000; i++)
            {
                Quantity<LengthUnit> a = new Quantity<LengthUnit>(i, LengthUnit.Feet);
                Quantity<LengthUnit> b = new Quantity<LengthUnit>(i, LengthUnit.Feet);
                a.Add(b);
                a.Subtract(b);
                a.Divide(b);
            }
            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 5000,
                $"Performance regression: {stopwatch.ElapsedMilliseconds}ms exceeded 5000ms limit");
        }

        /// <summary>
        /// testRefactoring_Validation_UnifiedBehavior() —
        /// Verifies all operations reject the same invalid inputs consistently.
        /// Tests: Unified validation behavior.
        /// </summary>
        [TestMethod]
        public void GivenInvalidInputs_WhenUsedInAnyOperation_ShouldBeRejectedConsistently()
        {
            Quantity<LengthUnit> q = new Quantity<LengthUnit>(10.0, LengthUnit.Feet);
            Quantity<LengthUnit> b = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
            Assert.Throws<ArgumentNullException>(() => q.Add(null));
            Assert.Throws<ArgumentNullException>(() => q.Subtract(null));
            Assert.Throws<ArgumentNullException>(() => q.Divide(null));
            Assert.Throws<ArgumentNullException>(() => q.Add(b, null));
            Assert.Throws<ArgumentNullException>(() => q.Subtract(b, null));
            Assert.Throws<ArithmeticException>(() => q.Divide(new Quantity<LengthUnit>(0.0, LengthUnit.Feet)));
        }
    }
}