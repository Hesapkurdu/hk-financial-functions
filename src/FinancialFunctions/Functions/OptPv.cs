namespace FinancialFunctions.Functions
{
    public class OptPv
    {
        /// <summary>
        ///     present value.
        /// </summary>
        /// <param name="cashFlow">Cash flow.</param>
        /// <param name="guess">Guess.</param>
        /// <returns></returns>
        public double OptionalPresentValue(double[] cashFlow, double guess)
        {
            short iterator = 0;
            var arrayUpperBound = (short)cashFlow.GetUpperBound(0);
            double presentValue = 0;
            var precision = 1 + guess;
            while (iterator <= arrayUpperBound && cashFlow[iterator] == 0)
                iterator++;
            for (var i = arrayUpperBound; i >= iterator; i += -1)
            {
                presentValue /= precision;
                presentValue += cashFlow[i];
            }
            return presentValue;
        }
    }
}
