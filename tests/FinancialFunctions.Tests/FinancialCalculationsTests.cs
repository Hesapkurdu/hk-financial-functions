using System;
using Excel.FinancialFunctions;
using FinancialFunctions.Enums;
using FluentAssertions;
using Xunit;

namespace FinancialFunctions.Tests
{
    public class FinancialCalculationsTests
    {

        [Theory]
        [InlineData(12, 900, 10000, 0, DueDate.EndOfPeriod)]
        [InlineData(24, 1500, 40000, 0, DueDate.EndOfPeriod)]
        [InlineData(36, 2000, 60000, 0, DueDate.EndOfPeriod)]
        [InlineData(48, 2500, 80000, 0, DueDate.EndOfPeriod)]
        public void RateShouldCalculateCorrectly(short numberPeriods, double payment, double presentValue, double futureValue,
            DueDate due)
        {
            //Act
            var correctSut = Financial.Rate(numberPeriods, -payment, presentValue, futureValue, PaymentDue.EndOfPeriod);
            var sut = FinancialCalculations.Rate(numberPeriods, -payment, presentValue, futureValue, due);

            var isEqual = TestHelper.IsEqualDoubles(sut, correctSut);
            //Assert
            isEqual.Should().BeTrue();
        }

        [Theory]
        [InlineData(1.2, 24, 10000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.3, 24, 20000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.4, 24, 30000, 0, DueDate.EndOfPeriod)]
        public void PaymentShouldCalculateCorrectly(double rate, short numberPeriods, double presentValue, double futureValue,
            DueDate due)
        {
            //Act
            var correctSut = Math.Abs(Financial.Pmt(rate, numberPeriods, presentValue, futureValue, PaymentDue.EndOfPeriod));
            var sut = Math.Abs(FinancialCalculations.Payment(rate, numberPeriods, presentValue, futureValue, due));

            //Assert
            sut.Should().Be(correctSut);
        }

        [Theory]
        [InlineData(1.2, 24, 750, 0, DueDate.EndOfPeriod)]
        [InlineData(1.3, 30, 1000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.4, 36, 1200, 0, DueDate.EndOfPeriod)]
        public void PresentValueShouldCalculateCorrectly(double rate, short numberPeriods, double payment, double futureValue,
            DueDate due)
        {
            //Act
            var correctSut = Financial.Pv(rate, numberPeriods, payment, futureValue, PaymentDue.EndOfPeriod);
            var sut = FinancialCalculations.PresentValue(rate, numberPeriods, payment, futureValue, due);

            var isEqual = TestHelper.IsEqualDoubles(sut, correctSut);
            //Assert
            isEqual.Should().BeTrue();
        }

        [Theory]
        [InlineData(1.2, 40000, 750, 0, DueDate.EndOfPeriod)]
        [InlineData(1.3, 60000, 1000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.4, 80000, 1200, 0, DueDate.EndOfPeriod)]
        public void NumberOfPeriodsShouldCalculateCorrectly(double rate, double presentValue, double payment, double futureValue,
            DueDate due)
        {
            //Act
            var correctSut = Financial.NPer(rate, payment, presentValue, futureValue, PaymentDue.EndOfPeriod);
            var sut = FinancialCalculations.NumberOfPeriods(rate, payment, presentValue, futureValue, due);

            //Assert
            sut.Should().Be((short)correctSut);
        }

        [Theory]
        [InlineData(1.5, 36, 10000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.2, 48, 20000, 0, DueDate.EndOfPeriod)]
        [InlineData(1.3, 24, 30000, 0, DueDate.EndOfPeriod)]
        public void CapitalPaymentShouldCalculateCorrectly(double rate, short numberPeriods, double presentValue, double futureValue,
            DueDate due)
        {
            //Act
            var correctSut = Math.Abs(Financial.CumPrinc(rate, numberPeriods, presentValue, 1, 1, PaymentDue.EndOfPeriod));
            var sut = Math.Abs(FinancialCalculations.CapitalPayment(rate, 1, numberPeriods, presentValue, futureValue, due));

            var isEqual = TestHelper.IsEqualDoubles(sut, correctSut);
            //Assert
            isEqual.Should().BeTrue();
        }


    }
}
