namespace FinancialFunctions.Domain.Models.Domain
{
    public class PaymentPlanItem
    {
        public int Period { get; set; }
        public double Installment { get; set; }
        public double Interest { get; set; }
        public double Tax { get; set; }
        public double Principal { get; set; }
        public double RemainingLoanAmount { get; set; }
    }
}
