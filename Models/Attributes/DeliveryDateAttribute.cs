using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentSchedule.Models.Attributes
{
    public class DeliveryDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            LoanCalculationInput input = (LoanCalculationInput)validationContext.ObjectInstance;
            if (input.DeliveryDate == null)
            {
                return ValidationResult.Success; //Other validation attributes should catch this
            }

            if (input.DeliveryDate < DateTime.UtcNow.Date)
                return new ValidationResult("Delivery date cannot be in the past.");

            return ValidationResult.Success;
        }
    }
}
