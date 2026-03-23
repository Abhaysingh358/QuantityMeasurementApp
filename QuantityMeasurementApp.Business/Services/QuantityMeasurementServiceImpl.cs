using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Models.DTO;
using QuantityMeasurementApp.Models.Entities;
using QuantityMeasurementApp.Models.Enums;
using QuantityMeasurementApp.Models.Exceptions;
using QuantityMeasurementApp.Models.Interfaces;

namespace QuantityMeasurementApp.Business.Services
{
    // UC15 - this is where all the business logic lives now
    // earlier Quantity<T> class was doing everything — holding value, unit and doing operations
    // now the data part is in QuantityDTO and QuantityModel, and logic is here
    // this makes it easier to test the logic without worrying about how data is stored

    // the service receives QuantityDTO from console or future API
    // it converts the DTO strings to actual unit instances using GetByName()
    // does the operation, wraps result back in QuantityDTO and returns

    // IQuantityMeasurementRepository is injected via constructor
    // right now we pass NoOp which does nothing
    // when real repo comes later, we just pass that — this class doesn't change
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        // i receive the repository from outside instead of creating it here
        // this is dependency injection — makes it easy to swap implementations
        // also makes testing easier because we can pass a fake repo in tests
        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository), "Repository cannot be null");

            _repository = repository;
        }
        

        // COMPARE

        public bool Compare(QuantityDTO dto1, QuantityDTO dto2)
        {
            ValidateNotNull(dto1, "First quantity");
            ValidateNotNull(dto2, "Second quantity");

            try
            {
                // if categories are different, they can never be equal
                // no point converting — just return false immediately
                if (!string.Equals(dto1.MeasurementType, dto2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                    return false;

                IMeasurable unit1 = ResolveUnit(dto1);
                IMeasurable unit2 = ResolveUnit(dto2);

                // convert both to base unit and compare with epsilon tolerance
                double base1 = unit1.ConvertToBaseUnit(dto1.Value);
                double base2 = unit2.ConvertToBaseUnit(dto2.Value);

                return Math.Abs(base1 - base2) < 0.0001;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException($"Compare failed: {ex.Message}", ex);
            }
        }

        // CONVERT 

        public QuantityDTO Convert(QuantityDTO dto, string targetUnit)
        {
            ValidateNotNull(dto, "Input quantity");

            if (string.IsNullOrWhiteSpace(targetUnit))
                throw new QuantityMeasurementException("Target unit cannot be empty");

            try
            {
                IMeasurable sourceUnit = ResolveUnit(dto);

                // resolve target unit using same category as source
                IMeasurable targetUnitInstance = ResolveUnitByTypeAndName(dto.MeasurementType, targetUnit);

                double baseValue = sourceUnit.ConvertToBaseUnit(dto.Value);
                double converted = Math.Round(targetUnitInstance.ConvertFromBaseUnit(baseValue), 5);

                return new QuantityDTO(converted, targetUnit, dto.MeasurementType);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException($"Convert failed: {ex.Message}", ex);
            }
        }

        // ADD

        public QuantityDTO Add(QuantityDTO dto1, QuantityDTO dto2)
        {
            ValidateNotNull(dto1, "First quantity");
            ValidateNotNull(dto2, "Second quantity");
            ValidateSameCategory(dto1, dto2, "Add");

            try
            {
                IMeasurable unit1 = ResolveUnit(dto1);
                IMeasurable unit2 = ResolveUnit(dto2);

                // UC14 - this throws for temperature — temperature does not support arithmetic
                unit1.ValidateOperationSupport("Add");

                double base1 = unit1.ConvertToBaseUnit(dto1.Value);
                double base2 = unit2.ConvertToBaseUnit(dto2.Value);
                double baseResult = base1 + base2;

                // convert result back to first operand's unit
                double result = Math.Round(unit1.ConvertFromBaseUnit(baseResult), 5);

                return new QuantityDTO(result, dto1.Unit, dto1.MeasurementType);
            }
            catch (InvalidOperationException ex)
            {
                // temperature throws InvalidOperationException, we wrap it in our custom exception
                throw new QuantityMeasurementException(ex.Message, ex);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException($"Add failed: {ex.Message}", ex);
            }
        }

        // SUBTRACT 

        public QuantityDTO Subtract(QuantityDTO dto1, QuantityDTO dto2)
        {
            ValidateNotNull(dto1, "First quantity");
            ValidateNotNull(dto2, "Second quantity");
            ValidateSameCategory(dto1, dto2, "Subtract");

            try
            {
                IMeasurable unit1 = ResolveUnit(dto1);
                IMeasurable unit2 = ResolveUnit(dto2);

                // UC14 - blocks temperature arithmetic same as Add
                unit1.ValidateOperationSupport("Subtract");

                double base1 = unit1.ConvertToBaseUnit(dto1.Value);
                double base2 = unit2.ConvertToBaseUnit(dto2.Value);
                double baseResult = base1 - base2;
                double result = Math.Round(unit1.ConvertFromBaseUnit(baseResult), 5);

                return new QuantityDTO(result, dto1.Unit, dto1.MeasurementType);
            }
            catch (InvalidOperationException ex)
            {
                throw new QuantityMeasurementException(ex.Message, ex);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException($"Subtract failed: {ex.Message}", ex);
            }
        }

        // DIVIDE

        public double Divide(QuantityDTO dto1, QuantityDTO dto2)
        {
            ValidateNotNull(dto1, "First quantity");
            ValidateNotNull(dto2, "Second quantity");
            ValidateSameCategory(dto1, dto2, "Divide");

            try
            {
                IMeasurable unit1 = ResolveUnit(dto1);
                IMeasurable unit2 = ResolveUnit(dto2);

                // UC14 - blocks temperature arithmetic same as Add and Subtract
                unit1.ValidateOperationSupport("Divide");

                double base1 = unit1.ConvertToBaseUnit(dto1.Value);
                double base2 = unit2.ConvertToBaseUnit(dto2.Value);

                if (base2 == 0)
                    throw new QuantityMeasurementException("Cannot divide by zero quantity");

                // result has no unit — just a ratio
                return base1 / base2;
            }
            catch (InvalidOperationException ex)
            {
                throw new QuantityMeasurementException(ex.Message, ex);
            }
            catch (QuantityMeasurementException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new QuantityMeasurementException($"Divide failed: {ex.Message}", ex);
            }
        }

        // PRIVATE HELPERS

        // takes a QuantityDTO and returns the actual unit instance
        // example: dto with MeasurementType="Length" and Unit="Feet" → returns LengthUnit.Feet
        private IMeasurable ResolveUnit(QuantityDTO dto)
        {
            return ResolveUnitByTypeAndName(dto.MeasurementType, dto.Unit);
        }

        // switches on category name and calls the right GetByName()
        // this is the mapping between QuantityDTO strings and actual unit instances
        private IMeasurable ResolveUnitByTypeAndName(string measurementType, string unitName)
        {
            switch (measurementType.ToLower())
            {
                case "length": return LengthUnit.GetByName(unitName);
                case "weight": return WeightUnit.GetByName(unitName);
                case "volume": return VolumeUnit.GetByName(unitName);
                case "temperature": return TemperatureUnit.GetByName(unitName);
                default:
                    throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}");
            }
        }

        private void ValidateNotNull(QuantityDTO dto, string name)
        {
            if (dto == null)
                throw new QuantityMeasurementException($"{name} cannot be null");
        }

        // checks both DTOs belong to same category before any arithmetic
        // you can't add Length + Weight — this stops that
        private void ValidateSameCategory(QuantityDTO dto1, QuantityDTO dto2, string operation)
        {
            if (!string.Equals(dto1.MeasurementType, dto2.MeasurementType, StringComparison.OrdinalIgnoreCase))
                throw new QuantityMeasurementException(
                    $"Cannot {operation} across different categories: " +
                    $"{dto1.MeasurementType} and {dto2.MeasurementType}");
        }

        
    }
}