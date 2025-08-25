using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Factories.Interfaces;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Factories;

public class AccountDataStoreFactory : IAccountDataStoreFactory
{
    private readonly AccountDataStore _accountDataStore;
    private readonly BackupAccountDataStore _backupAccountDataStore;
    private readonly IOptionsMonitor<AccountDataStoreFactoryOptions> _optionsMonitor;
    private const string BackupAccount = "Backup";

    public AccountDataStoreFactory(IEnumerable<IAccountDataStore> accountDataStores,
        IOptionsMonitor<AccountDataStoreFactoryOptions> optionsMonitor)
    {
        var dataStores = accountDataStores.ToList();
        _accountDataStore = dataStores.OfType<AccountDataStore>().Single();
        _backupAccountDataStore = dataStores.OfType<BackupAccountDataStore>().Single();
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