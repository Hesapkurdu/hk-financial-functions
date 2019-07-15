using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    class NPer
    {
        /// <summary>
        ///     Returns the number of periods for an investment, based on periodic constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="payment">Payment.</param>
        /// <param name="presentValue">Present (principal) value.</param>
        /// <param name="futureValue">Future value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public short NumberOfPeriods(double rate, double payment, double presentValue, double futureValue, DueDate due)
        {
            double auxiliaryPayment;
            if (rate <= -1)
                throw new ArgumentException("Invalid Argument Value", "rate");
            if (rate == 0)
            {
                if (payment == 0)
                    throw new ArgumentException("Invalid Argument Value", "payment");
                return (short)(-(presentValue + futureValue) / payment);
            }
            if (due != DueDate.EndOfPeriod)
                auxiliaryPayment = payment * (1 + rate) / rate;
            else
                auxiliaryPayment = payment / rate;
            var auxiliaryFutureValue = -futureValue + auxiliaryPayment;
            var auxiliaryPresentValue = presentValue + auxiliaryPayment;
            if (auxiliaryFutureValue < 0 && auxiliaryPresentValue < 0)
            {
                auxiliaryFutureValue = -1 * auxiliaryFutureValue;
                auxiliaryPresentValue = -1 * auxiliaryPresentValue;
            }
            else if (auxiliaryFutureValue <= 0 || auxiliaryPresentValue <= 0)
            {
                throw new ArgumentException("Cannot Calculate Number of Periods");
            }
            return (short)((Math.Log(auxiliaryFutureValue) - Math.Log(auxiliaryPresentValue)) /
                            Math.Log(rate + 1));
        }
    }
}
