using System;
using Excel.FinancialFunctions;
using FinancialFunctions.Domain.Enums;
using FinancialFunctions.Tests.Base;
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

            var isEqual = TestHelper.IsEqualResultsWithExcelFinancialFunctions(sut, correctSut);
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

            var isEqual = TestHelper.IsEqualResultsWithExcelFinancialFunctions(sut, correctSut);
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

            var isEqual = TestHelper.IsEqualResultsWithExcelFinancialFunctions(sut, correctSut);
            //Assert
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void EvaluateRateShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.EvaluateRate(1.4, 48, 60000, 120000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }


        [Fact]
        public void FutureValueShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.FutureValue(1.4, 48, 60000, 120000, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void FutureValueInternalShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.FutureValueInternal(1.4, 48, 60000, 120000, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void PresentValueInternalShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.InternalPresentValue(1.4, new[] { 1.2, 2 }, 1));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void InterestPaymentShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.InterestPayment(1.4, 24, 48, 100000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }


        [Fact]
        public void NumberOfPeriodsShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.NumberOfPeriods(1.4, 50000, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void NetPresentValueShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.NetPresentValue(1.4, new[] { 1.2, 2 }));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void NetPresentValueShouldThrowExceptionWithNullParameters()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.NetPresentValue(1.4, null));

            //Assert
            exception.Should().NotBeNull();
        }

        [Fact]
        public void OptionalPresentValueShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.PresentValueOptional(new[] { 1.2, 2 }, 1.4));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void PaymentInternalShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.Payment(1.4, 48, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void CapitalPaymentShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.CapitalPayment(1.4, 48, 60, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void CapitalPaymentShouldThrowExceptionWhenPeriodIsLessOrEqualToZero()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.CapitalPayment(1.2, 0, 60, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().NotBeNull();
        }


        [Fact]
        public void PresentValueShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.PresentValue(1.4, 48, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void CalculateRateShouldCalculatedWithoutError()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.Rate(36, -2000, 60000, 0, DueDate.EndOfPeriod));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void ReinvestedInternalReturnRateShouldCalculatedWithoutError()
        {
            //ActSSS
            var exception = Record.Exception(() => FinancialCalculations.ReinvestedInternalReturnRate(new[] { -4.8, -20 }, 2.4, 1.8));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void ReinvestedInternalReturnRateShouldThrowExceptionWhenFinancialRateIsNegative()
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.ReinvestedInternalReturnRate(new[] { 1.2, 2 }, -1, 1.8));

            //Assert
            exception.Should().NotBeNull();
        }

        [Theory]
        [InlineData(new[] { 1.2, 2 }, 1.4)]
        public void InternalReturnRateShouldThrowDivideByZeroException(double[] cashFlow, double guess)
        {
            //Act
            var exception = Record.Exception(() => FinancialCalculations.InternalReturnRate(cashFlow, guess));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentException));
        }
    }
}
