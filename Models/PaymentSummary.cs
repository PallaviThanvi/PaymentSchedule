using System;
using System.Collections.Generic;

namespace PaymentSchedule.Models
{
    public class PaymentSummary
    {
        public decimal VehiclePrice { get; }

        public decimal DepositAmount { get; }

        public decimal LoanAmount { get; }

        public double RateOfInterest { get; }

        public int MaximumRepaymentPeriod { get; }

        public decimal MonthlyPayment { get; }

        public DateTime DeliveryDate { get; }

        public decimal ArrangementFee { get; }

        public decimal CompletionFee { get; }

        public List<PaymentItem> PaymentItems { get; }

        public PaymentSummary(LoanCalculationInput input, FeeOptions feeOptions)
        {
            PaymentItems = new List<PaymentItem>();

            VehiclePrice = input.VehiclePrice.Value;
            DepositAmount = input.DepositAmount.Value;
            LoanAmount = VehiclePrice - DepositAmount;
            RateOfInterest = 0;
            MaximumRepaymentPeriod = input.FinanceYear.Value * 12;
            MonthlyPayment = CalculateMonthlyPayment(input);
            DeliveryDate = input.DeliveryDate.Value;
            ArrangementFee = feeOptions.ArrangementFee;
            CompletionFee = feeOptions.CompletionFee;

            decimal totalPayment = 0;
            for (int index = 0; index < MaximumRepaymentPeriod; index++)
            {
                decimal paymentAmount = MonthlyPayment;
                totalPayment += paymentAmount;

                if (index == 0)
                {
                    paymentAmount += ArrangementFee;
                }

                if (index == MaximumRepaymentPeriod - 1)
                {
                    paymentAmount += CompletionFee;
                }

                DateTime deliveryMonth = DeliveryDate.AddMonths(index + 1);
                PaymentItems.Add(new PaymentItem(GetFirstMonday(new DateTime(deliveryMonth.Year, deliveryMonth.Month, 1)), paymentAmount, LoanAmount - totalPayment));
            }
        }

        private decimal CalculateMonthlyPayment(LoanCalculationInput input)
        {
            return LoanAmount / (input.FinanceYear.Value * 12);
        }

        private DateTime GetFirstMonday(DateTime dayOneOfMonth)
        {
            DateTime firstPaymentDate = DateTime.Now;
            switch (dayOneOfMonth.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        firstPaymentDate = dayOneOfMonth;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 7);
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 6);
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 5);
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 4);
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 3);
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        firstPaymentDate = new DateTime(dayOneOfMonth.Year, dayOneOfMonth.Month, 2);
                        break;
                    }
            }

            return firstPaymentDate;
        }
    }
}