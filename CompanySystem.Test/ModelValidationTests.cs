using CompanySystem.Domains.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanySystem.Test
{
    public class ModelValidationTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
        [Fact]
        public void CompanyModel_ShouldHaveError_WhenNameIsMissing()
        {
            // Arrange
            var company = new Company { Name = null, Address = "123 Street" };

            // Act
            var errors = ValidateModel(company);

            // Assert
            Assert.Contains(errors, v => v.ErrorMessage == "Company name is a required field.");
        }
        [Fact]
        public void CompanyModel_ShouldHaveError_WhenNameIsTooLong()
        {
            // Arrange: 
            var company = new Company
            {
                Name = new string('A', 61),
                Address = "Valid Address"
            };

            // Act
            var errors = ValidateModel(company);

            // Assert
            Assert.Contains(errors, v => v.ErrorMessage == "Maximum length for the Name is 60 characters.");
        }

        [Fact]
        public void CompanyModel_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var company = new Company
            {
                Name = "Valid Company",
                Address = "123 Valid Street"
            };
            // Act
            var errors = ValidateModel(company);
            // Assert
            Assert.Empty(errors);
        }

        [Fact]

        public void EmployeeModel_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "Jane Smith",
                Age = 28,
                Position = "Manager",

            };
            // Act
            var errors = ValidateModel(employee);
            // Assert
            Assert.Empty(errors);

        }

        [Fact]
        public void EmployeeModel_ShouldHaveError_WhenPositionIsMissing()
        {
            // Arrange
            var employee = new Employee
            {
                Name = "John Doe",
                Age = 30,
                Position = null
            };
            // Act
            var errors = ValidateModel(employee);
            // Assert
            Assert.Contains(errors, v => v.ErrorMessage == "Position is a required field.");
        }
    }
}