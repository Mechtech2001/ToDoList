using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class YearFromNow : ValidationAttribute
    {
        private int numYears;
        public YearFromNow(int years)
        {
            numYears = years;
        }
        public bool IsPast { get; set; } = false;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                DateTime now = DateTime.Today;
                DateTime limit;

                if(IsPast)
                {
                    limit = new DateTime(now.Year, 1, 1);
                    limit = limit.AddYears(-numYears);
                } 
                else
                {
                    limit = new DateTime(numYears, 12, 31);
                    limit = limit.AddYears(numYears);
                }

                if (IsPast)
                {
                    if (dateToCheck >= limit && dateToCheck < now)
                    {
                        return ValidationResult.Success!;
                    }
                } 
                else
                {
                    if (dateToCheck > now && dateToCheck <= limit)
                    {
                        return ValidationResult.Success!;
                    }
                }
            }

            string msg = base.ErrorMessage ?? validationContext.DisplayName + " must be a " + (IsPast ? "past" : "future") + " date within " + numYears + " years of now.";
            return new ValidationResult(msg);
        }
    }
}
