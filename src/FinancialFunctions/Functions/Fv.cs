using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    public class Fv
    {
        /// <summary>
        ///     Returns the future value of an investment based on periodic, constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="numberPeriods">Number of periods.</param>
        /// <param name="payment">Periodic Payment.</param>
        /// <param name="presentValue">Present value.</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <returns></returns>
        public double FutureValue(double rate, short numberPeriods, double payment, double presentValue, DueDate due)
        {
            return new IntFv().FutureValueInternal(rate, numberPeriods, payment, presentValue, due);
        }
    }
}
