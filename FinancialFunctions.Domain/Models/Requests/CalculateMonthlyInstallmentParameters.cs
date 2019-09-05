using FinancialFunctions.Domain.Enums;

namespace FinancialFunctions.Domain.Models.Requests
{
    public class CalculateMonthlyInstallmentParameters
    {
        public ProductType ProductType { get; set; }
        public double LoanAmount { get; set; }
        public double InterestRateAsPercentage { get; set; }
        public short MaturityInMonths { get; set; }
    }
}
