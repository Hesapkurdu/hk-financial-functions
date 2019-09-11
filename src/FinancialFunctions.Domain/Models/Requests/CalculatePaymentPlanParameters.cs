using FinancialFunctions.Domain.Enums;

namespace FinancialFunctions.Domain.Models.Requests
{
    public class CalculatePaymentPlanParameters
    {
        public ProductType ProductType { get; set; }
        public BankCommissionCollectionTypes BankCommissionCollectionType { get; set; }
        public double InterestRateAsPercentage { get; set; }
        public short MaturityInMonths { get; set; }
        public double LoanAmount { get; set; }
        public double TotalFee { get; set; }
        public string CallMethod { get; set; }
    }
}
