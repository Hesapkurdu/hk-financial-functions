using System;

namespace FinancialFunctions.Functions
{
    class Npv
    {
        /// <summary>
        ///     Returns the net present value of an investment based on a discount rate and a series of future payments
        ///     (negative values) and income (positive values),
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="cashFlow">Cash Flow.</param>
        /// <returns></returns>
        public double NetPresentValue(double rate, double[] cashFlow)
        {
            if (cashFlow == null)
                throw new ArgumentException("Invalid Argument Value", "cashFlow");
            if (cashFlow.Rank != 1)
                throw new ArgumentException("Invalid Argument Value", "cashFlow");
            if (rate == -1)
                throw new ArgumentException("Invalid Argument Value", "rate");
            if (cashFlow.GetUpperBound(0) + 1 < 1)
                throw new ArgumentException("Invalid Argument Value", "cashFlow");
            return new IntPv().PresentValueInternal(rate, cashFlow, 0);
        }
    }
}
