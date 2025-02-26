using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using TestInfotecs.Models;
using TestInfotecs.Validators;

namespace Tests
{
    public class ValidationTest
    {
        private readonly Validation _validation;

        public ValidationTest() => _validation = new Validation();
        
        [Fact]
        public void IsValid_ValidationDataTest_ReturnTrue()
        {
            var model = new CSVModel()
            {
               
                Date = "2023-10-27T10:30:00.0000Z",
                ExecutionTime = 30,
                Value = 3
            };


            ValidationResult result = _validation.Validate(model);

            Assert.True(result.IsValid);

        }

        [Theory]
        
        [InlineData("1999-01-01T10:30:00.0000Z", 30, 3)]
        [InlineData("1707-01-01T10:30:00.0000Z", 30, 3)]
        [InlineData("2026-01-01T10:30:00.0000Z", 30, 3)]

        public void IsValid_InvalidDateTest_ReturnFalse(string dateString, int executionTime, double value)
        {
            

            var model = new CSVModel()
            {
                Date = dateString,
                ExecutionTime = executionTime,
                Value = value
            };


            ValidationResult result = _validation.Validate(model);

            Assert.False(result.IsValid);

        }

        [Fact]
        public void IsValid_InvalidExecutionTimeTest_ReturnFalse()
        {
            
            var model = new CSVModel()
            {
                Date = "2023-10-27T10:30:00.0000Z",
                ExecutionTime = -1,
                Value = 3
            };

            ValidationResult result = _validation.Validate(model);

            Assert.False(result.IsValid);

        }

        [Fact]
        public void IsValid_InvalidValueTest_ReturnFalse()
        {

            var model = new CSVModel()
            {
                Date = "2023-10-27T10:30:00.0000Z",
                ExecutionTime = 30,
                Value = -3
            };

            ValidationResult result = _validation.Validate(model);

            Assert.False(result.IsValid);

        }


    }
}