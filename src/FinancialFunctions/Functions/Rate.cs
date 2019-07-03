using System;
using FinancialFunctions.Enums;

namespace FinancialFunctions.Functions
{
    class Rate
    {
        /// <summary>
        ///     Returns the interest rate per period of an annuity
        ///     (Rate)
        /// </summary>
        /// <param name="numberPeriods">Number of periods.(nper)</param>
        /// <param name="payment">Payment.(pmt)</param>
        /// <param name="presentValue">Present (principal) value.(pv)</param>
        /// <param name="futureValue">Future value.(fv)</param>
        /// <param name="due">Specifies when values are Due, at the beginning or at the end of the period.</param>
        /// <param name="guess">Guess (optional parameter).</param>
        /// <returns></returns>
        public double CalculateRate(short numberPeriods, double payment, double presentValue, double futureValue, DueDate due,
            double guess = 0)
        {
            double auxiliaryRate1;

            if (guess == 0)
                guess = 0.033;

            if (numberPeriods <= 0)
                throw new ArgumentException("numberPeriods must be greater than Zero", "numberPeriods");
            var precision = guess;
            var auxiliaryRate3 = new EvalRate().EvaluateRate(precision, numberPeriods, payment, presentValue, futureValue, due);
            if (auxiliaryRate3 > 0)
                auxiliaryRate1 = precision / 2;
            else
                auxiliaryRate1 = precision * 2;
            var auxiliaryRate2 = new EvalRate().EvaluateRate(auxiliaryRate1, numberPeriods, payment, presentValue, futureValue, due);
            var numberofTries = 0;
            while (true)
            {
                if (auxiliaryRate2 == auxiliaryRate3)
                {
                    if (auxiliaryRate1 > precision)
                        precision -= 1E-05;
                    else
                        precision -= -1E-05;
                    auxiliaryRate3 = new EvalRate().EvaluateRate(precision, numberPeriods, payment, presentValue, futureValue, due);
                    if (auxiliaryRate2 == auxiliaryRate3)
                    {
                        var parameters =
                            string.Format(
                                "   Parameters:: short numberPeriods: {0}, double payment: {1}, double presentValue: {2}, double futureValue: {3}",
                                numberPeriods, payment, presentValue, futureValue);
                        throw new ArgumentException("Cannot Calculate Divide By Zero!" + parameters);
                    }
                }

                precision = auxiliaryRate1 -
                            (auxiliaryRate1 - precision) * auxiliaryRate2 / (auxiliaryRate2 - auxiliaryRate3);
                auxiliaryRate3 = new EvalRate().EvaluateRate(precision, numberPeriods, payment, presentValue, futureValue, due);
                if (Math.Abs(auxiliaryRate3) < 1E-07)
                    return precision;
                var auxiliaryRate4 = auxiliaryRate3;
                auxiliaryRate3 = auxiliaryRate2;
                auxiliaryRate2 = auxiliaryRate4;
                auxiliaryRate4 = precision;
                precision = auxiliaryRate1;
                auxiliaryRate1 = auxiliaryRate4;
                numberofTries++;

                if (numberofTries > 5000)
                {
                    var parameters =
                        string.Format(
                            "   Parameters:: short numberPeriods: {0}, double payment: {1}, double presentValue: {2}, double futureValue: {3}",
                            numberPeriods, payment, presentValue, futureValue);
                    throw new ArgumentException("Cannot Calculate Rate!" + parameters);
                }
            }
        }
    }
}
