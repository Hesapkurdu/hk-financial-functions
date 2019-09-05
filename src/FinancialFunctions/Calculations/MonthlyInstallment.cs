using FinancialFunctions.Domain.Enums;
using FinancialFunctions.Domain.Models.Requests;
using FinancialFunctions.Domain.Models.Responses;
using FinancialFunctions.Functions;

namespace FinancialFunctions.Calculations
{
    public static class MonthlyInstallment
    {
        public static CalculateMonthlyInstallmentReturnValues Get(CalculateMonthlyInstallmentParameters parameters)
        {
            var totalTaxRatio = TotalTaxRatio.Get(parameters.ProductType);
            if (parameters.MaturityInMonths == 0)
                return new CalculateMonthlyInstallmentReturnValues();

            return new CalculateMonthlyInstallmentReturnValues
            {
                MonthlyInstallment = -new Pmt().PaymentInternal(parameters.InterestRateAsPercentage / 100 * (1 + totalTaxRatio),
                    parameters.MaturityInMonths,
                    parameters.LoanAmount,
                    0,
                    DueDate.EndOfPeriod)
            };
        }
    }
}
