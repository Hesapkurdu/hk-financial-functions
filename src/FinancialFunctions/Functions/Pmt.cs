using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    public class Pmt
    {
        /// <summary>
        ///     see Payment
        /// </summary>
        public double PaymentInternal(double rate, short numberPeriods, double presentValue,
            double futureValue, DueDate due)
        {
            double auxiliaryRate1;
            if (numberPeriods == 0)
                throw new ArgumentException("Invalid Argument Value", "numberPeriods");
            if (rate == 0)
                return (-futureValue - presentValue) / numberPeriods;
            if (due != DueDate.EndOfPeriod)
                auxiliaryRate1 = 1 + rate;
            else
                auxiliaryRate1 = 1;
            var auxiliaryRate2 = Math.Pow(rate + 1, numberPeriods);
            return (-futureValue - presentValue * auxiliaryRate2) / (auxiliaryRate1 * (auxiliaryRate2 - 1)) * rate;
        }
    }
}
