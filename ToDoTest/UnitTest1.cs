using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoTest
{
    public class UnitTest1
    {

        [Fact]
        public void YearFromNow_InvalidPastDate()
        {
            // Arrange
            var attribute = new YearFromNow(2);
            var date = DateTime.Today.AddYears(-3); 

            // Act
            var validationResult = attribute.GetValidationResult(date, new ValidationContext(date));

            // Assert
            Assert.NotNull(validationResult); 
           
        }

        [Fact]
        public void YearFromNow_ValidPastDate()
        {
            // Arrange
            var attribute = new YearFromNow(2) { IsPast = true };
            var date = DateTime.Today.AddYears(-1).AddMonths(6); 

            // Act
            var validationResult = attribute.GetValidationResult(date, new ValidationContext(date));

            // Assert
            Assert.Null(validationResult); 
        }

        [Fact]
        public void YearFromNow_ValidPastDate_IsPastTrue()
        {
            // Arrange
            var attribute = new YearFromNow(2) { IsPast = true };
            var yesterday = DateTime.Today.AddDays(-1);

            // Act
            var validationResult = attribute.GetValidationResult(yesterday, new ValidationContext(yesterday));

            // Assert
            Assert.Null(validationResult);
        }
    }
}