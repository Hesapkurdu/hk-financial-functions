using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    public class EvalRate
    {
        /// <summary>
        ///     Evaluates the rate.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="numberPeriods">Number of periods.</param>
        /// <param name="payment">Payment.</param>
        /// <param name="presentValue">Present value.</param>
        /// <param name="futureValue">Future value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public double EvaluateRate(double rate, short numberPeriods, double payment, double presentValue, double futureValue, DueDate due)
        {
            double auxiliaryRate1;
            if (rate == 0)
                return presentValue + payment * numberPeriods + futureValue;
            var auxiliaryRate2 = Math.Pow(rate + 1, numberPeriods);
            if (due != DueDate.EndOfPeriod)
                auxiliaryRate1 = 1 + rate;
            else
                auxiliaryRate1 = 1;
            return presentValue * auxiliaryRate2 + payment * auxiliaryRate1 * (auxiliaryRate2 - 1) / rate + futureValue;
        }
    }
}
