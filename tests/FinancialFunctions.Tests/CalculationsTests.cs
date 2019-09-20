using System;
using FinancialFunctions.Calculations;
using FinancialFunctions.Domain.Enums;
using FinancialFunctions.Domain.Models.Requests;
using FluentAssertions;
using Xunit;

namespace FinancialFunctions.Tests
{
    public class CalculationsTests
    {
        [Fact]
        public void WhenBankComissionTypeIsZero_GetEffectiveLoanAmountShouldThrowArgumentException()
        {
            //Act
            var exception = Record.Exception(() => EffectiveLoanAmount.Get(0, 10000, 2000));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentException));
        }

        [Fact]
        public void GetEffectiveLoanAmountShouldWorkCorrectly()
        {
            //Act
            var exception = Record.Exception(() => EffectiveLoanAmount.Get(BankCommissionCollectionTypes.BankCommissionAmortizedOverTheLifeOfLoan, 10000, 2000));

            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void WhenMaturityIsZero_GetMonthlyInstallmentShouldReturnEmptyReturnDto()
        {
            //Arrange 
            var parameters = new CalculateMonthlyInstallmentParameters
            {
                MaturityInMonths = 0,
                InterestRateAsPercentage = 1.5,
                LoanAmount = 48000,
                ProductType = ProductType.PersonalLoan
            };

            //Act
            var sut = MonthlyInstallment.Get(parameters);

            //Assert
            sut.MonthlyInstallment.Should().Be(0);
        }

        [Fact]
        public void GetMonthlyInstallmentShouldWorkCorrectly()
        {
            //Arrange 
            var parameters = new CalculateMonthlyInstallmentParameters
            {
                MaturityInMonths = 36,
                InterestRateAsPercentage = 1.5,
                LoanAmount = 48000,
                ProductType = ProductType.PersonalLoan
            };

            //Act
            var sut = MonthlyInstallment.Get(parameters);

            //Assert
            sut.MonthlyInstallment.Should().NotBe(0);
        }

        [Fact]
        public void CalculatePaymentPlanShouldWorkCorrectly()
        {
            //Arrange
            var @object = new CalculatePaymentPlanParameters
            {
                LoanAmount = 60000,
                InterestRateAsPercentage = 1.2,
                ProductType = ProductType.PersonalLoan,
                MaturityInMonths = 48,
                BankCommissionCollectionType = BankCommissionCollectionTypes.BankCommissionSubtractedFromPrincipalAtFirst
            };

            //Act
            var sut = PaymentPlan.CalculatePaymentPlan(@object);

            //Assert
            sut.PaymentPlan.Should().NotBeEmpty();
        }

        
        [Fact]
        public void WhenBankComissionTypeIsZero_GetTotalPaybackShouldThrowArgumentException()
        {
            //Act
            var exception = Record.Exception(() => TotalPayback.Get(0, 2000, 48,10000));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentException));
        }

        [Fact]
        public void GetTotalPaybackShouldWorkCorrectly()
        {
            //Act
            var exception = Record.Exception(() => TotalPayback.Get(BankCommissionCollectionTypes.BankCommissionAmortizedOverTheLifeOfLoan, 2000, 48,10000));

            //Assert
            exception.Should().BeNull();
        }


        [Fact]
        public void WhenProductTypeIsZero_GetTotalTaxRatioShouldThrowArgumentException()
        {
            //Act
            var exception = Record.Exception(() => TotalTaxRatio.Get(0));

            //Assert
            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentException));
        }

        [Theory]
        [InlineData(ProductType.AutoLoan)]
        [InlineData(ProductType.PersonalLoan)]
        [InlineData(ProductType.SecuredLoanOtherProperty)]
        [InlineData(ProductType.SmeLoan)]
        public void GetTotalTaxRatioShouldWorkCorrectly(ProductType type)
        {
            //Act
            var sut = TotalTaxRatio.Get(type);

            //Assert
            sut.Should().NotBe(0);
        }

        [Theory]
        [InlineData(ProductType.MortgageLoan)]
        public void GetTotalTaxRatioShouldWorkCorrectlyForMortgageLoan(ProductType type)
        {
            //Act
            var sut = TotalTaxRatio.Get(type);

            //Assert
            sut.Should().Be(0);
        }


    }
}
