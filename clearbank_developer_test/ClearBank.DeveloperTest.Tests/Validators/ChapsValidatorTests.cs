using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class ChapsValidatorTests
{
    private readonly ChapsValidator _sut;

    public ChapsValidatorTests()
    {
        _sut = new ChapsValidator();
    }

    [Fact]
    public void Given_ChapsValidator_When_AllConditionsAreMet_Then_ReturnTrue()
    {
        var request = new MakePaymentRequest();
        var validAccount = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Live,
            AccountNumber = "genuine-account-number",
            Balance = 0_001M
        };

        var result = _sut.IsValid(request, validAccount);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(AccountStatus.InboundPaymentsOnly)]
    [InlineData(AccountStatus.Disabled)]
    public void Given_ChapsValidator_When_LiveAccountStatusButValidScheme_Then_ReturnTrue(AccountStatus accountStatus)
    {
        var validAccount = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = accountStatus,
            AccountNumber = "genuine-account-number",
            Balance = 0_001M
        };

        var request = new MakePaymentRequest();

        var result = _sut.IsValid(request, validAccount);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs)]
    [InlineData(AllowedPaymentSchemes.FasterPayments)]
    public void Given_ChapsValidator_When_NonChapsSchemeAndLiveAccount_Then_ReturnTrue(AllowedPaymentSchemes paymentSchemes)
    {
        var validAccount = new Account
        {
            AllowedPaymentSchemes = paymentSchemes,
            Status = AccountStatus.Live,
            AccountNumber = "genuine-account-number",
            Balance = 0_001M
        };
        var request = new MakePaymentRequest();

        var result = _sut.IsValid(request, validAccount);

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs, AccountStatus.Disabled)]
    [InlineData(AllowedPaymentSchemes.FasterPayments, AccountStatus.InboundPaymentsOnly)]
    public void Given_ChapsValidator_When_NoConditionMet_Then_ReturnFalse(AllowedPaymentSchemes paymentSchemes, AccountStatus invalidStatus)
    {
        var validAccount = new Account
        {
            AllowedPaymentSchemes = paymentSchemes,
            Status = invalidStatus,
            AccountNumber = "genuine-account-number",
            Balance = 0_001M
        };
        var request = new MakePaymentRequest();

        var result = _sut.IsValid(request, validAccount);

        result.Should().BeFalse();
    }
}