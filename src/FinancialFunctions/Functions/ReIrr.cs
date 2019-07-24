using System;

namespace FinancialFunctions.Functions
{
    public class ReIrr
    {

        /// <summary>
        ///     Returns the internal rate of return for a series of periodic cash flows,
        ///     considering both cost of investment and interest on reinvestment of cash
        /// </summary>
        /// <param name="cashFlow">Value array.</param>
        /// <param name="financeRate">Finance rate.</param>
        /// <param name="reInvestRate">Reinvestment rate.</param>
        /// <returns></returns>
        public double ReinvestedInternalReturnRate(double[] cashFlow, double financeRate,
            double reInvestRate)
        {
            if (cashFlow.Rank != 1)
                throw new ArgumentException("Invalid Argument Value", "CashFlow");
            if (financeRate == -1)
                throw new ArgumentException("Invalid Argument Value", "FinanceRate");
            if (reInvestRate == -1)
                throw new ArgumentException("Invalid Argument Value", "ReinvestRate");
            if (cashFlow.GetUpperBound(0) + 1 <= 1)
                throw new ArgumentException("Invalid Argument Value", "CashFlow");
            var presentValueFinance = new IntPv().PresentValueInternal(financeRate, cashFlow, -1);
            if (presentValueFinance == 0)
                throw new DivideByZeroException("Cannot Divide By Zero");
            var arrayUpperBound = (short)cashFlow.GetUpperBound(0);
            var presentValueReInvest = new IntPv().PresentValueInternal(reInvestRate, cashFlow, 1);
            var internalReInvestedRate = -presentValueReInvest *
                                         Math.Pow(reInvestRate + 1, arrayUpperBound) /
                                         (presentValueFinance * (financeRate + 1));
            if (internalReInvestedRate < 0)
                throw new ArgumentException("Argument Invalid Value");
            return Math.Pow(internalReInvestedRate, (double)1 / (arrayUpperBound - 1)) - 1;
        }
    }
}
