using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Logging;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentSchemeServiceTests
{
    private readonly PaymentSchemeService _sut;
    private readonly Mock<IPaymentSchemeValidatorResolver> _mockResolver;
    private readonly Mock<IAccountValidator> _mockAccountValidator;
    private readonly FakeLogger<PaymentSchemeService> _mockLogger;

    public PaymentSchemeServiceTests()
    {
        _mockResolver = new();
        _mockAccountValidator = new();
        _mockLogger = new();

        _sut = new PaymentSchemeService(_mockResolver.Object, _mockAccountValidator.Object, _mockLogger);
    }

    [Theory]
    [AutoData]
    public void Given_PaymentSchemeService_When_PaymentAndAccountAreValid_Then_IsSuccessfulPaymentReturnsTrue(MakePaymentRequest request, Account account)
    {
        _mockAccountValidator.Setup(mock => mock.IsValidAccount(account)).Returns(true);
        var mockValidator = new Mock<IPaymentSchemeValidator>();
        mockValidator.Setup(mock => mock.IsValidPaymentScheme(request, account)).Returns(true);
        _mockResolver.Setup(mock => mock.RetrievePaymentSchemeValidator(request.PaymentScheme))
            .Returns(mockValidator.Object);

        var result = _sut.IsSuccessfulPayment(request, account);

        result.Should().BeTrue();
        var expectedLogOutcome = new PaymentSchemeResolverLog(request.PaymentScheme, account.AccountNumber, true);
        _mockLogger.Collector.Count.Should().Be(1);
        _mockLogger.LatestRecord.Level.Should().Be(LogLevel.Information);
        _mockLogger.LatestRecord.Message.Should().Be($"PaymentSchemeResolver_Outcome {expectedLogOutcome}");
    }

    [Theory]
    [AutoData]
    public void Given_PaymentSchemeService_When_AccountIsValidButPaymentSchemeIsNot_Then_IsSuccessfulPaymentReturnsFalse(MakePaymentRequest request, Account account)
    {
        _mockAccountValidator.Setup(mock => mock.IsValidAccount(account)).Returns(true);
        var mockValidator = new Mock<IPaymentSchemeValidator>();
        mockValidator.Setup(mock => mock.IsValidPaymentScheme(request, account)).Returns(false);
        _mockResolver.Setup(mock => mock.RetrievePaymentSchemeValidator(request.PaymentScheme))
            .Returns(mockValidator.Object);

        var result = _sut.IsSuccessfulPayment(request, account);

        result.Should().BeFalse();
        var expectedLogOutcome = new PaymentSchemeResolverLog(request.PaymentScheme, account.AccountNumber, false);
        _mockLogger.Collector.Count.Should().Be(1);
        _mockLogger.LatestRecord.Level.Should().Be(LogLevel.Information);
        _mockLogger.LatestRecord.Message.Should().Be($"PaymentSchemeResolver_Outcome {expectedLogOutcome}");
    }

    [Theory]
    [AutoData]
    public void Given_PaymentSchemeService_When_AccountIsInvalid_Then_IsSuccessfulPaymentReturnsFalse(MakePaymentRequest request, Account account)
    {
        _mockAccountValidator.Setup(mock => mock.IsValidAccount(account)).Returns(false);

        var result = _sut.IsSuccessfulPayment(request, account);

        result.Should().BeFalse();
        _mockLogger.Collector.Count.Should().Be(1);
        _mockLogger.LatestRecord.Level.Should().Be(LogLevel.Warning);
        _mockLogger.LatestRecord.Message.Should().Be($"PaymentSchemeResolver_InvalidAccount {request.DebtorAccountNumber}");

    }
}