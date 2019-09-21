using System;
using FinancialFunctions.Domain.Enums;

namespace FinancialFunctions.Calculations
{
    public static class TotalTaxRatio
    {
        public static double Get(ProductType productType)
        {
            switch (productType) {
                case ProductType.PersonalLoan:
                case ProductType.SecuredLoanOtherProperty:
                case ProductType.SmeLoan:
                case ProductType.AutoLoan:
                    return (Convert.ToDouble(15) + Convert.ToDouble(5)) / 100;

                case ProductType.SecuredLoanOwnProperty:
                    return Convert.ToDouble(15) / 100;

                case ProductType.MortgageLoan:
                    return 0;

                default:
                    throw new ArgumentException("Unrecognized ProductType! Acceptable values are 'PersonalLoan', 'SecuredLoanOtherProperty', 'SecuredLoanOwnProperty', 'Mortgage', 'SmeLoan', 'AutoLoan'");
            }
        }
    }
}
