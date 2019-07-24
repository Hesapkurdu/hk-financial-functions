using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    public class PPmt
    {
        /// <summary>
        ///     Returns the capital payment for a given period for an investment based on periodic,
        ///     constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="period">Period.</param>
        /// <param name="numberPeriods">Number of periods.</param>
        /// <param name="presentValue">Present (principal) value.</param>
        /// <param name="futureValue">Future value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public double CapitalPayment(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            if (period <= 0 || period >= numberPeriods + 1)
                throw new ArgumentException("Invalid Argument Value", "period");
            var payment = new Pmt().PaymentInternal(rate, numberPeriods, presentValue, futureValue, due);
            var interest = new IPmt().InterestPayment(rate, period, numberPeriods, presentValue, futureValue, due);
            return payment - interest;
        }
    }
}
