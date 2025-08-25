using System;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;

public class AccountServiceTests
{
    private readonly AccountService _sut;
    private readonly Mock<IAccountDataStore> _accountDataStoreMock;
    private readonly FakeLogger<AccountService> _loggerMock;

    public AccountServiceTests()
    {
        _accountDataStoreMock = new();
        _loggerMock = new();
        
        _sut = new AccountService(_accountDataStoreMock.Object, _loggerMock);
    }

    [Fact]
    public void Given_AccountNumberToRetrieveAccount_When_RetrieveAccountCalled_ThenAccountRetrieved()
    {
        var accountId = Guid.NewGuid().ToString();
        var retrievedAccount = new Account();
        _accountDataStoreMock.Setup(mock => mock.GetAccount(accountId))
            .Returns(retrievedAccount);
        
       var result=  _sut.RetrieveAccount(accountId);
       
       result.Should().Be(retrievedAccount);
       _loggerMock.Collector.Count.Should().Be(1);
       var record = _loggerMock.Collector.LatestRecord;
       record.Level.Should().Be(LogLevel.Information);
       record.Message.Should().Be($"AccountService_Success {accountId}");
    }
    
    [Fact]
    public void Given_AccountNumberToRetrieveAccount_When_RetrieveAccountCalledButNoAccountFound_ThenReturnNull()
    {
        var accountId = Guid.NewGuid().ToString();
        _accountDataStoreMock.Setup(mock => mock.GetAccount(accountId))
            .Returns((Account)null);
        
        var result=  _sut.RetrieveAccount(accountId);
        
        result.Should().Be(null);
        _loggerMock.Collector.Count.Should().Be(1);
        var record = _loggerMock.Collector.LatestRecord;
        record.Level.Should().Be(LogLevel.Warning);
        record.Message.Should().Be($"AccountService_FailedToFindAccount {accountId}");
    }

    [Fact]
    public void Given_AccountToUpdate_When_UpdateAccountCalled_Then_AccountIsUpdated()
    {
        var accountToUpdate = new Account { AccountNumber = "new account"} ;

        _sut.UpdateAccount(accountToUpdate);
        
        _accountDataStoreMock.Verify(mock => mock.UpdateAccount(accountToUpdate), Times.Once);
        _loggerMock.Collector.Count.Should().Be(1);
        var record = _loggerMock.Collector.LatestRecord;
        record.Level.Should().Be(LogLevel.Information);
        record.Message.Should().Be($"AccountService_UpdatingAccount new account");
    }
}