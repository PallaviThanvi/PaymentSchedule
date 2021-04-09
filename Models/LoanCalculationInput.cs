using PaymentSchedule.Models.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentSchedule.Models
{
    public class LoanCalculationInput
    {
        [Required(ErrorMessage = "Please enter the vehicle price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pelase enter a positive price")]
        [DataType(DataType.Currency)]
        public decimal? VehiclePrice { get; set; }

        [Required(ErrorMessage = "Please enter the deposit amount.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pelase enter a positive deposit")]
        [DataType(DataType.Currency)]
        [DepositAmount]
        public decimal? DepositAmount { get; set; }

        [Required(ErrorMessage = "Please enter the delivery date.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [DeliveryDate]
        public DateTime? DeliveryDate { get; set; }

        [Required(ErrorMessage = "Please specify the finance option.")]
        public int? FinanceYear { get; set; }
    }
}
