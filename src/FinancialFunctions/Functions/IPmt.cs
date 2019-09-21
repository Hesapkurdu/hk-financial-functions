using System;
using FinancialFunctions.Domain.Enums;

namespace FinancialFunctions.Functions
{
    public class IPmt
    {
        /// <summary>
        ///     Returns the interest payment for a given period for an investment, based on periodic constant payments and a
        ///     constant interest rate.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="period">Period.</param>
        /// <param name="numberPeriods">Number of periods.</param>
        /// <param name="presentValue">Present value.</param>
        /// <param name="futureValue">Future value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public double InterestPayment(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            short auxiliaryPeriod;
            auxiliaryPeriod = due != DueDate.EndOfPeriod ? (short)2 : (short)1;

            if (period <= 0 || period >= numberPeriods + 1)
                throw new ArgumentException("Invalid Argument Value", "period");
            if (due != DueDate.EndOfPeriod && period == 1)
                return 0;

            var payment = new Pmt().PaymentInternal(rate, numberPeriods, presentValue, futureValue, due);
            if (due != DueDate.EndOfPeriod)
                presentValue += payment;

            var internalFutureValue = new IntFv().FutureValueInternal(rate, (short)(period - auxiliaryPeriod), payment,
                presentValue, DueDate.EndOfPeriod);
            return internalFutureValue * rate;
        }
    }
}
