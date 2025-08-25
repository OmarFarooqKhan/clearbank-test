using System;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentSchemeValidatorResolverTests
{
    private readonly PaymentSchemeValidatorResolver _sut;
    public PaymentSchemeValidatorResolverTests()
    {
        _sut = new PaymentSchemeValidatorResolver();
    }

    [Theory]
    [InlineData(PaymentScheme.Bacs, typeof(BacsValidator))]
    [InlineData(PaymentScheme.Chaps, typeof(ChapsValidator))]
    [InlineData(PaymentScheme.FasterPayments, typeof(FasterPaymentsValidator))]
    public void Given_PaymentSchemeValidatorResolver_When_ValidPaymentSchemeSelected_Then_CorrectValidatorChosen(PaymentScheme paymentScheme, Type validatorType)
    { 
        var result =  _sut.RetrievePaymentSchemeValidator(paymentScheme);
        result.Should().BeOfType(validatorType);
    }

    [Fact]
    public void Given_PaymentSchemeValidatorResolver_When_InvalidPaymentSchemeSelected_Then_ArgExceptionRaised()
    {
        var unknownPaymentScheme = (PaymentScheme)int.MaxValue;
        
        Action act = () => _sut.RetrievePaymentSchemeValidator(unknownPaymentScheme);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}