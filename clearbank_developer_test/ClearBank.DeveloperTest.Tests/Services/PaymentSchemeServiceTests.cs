using AutoFixture.Xunit2;
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
    private readonly Mock<IPaymentCalculationService> _paymentCalculationServiceMock;
    private readonly Mock<IPaymentSchemeService> _paymentSchemeServiceMock;
    
    public PaymentSchemeServiceTests()
    {
        _accountServiceMock = new();
        _paymentCalculationServiceMock = new();
        _paymentSchemeServiceMock = new();
        
        _sut = new PaymentService(_accountServiceMock.Object, _paymentCalculationServiceMock.Object, _paymentSchemeServiceMock.Object);
    }

    [Theory]
    [AutoData]
    public void Given_MakePaymentIsCalled_When_PaymentSchemeValidates_MakePaymentResultIsSuccessful(MakePaymentRequest request)
    {
        var accountRetrieved = new Account();
        _accountServiceMock
            .Setup(mock => mock.RetrieveAccount(request.DebtorAccountNumber))
            .Returns(accountRetrieved);
        _paymentSchemeServiceMock.Setup(mock => mock.IsSuccessfulPayment(request, accountRetrieved))
            .Returns(true);

       var outcome =  _sut.MakePayment(request);
       
       _paymentCalculationServiceMock.Verify(calls => calls.ProcessDeductions(accountRetrieved, request.Amount), Times.Once);
       outcome.Success.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void Given_MakePaymentIsCalled_When_UnsuccessfulPaymentSchemeValidation_Then_MakePaymentResultUnsuccessful(MakePaymentRequest request)
    {
        var accountRetrieved = new Account();
        _accountServiceMock
            .Setup(mock => mock.RetrieveAccount(request.DebtorAccountNumber))
            .Returns(accountRetrieved);
        _paymentSchemeServiceMock.Setup(mock => mock.IsSuccessfulPayment(request, accountRetrieved))
            .Returns(false);

        var outcome =  _sut.MakePayment(request);
        _paymentCalculationServiceMock.VerifyNoOtherCalls();
        outcome.Success.Should().BeFalse();
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