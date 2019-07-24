using System;

namespace FinancialFunctions.Tests.Base
{
    static class TestHelper
    {
        public static bool IsEqualResultsWithExcelFinancialFunctions(double? value1, double? value2, double unimportantDifference = 0.0000001)
        {
            if (!Equals(value1, value2))
            {
                if (value1 == null || value2 == null)
                    return false;

                return Math.Abs(value1.Value - value2.Value) < unimportantDifference;
            }

            return true;
        }
    }
}
