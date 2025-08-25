using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Factories;

public class AccountDataStoreFactoryTests
{
    private readonly AccountDataStoreFactory _sut;
    private readonly Mock<AccountDataStore> _mockAccountDataStore;
    private readonly Mock<BackupAccountDataStore> _mockBackupAccountDataStore;
    private readonly Mock<IOptionsMonitor<AccountDataStoreFactoryOptions>> _mockOptions;
    private const string Backup = "Backup";

    public AccountDataStoreFactoryTests()
    {
        _mockAccountDataStore = new();
        _mockBackupAccountDataStore = new();
        _mockOptions = new();

        _sut = new AccountDataStoreFactory(
            _mockAccountDataStore.Object, _mockBackupAccountDataStore.Object,
            _mockOptions.Object);
    }

    [Fact]
    public void Given_AccountDataStoreFactory_When_CreateAndDataStoreTypeIsNotBackup_ThenAccountDataStoreReturned()
    {
        _mockOptions.SetupGet(mock => mock.CurrentValue).Returns(new AccountDataStoreFactoryOptions("Something"));
        
        var result = _sut.Create();

        result.Should().Be(_mockAccountDataStore.Object);
    }
    [Fact]
    public void Given_AccountDataStoreFactory_When_CreateAndDataStoreTypeIsBackup_ThenBackupAccountDataStoreReturned()
    {
        _mockOptions.SetupGet(mock => mock.CurrentValue).Returns(new AccountDataStoreFactoryOptions(Backup));
        
        var result = _sut.Create();

        result.Should().Be(_mockBackupAccountDataStore.Object);
    }
}