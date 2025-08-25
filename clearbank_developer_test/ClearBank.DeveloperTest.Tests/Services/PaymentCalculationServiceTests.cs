using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentCalculationServiceTests
{
    private readonly Mock<IAccountService> _mockAccountService;
    private readonly PaymentCalculationService _sut;

    public PaymentCalculationServiceTests()
    {
        _mockAccountService = new();
        _sut = new PaymentCalculationService(_mockAccountService.Object);
    }

    [Theory]
    [AutoData]
    public void Given_PaymentCalculationService_When_DeductionToBeMade_ThenDeductionIsProcessed(Account account, decimal amountToDeduct)
    {
        _sut.ProcessDeductions(account, amountToDeduct);

        _mockAccountService.Verify(mock => mock.UpdateAccount(account), Times.Once);
    }
}