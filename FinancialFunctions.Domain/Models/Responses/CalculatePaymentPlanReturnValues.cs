using System.Collections.Generic;
using FinancialFunctions.Domain.Models.Domain;

namespace FinancialFunctions.Domain.Models.Responses
{
    public class CalculatePaymentPlanReturnValues
    {
        public List<PaymentPlanItem> PaymentPlan { get; set; }
        public double MonthlyInstallment { get; set; }
        public double TotalInstallment { get; set; }
        public double TotalInterest { get; set; }
        public double TotalTax { get; set; }
        public double TotalPayback { get; set; }
    }
}
