using ClearBank.DeveloperTest.Data.Factories.Interfaces;
using ClearBank.DeveloperTest.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Data.Factories;

public class AccountDataStoreFactory : IAccountDataStoreFactory
{
    private readonly AccountDataStore _accountDataStore;
    private readonly BackupAccountDataStore _backupAccountDataStore;
    private readonly IOptionsMonitor<AccountDataStoreFactoryOptions> _optionsMonitor;
    private const string BackupAccount = "Backup";

    public AccountDataStoreFactory(AccountDataStore accountDataStore,
        BackupAccountDataStore backupAccountDataStore,
        IOptionsMonitor<AccountDataStoreFactoryOptions> optionsMonitor)
    {
        _accountDataStore = accountDataStore;
        _backupAccountDataStore = backupAccountDataStore;
        _optionsMonitor = optionsMonitor;
    }

    public IAccountDataStore Create()
    {
        IAccountDataStore database = _optionsMonitor.CurrentValue.DataStoreType == BackupAccount
            ? _backupAccountDataStore
            : _accountDataStore;

        return database;
    }
}