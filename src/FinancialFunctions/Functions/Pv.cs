using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    public class Pv
    {
        /// <summary>
        ///     Returns the present value of an investment. The present value is the total amount that a series of future payments
        ///     is worth now.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="numberPeriods">Number of periods.</param>
        /// <param name="payment">Payment.</param>
        /// <param name="futureValue">Future value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public double PresentValue(double rate, short numberPeriods, double payment, double futureValue, DueDate due)
        {
            double auxiliaryRate1;
            if (rate == 0)
                return -futureValue - payment * numberPeriods;
            if (due != DueDate.EndOfPeriod)
                auxiliaryRate1 = 1 + rate;
            else
                auxiliaryRate1 = 1;
            var auxiliaryRate2 = Math.Pow(1 + rate, numberPeriods);
            return -(futureValue + payment * auxiliaryRate1 * ((auxiliaryRate2 - 1) / rate)) / auxiliaryRate2;
        }
    }
}
