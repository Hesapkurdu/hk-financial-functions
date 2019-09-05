using FinancialFunctions.Domain.Enums;
using System;

namespace FinancialFunctions.Calculations
{
    public static class EffectiveLoanAmount
    {
        public static double Get(BankCommissionCollectionTypes bankCommissionCollectionType,
            double loanAmount, double totalFee)
        {
            switch (bankCommissionCollectionType)
            {
                case BankCommissionCollectionTypes.BankCommissionAmortizedOverTheLifeOfLoan:
                    return loanAmount + totalFee;

                case BankCommissionCollectionTypes.BankCommissionSubtractedFromPrincipalAtFirst:
                    return loanAmount;

                default:
                    throw new ArgumentException(
                        "Unrecognized BankCommissionCollectionType! Acceptable values are 'BankCommissionAmortizedOverTheLifeOfLoan', 'BankCommissionSubtractedFromPrincipalAtFirst'");
            }
        }
    }
}
