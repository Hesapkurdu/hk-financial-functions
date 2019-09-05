using System.Collections.Generic;
using FinancialFunctions.Domain.Models.Domain;
using FinancialFunctions.Domain.Models.Requests;
using FinancialFunctions.Domain.Models.Responses;

namespace FinancialFunctions.Calculations
{
    public static class PaymentPlan
    {
        public static CalculatePaymentPlanReturnValues CalculatePaymentPlan(CalculatePaymentPlanParameters parameters)
        {
            var totalTaxRatio = TotalTaxRatio.Get(parameters.ProductType);
            var effectiveLoanAmount = EffectiveLoanAmount.Get(parameters.BankCommissionCollectionType,
                                                            parameters.LoanAmount,
                                                            parameters.TotalFee);

            var monthlyInstallment = MonthlyInstallment.Get(new CalculateMonthlyInstallmentParameters
            {
                InterestRateAsPercentage = parameters.InterestRateAsPercentage,
                LoanAmount = parameters.LoanAmount,
                MaturityInMonths = parameters.MaturityInMonths,
                ProductType = parameters.ProductType
            });

            var returnValues = new CalculatePaymentPlanReturnValues
            {
                PaymentPlan = new List<PaymentPlanItem>(),
                TotalInstallment = 0,
                TotalTax = 0,
                MonthlyInstallment = monthlyInstallment.MonthlyInstallment
            };

            var remainingLoanAmount = effectiveLoanAmount;
            for (var i = 0; i < parameters.MaturityInMonths; i++)
            {
                var paymentPlanItem = new PaymentPlanItem
                {
                    Period = i + 1,
                    Installment = returnValues.MonthlyInstallment,
                    Interest = remainingLoanAmount * parameters.InterestRateAsPercentage / 100
                };
                paymentPlanItem.Tax = paymentPlanItem.Interest * totalTaxRatio;
                paymentPlanItem.Principal = paymentPlanItem.Installment - paymentPlanItem.Interest - paymentPlanItem.Tax;
                remainingLoanAmount -= paymentPlanItem.Principal;
                paymentPlanItem.RemainingLoanAmount = remainingLoanAmount;

                returnValues.PaymentPlan.Add(paymentPlanItem);
                returnValues.TotalInterest += paymentPlanItem.Interest;
                returnValues.TotalTax += paymentPlanItem.Tax;
            }

            returnValues.TotalInstallment = returnValues.MonthlyInstallment * parameters.MaturityInMonths;

            returnValues.TotalPayback = TotalPayback.Get(parameters.BankCommissionCollectionType,
                                                        returnValues.MonthlyInstallment,
                                                        parameters.MaturityInMonths,
                                                        parameters.TotalFee);
            return returnValues;
        }
    }
}
