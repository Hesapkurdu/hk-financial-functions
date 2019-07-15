using FinancialFunctions.Enums;
using System;

namespace FinancialFunctions.Functions
{
    class IntFv
    {
        /// <summary>
        ///     Futures the value internal.
        /// </summary>
        /// <param name="rate">Rate.</param>
        /// <param name="numberPeriods">Number periods.</param>
        /// <param name="payment">Payment.</param>
        /// <param name="presentValue">Present value.</param>
        /// <param name="due">Due.</param>
        /// <returns></returns>
        public double FutureValueInternal(double rate, short numberPeriods, double payment,
           double presentValue, DueDate due)
        {
            double auxiliaryRate1;
            if (rate == 0)
                return -presentValue - payment * numberPeriods;
            if (due != DueDate.EndOfPeriod)
                auxiliaryRate1 = 1 + rate;
            else
                auxiliaryRate1 = 1;
            var auxiliaryRate2 = Math.Pow(1 + rate, numberPeriods);
            return -presentValue * auxiliaryRate2 - payment / rate * auxiliaryRate1 * (auxiliaryRate2 - 1);
        }
    }
}
