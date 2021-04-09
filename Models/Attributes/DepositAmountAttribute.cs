using System.ComponentModel.DataAnnotations;

namespace PaymentSchedule.Models.Attributes
{
    public class DepositAmountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            LoanCalculationInput input = (LoanCalculationInput)validationContext.ObjectInstance;
          
            if (input.VehiclePrice.HasValue && input.VehiclePrice.Value > 0 &&
                input.DepositAmount.HasValue && input.DepositAmount.Value > 0)
            {
                decimal depositAmount = input.DepositAmount.Value;
                decimal vehiclePrice = input.VehiclePrice.Value;

                if (depositAmount > vehiclePrice)
                    return new ValidationResult("Deposit amount should be less than the vehicle price.");

                if (depositAmount < (15 * vehiclePrice) / 100)
                    return new ValidationResult("Required minimum deposit amount is 15%.");
            }

            return ValidationResult.Success;
        }
    }
}
