using System;
using ClearBank.DeveloperTest.Data.Factories.Interfaces;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Data.Proxies;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Data.Proxies;

public class AccountDataStoreProxyTests
{
    private readonly AccountDataStoreProxy _sut;
    private readonly Mock<IAccountDataStore> _mockDataStore;

    public AccountDataStoreProxyTests()
    {
        Mock<IAccountDataStoreFactory> mockFactory = new();
        _mockDataStore = new Mock<IAccountDataStore>();
        mockFactory.Setup(mock => mock.Create()).Returns(_mockDataStore.Object);

        _sut = new AccountDataStoreProxy(mockFactory.Object);
    }
    [Fact]
    public void Given_AccountDataStoreProxy_When_GetAccount_ThenAccountIsReturned()
    {
        var accountNumber = Guid.NewGuid().ToString();
        var expectedAccount = new Account { AccountNumber = accountNumber };
        _mockDataStore.Setup(mock => mock.GetAccount(accountNumber)).Returns(expectedAccount);

        var result = _sut.GetAccount(accountNumber);

        _mockDataStore.Verify(mock => mock.GetAccount(accountNumber), Times.Once);
        result.Should().Be(expectedAccount);
    }
    [Fact]
    public void Given_AccountDataStoreProxy_When_UpdateAccount_AccountIsUpdated()
    {
        var accountToUpdate = new Account();

        _sut.UpdateAccount(accountToUpdate);

        _mockDataStore.Verify(mock => mock.UpdateAccount(accountToUpdate), Times.Once);
    }
}