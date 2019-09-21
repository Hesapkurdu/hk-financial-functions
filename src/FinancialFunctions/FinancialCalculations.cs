using FinancialFunctions.Domain.Enums;
using FinancialFunctions.Functions;

namespace FinancialFunctions
{
    /// <summary>
    ///     Extension class for financial functions
    /// </summary>
    public static class FinancialCalculations
    {
        public static double FutureValue(double rate, short numberPeriods, double payment, double presentValue, DueDate due)
        {
            return new Fv().FutureValue(rate, numberPeriods, payment, presentValue, due);
        }

        public static double FutureValueInternal(double rate, short numberPeriods, double payment,
            double presentValue, DueDate due)
        {
            return new IntFv().FutureValueInternal(rate, numberPeriods, payment, presentValue, due);
        }

        public static double InterestPayment(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            return new IPmt().InterestPayment(rate, period, numberPeriods, presentValue, futureValue, due);
        }

        public static double InternalReturnRate(double[] cashFlow, double guess)
        {
            return new Irr().InternalReturnRate(cashFlow, guess);
        }

        public static double InternalPresentValue(double rate, double[] cashFlow, short paymentDueCollectable)
        {
            return new IntPv().PresentValueInternal(rate, cashFlow, paymentDueCollectable);
        }

        public static double EvaluateRate(double rate, short numberPeriods, double payment, double presentValue, double futureValue, DueDate due)
        {
            return new EvalRate().EvaluateRate(rate, numberPeriods, payment, presentValue, futureValue, due);
        }

        public static double ReinvestedInternalReturnRate(double[] cashFlow, double financeRate,
            double reInvestRate)
        {
            return new ReIrr().ReinvestedInternalReturnRate(cashFlow, financeRate, reInvestRate);
        }

        public static short NumberOfPeriods(double rate, double payment, double presentValue, double futureValue, DueDate due)
        {
            return new NPer().NumberOfPeriods(rate, payment, presentValue, futureValue, due);
        }

        public static double NetPresentValue(double rate, double[] cashFlow)
        {
            return new Npv().NetPresentValue(rate, cashFlow);
        }

        public static double PresentValueOptional(double[] cashFlow, double guess)
        {
            return new OptPv().OptionalPresentValue(cashFlow, guess);
        }

        public static double Payment(double rate, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            return new Pmt().PaymentInternal(rate, numberPeriods, presentValue, futureValue, due);
        }

        public static double CapitalPayment(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            return new PPmt().CapitalPayment(rate, period, numberPeriods, presentValue, futureValue, due);
        }

        public static double PresentValue(double rate, short numberPeriods, double payment, double futureValue, DueDate due)
        {
            return new Pv().PresentValue(rate, numberPeriods, payment, futureValue, due);
        }

        public static double Rate(short numberPeriods, double payment, double presentValue, double futureValue, DueDate due, double guess = 0)
        {
            return new Rate().CalculateRate(numberPeriods, payment, presentValue, futureValue, due, guess);
        }
    }
}
