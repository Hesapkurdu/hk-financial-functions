using FinancialFunctions.Domain.Enums;
using FinancialFunctions.Functions;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialFunctions.Tests
{
    public class FunctionsTests
    {

        [Theory]
        [InlineData(1.4, 48, 60000, 120000, DueDate.EndOfPeriod)]
        public void FvShouldCalculatedWithoutError(double rate, short numberPeriods, double payment, double presentValue, DueDate due)
        {
            //Arrange
            var fv = new Fv();

            //Act
            var exception = Record.Exception(() => fv.FutureValue(rate, numberPeriods, payment, presentValue, due));

            //Assert
            exception.Should().BeNull();
        }


        [Theory]
        [InlineData(1.4, 48, 60000, 120000, DueDate.EndOfPeriod)]
        public void IntFvShouldCalculatedWithoutError(double rate, short numberPeriods, double payment,
            double presentValue, DueDate due)
        {
            //Arrange
            var intFv = new IntFv();

            //Act
            var exception = Record.Exception(() => intFv.FutureValueInternal(rate, numberPeriods, payment, presentValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, new[] { 1.2, 2 }, 1)]
        public void IntPvShouldCalculatedWithoutError(double rate, double[] cashFlow, short paymentDueCollectable)
        {
            //Arrange
            var intPv = new IntPv();

            //Act
            var exception = Record.Exception(() => intPv.PresentValueInternal(rate, cashFlow, paymentDueCollectable));

            //Assert
            exception.Should().BeNull();
        }


        [Theory]
        [InlineData(1.4, 24, 48, 100000, 0, DueDate.EndOfPeriod)]
        public void IPmtShouldCalculatedWithoutError(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            //Arrange
            var iPmt = new IPmt();

            //Act
            var exception = Record.Exception(() => iPmt.InterestPayment(rate, period, numberPeriods, presentValue, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, 50000, 60000, 0, DueDate.EndOfPeriod)]
        public void NPerShouldCalculatedWithoutError(double rate, double payment, double presentValue, double futureValue, DueDate due)
        {
            //Arrange 
            var nper = new NPer();

            //Act
            var exception = Record.Exception(() => nper.NumberOfPeriods(rate, payment, presentValue, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, new[] { 1.2, 2 })]
        public void NpvShouldCalculatedWithoutError(double rate, double[] cashFlow)
        {
            //Arrange 
            var npv = new Npv();

            //Act
            var exception = Record.Exception(() => npv.NetPresentValue(rate, cashFlow));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, new[] { 1.2, 2 })]
        public void OptPvShouldCalculatedWithoutError(double guess, double[] cashFlow)
        {
            //Arrange 
            var opv = new OptPv();

            //Act
            var exception = Record.Exception(() => opv.OptionalPresentValue(cashFlow, guess));

            //Assert
            exception.Should().BeNull();
        }


        [Theory]
        [InlineData(1.4, 48, 60000, 0, DueDate.EndOfPeriod)]
        public void PmtShouldCalculatedWithoutError(double rate, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            //Arrange 
            var pmt = new Pmt();

            //Act
            var exception = Record.Exception(() => pmt.PaymentInternal(rate, numberPeriods, presentValue, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, 48, 60, 60000, 0, DueDate.EndOfPeriod)]
        public void PPmtShouldCalculatedWithoutError(double rate, short period, short numberPeriods, double presentValue, double futureValue, DueDate due)
        {
            //Arrange 
            var ppmt = new PPmt();

            //Act
            var exception = Record.Exception(() => ppmt.CapitalPayment(rate, period, numberPeriods, presentValue, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(1.4, 48, 60000, 0, DueDate.EndOfPeriod)]
        public void PvShouldCalculatedWithoutError(double rate, short numberPeriods, double payment, double futureValue, DueDate due)
        {
            //Arrange 
            var pv = new Pv();

            //Act
            var exception = Record.Exception(() => pv.PresentValue(rate, numberPeriods, payment, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(36, -2000, 60000, 0, DueDate.EndOfPeriod)]
        public void RateShouldCalculatedWithoutError(short numberPeriods, double payment, double presentValue, double futureValue, DueDate due, double guess = 0)
        {
            //Arrange 
            var rate = new Rate();

            //Act
            var exception = Record.Exception(() => rate.CalculateRate(numberPeriods, payment, presentValue, futureValue, due, guess));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(new[] { 1.2, 2 }, 1.4, 1.4)]
        public void ReIrrShouldThrowDivideByZeroException(double[] cashFlow, double financeRate, double reInvestRate)
        {
            //Arrange 
            var reIrr = new ReIrr();

            //Act
            var exception = Record.Exception(() => reIrr.ReinvestedInternalReturnRate(cashFlow, financeRate, reInvestRate));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(DivideByZeroException));
        }

        [Theory]
        [InlineData(new[] { -4.8, -20 }, 2.4, 1.8)]
        public void ReIrrShouldCalculatedWithoutError(double[] cashFlow, double financeRate, double reInvestRate)
        {
            //Arrange 
            var reIrr = new ReIrr();

            //Act
            var exception = Record.Exception(() => reIrr.ReinvestedInternalReturnRate(cashFlow, financeRate, reInvestRate));

            //Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(new[] { 1.2, 2 }, 1.4)]
        public void IrrShouldThrowDivideByZeroException(double[] cashFlow, double guess)
        {
            //Arrange 
            var irr = new Irr();

            //Act
            var exception = Record.Exception(() => irr.InternalReturnRate(cashFlow, guess));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentException));
        }

        [Theory]
        [InlineData(1.4, 48, 50000, 60000, 0, DueDate.EndOfPeriod)]
        public void EvalRateShouldCalculatedWithoutError(double rate, short numberPeriods, double payment, double presentValue, double futureValue, DueDate due)
        {
            //Arrange 
            var er = new EvalRate();

            //Act
            var exception = Record.Exception(() => er.EvaluateRate(rate, numberPeriods, payment, presentValue, futureValue, due));

            //Assert
            exception.Should().BeNull();
        }
    }
}
