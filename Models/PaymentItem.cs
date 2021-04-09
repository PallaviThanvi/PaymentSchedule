using System;

namespace PaymentSchedule.Models
{
    public class PaymentItem
    {
        public DateTime PaymentDate { get; }

        public decimal PaymentAmount { get; }

        public decimal AmountDue { get; }

        public PaymentItem(DateTime paymentDate, decimal paymentAmount, decimal amountDue)
        {
            PaymentDate = paymentDate;
            PaymentAmount = paymentAmount;
            AmountDue = amountDue;
        }
    }
}
