using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Options;

namespace ClearBank.DeveloperTest.Data;

public class AccountDataStoreFactory : IAccountDataStore
{
    private readonly AccountDataStore _accountDataStore;
    private readonly BackupAccountDataStore _backupAccountDataStore;
    private readonly IOptionsMonitor<AccountManagementOptions> _optionsMonitor;
    private const string BackupAccount = "Backup";

    public AccountDataStoreFactory(AccountDataStore accountDataStore,
        BackupAccountDataStore backupAccountDataStore,
        IOptionsMonitor<AccountManagementOptions> optionsMonitor)
    {
        _accountDataStore = accountDataStore;
        _backupAccountDataStore = backupAccountDataStore;
        _optionsMonitor = optionsMonitor;
    }

    private IAccountDataStore Create()
    {
        IAccountDataStore database = _optionsMonitor.CurrentValue.DataStoreType == BackupAccount
            ? _backupAccountDataStore
            : _accountDataStore;
        
        return database;
    }

    public Account GetAccount(string accountNumber)
    {
        return Create().GetAccount(accountNumber);
    }

    public void UpdateAccount(Account account)
    { 
        Create().UpdateAccount(account);
    }
}