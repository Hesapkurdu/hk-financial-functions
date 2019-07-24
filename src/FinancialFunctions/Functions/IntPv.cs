namespace FinancialFunctions.Functions
{
    public class IntPv
    {
        /// <summary>
        ///     Returns the present value.
        /// </summary>
        /// <param name="rate">Annual Interest Rate.</param>
        /// <param name="cashFlow">Cash flow.</param>
        /// <param name="paymentDueCollectable">Payment is due or collectable.</param>
        /// <returns></returns>
        public double PresentValueInternal(double rate, double[] cashFlow, short paymentDueCollectable)
        {
            var flagLessThenZero = paymentDueCollectable < 0;
            var flagGreaterThenZero = paymentDueCollectable > 0;
            double auxiliaryValue = 1;
            double internalPresentValue = 0;
            var arrayUpperBound = (short)cashFlow.GetUpperBound(0);
            for (var arrayIterador = 0; arrayIterador <= arrayUpperBound; arrayIterador++)
            {
                var arrayValue = cashFlow[arrayIterador];
                auxiliaryValue += auxiliaryValue * rate;
                if ((!flagLessThenZero || arrayValue <= 0) && (!flagGreaterThenZero || arrayValue >= 0))
                    internalPresentValue += arrayValue / auxiliaryValue;
            }
            return internalPresentValue;
        }
    }
}
