using System;

namespace FinancialFunctions.Functions
{
    class Irr
    {
        /// <summary>
        ///     Returns the internal rate of return for a series of cash flows
        /// </summary>
        /// <param name="cashFlow">cash flow.</param>
        /// <param name="guess">Guess.</param>
        /// <returns></returns>
        public double InternalReturnRate(double[] cashFlow, double guess)
        {
            double auxiliaryValue2;
            double auxiliaryValue1;
            short arrayUpperBound;

            if (guess == 0)
                guess = 0.033;

            try
            {
                arrayUpperBound = (short)cashFlow.GetUpperBound(0);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid Argument Value", "cashFlow");
            }
            if (guess <= -1)
                throw new ArgumentException("Invalid Argument Value", "guess");
            if (arrayUpperBound + 1 <= 1)
                throw new ArgumentException("Invalid Argument Value", "cashFlow");
            if (cashFlow[0] > 0)
                auxiliaryValue1 = cashFlow[0];
            else
                auxiliaryValue1 = -cashFlow[0];
            short numberOfTries = 0;
            while (numberOfTries <= arrayUpperBound)
            {
                if (cashFlow[numberOfTries] > auxiliaryValue1)
                    auxiliaryValue1 = cashFlow[numberOfTries];
                else if (-cashFlow[numberOfTries] > auxiliaryValue1)
                    auxiliaryValue1 = -cashFlow[numberOfTries];
                numberOfTries++;
            }
            var auxiliaryValue3 = auxiliaryValue1 * 1E-07 * 0.01;
            var auxiliaryValue4 = guess;
            var auxiliaryValue5 = new OptPv().OptionalPresentValue(cashFlow, auxiliaryValue4);
            if (auxiliaryValue5 > 0)
                auxiliaryValue2 = auxiliaryValue4 + 1E-05;
            else
                auxiliaryValue2 = auxiliaryValue4 - 1E-05;
            if (auxiliaryValue2 <= -1)
                throw new ArgumentException("Invalid Argument Value");
            var auxiliaryValue6 = new OptPv().OptionalPresentValue(cashFlow, auxiliaryValue2);
            numberOfTries = 0;
            while (true)
            {
                double auxiliaryValue7;
                if (auxiliaryValue6 == auxiliaryValue5)
                {
                    if (auxiliaryValue2 > auxiliaryValue4)
                        auxiliaryValue4 -= 1E-05;
                    else
                        auxiliaryValue4 += 1E-05;
                    auxiliaryValue5 = new OptPv().OptionalPresentValue(cashFlow, auxiliaryValue4);
                    if (auxiliaryValue6 == auxiliaryValue5)
                        throw new ArgumentException("Divide by zero");
                }
                auxiliaryValue4 = auxiliaryValue2 - (auxiliaryValue2 - auxiliaryValue4) * auxiliaryValue6 /
                                  (auxiliaryValue6 - auxiliaryValue5);
                if (auxiliaryValue4 <= -1)
                    auxiliaryValue4 = (auxiliaryValue2 - 1) * 0.5;
                auxiliaryValue5 = new OptPv().OptionalPresentValue(cashFlow, auxiliaryValue4);
                if (auxiliaryValue4 > auxiliaryValue2)
                    auxiliaryValue1 = auxiliaryValue4 - auxiliaryValue2;
                else
                    auxiliaryValue1 = auxiliaryValue2 - auxiliaryValue4;
                if (auxiliaryValue5 > 0)
                    auxiliaryValue7 = auxiliaryValue5;
                else
                    auxiliaryValue7 = -auxiliaryValue5;
                if (auxiliaryValue7 < auxiliaryValue3 && auxiliaryValue1 < 1E-07)
                    return auxiliaryValue4;
                auxiliaryValue1 = auxiliaryValue5;
                auxiliaryValue5 = auxiliaryValue6;
                auxiliaryValue6 = auxiliaryValue1;
                auxiliaryValue1 = auxiliaryValue4;
                auxiliaryValue4 = auxiliaryValue2;
                auxiliaryValue2 = auxiliaryValue1;
                numberOfTries++;
                if (numberOfTries > 50)
                    throw new ArgumentException("Invalid Argument Value");
            }
        }
    }
}
