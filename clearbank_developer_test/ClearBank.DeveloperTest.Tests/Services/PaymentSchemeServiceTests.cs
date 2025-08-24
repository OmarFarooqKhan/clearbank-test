using AutoFixture;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentSchemeServiceTests
{
    private readonly PaymentService _sut;
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly Mock<IPaymentCalculationService> _paymentCalculationService;
    private readonly Mock<IPaymentSchemeService> _paymentSchemeService;
    
    public PaymentSchemeServiceTests()
    {
        _accountServiceMock = new();
        _paymentCalculationService = new();
        _paymentSchemeService = new();
        
        _sut = new PaymentService(_accountServiceMock.Object, _paymentCalculationService.Object, _paymentSchemeService.Object);
    }

    [Fact]
    public void Given_MakePaymentIsCalledWithAppSettings_Then_CorrectDataStoreRetrieved()
    {
        
    }

    [Fact]
    [InlineData(PaymentScheme.Bacs)]
    [InlineData(PaymentScheme.Chaps)]
    [InlineData(PaymentScheme.FasterPayments)]
    public void Given_MakePaymentIsCalled_When_AccountAndRequestPopulatedWithValidScheme_Then_MakePaymentResultIsSuccessful(PaymentScheme paymentScheme)
    {
    }

    [Fact]
    public void Given_MakePaymentIsCalled_When_AccountIsInvalid_Then_MakePaymentResultUnsuccessful()
    {
        
    }

    [Fact]
    public void Given_MakePaymentIsCalled_WhenBacsPaymentSchemeIsUsed_Then_ReturnsCorrectMakePaymentResult()
    {
        
    }
    
    [Fact]
    public void Given_MakePaymentIsCalled_WhenChapsPaymentSchemeIsUsed_Then_ReturnsCorrectMakePaymentResult()
    {
        
    }
    
    [Fact]
    public void Given_MakePaymentIsCalled_WhenFasterPaymentsPaymentSchemeIsUsed_Then_ReturnsCorrectMakePaymentResult()
    {
        
    }
    
    [Fact]
    public void Given_MakePaymentIsCalled_WhenUsingValidPaymentScheme_Then_DeductAndReturnCorrectMakePaymentResult()
    {
        
    }
}