using FinancialFunctions.Domain.Enums;
using System;

namespace FinancialFunctions.Calculations
{
    public static class TotalPayback
    {
        /// <summary>
        /// returns total payback value
        /// </summary>
        /// <param name="bankCommissionCollectionType"></param>
        /// <param name="monthlyInstallment"></param>
        /// <param name="maturityInMonths"></param>
        /// <param name="totalFee"></param>
        /// <returns></returns>
        public static double Get(BankCommissionCollectionTypes bankCommissionCollectionType,
            double monthlyInstallment, int maturityInMonths, double totalFee)
        {
            switch (bankCommissionCollectionType)
            {
                case BankCommissionCollectionTypes.BankCommissionAmortizedOverTheLifeOfLoan:
                    return monthlyInstallment * maturityInMonths;

                case BankCommissionCollectionTypes.BankCommissionSubtractedFromPrincipalAtFirst:
                    return monthlyInstallment * maturityInMonths + totalFee;

                default:
                    throw new ArgumentException(
                        "Unrecognized BankCommissionCollectionType! Acceptable values are 'BankCommissionAmortizedOverTheLifeOfLoan', 'BankCommissionSubtractedFromPrincipalAtFirst'");
            }
        }
    }
}
