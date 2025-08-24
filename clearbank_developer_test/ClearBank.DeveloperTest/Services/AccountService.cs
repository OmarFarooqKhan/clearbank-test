using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Logging;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Logging;

namespace ClearBank.DeveloperTest.Services;

public class AccountService : IAccountService
{
    private readonly IAccountDataStore _accountDataStore;
    private readonly ILogger<AccountService> _logger;
    
    public AccountService(IAccountDataStore accountDataStore, ILogger<AccountService> logger)
    {
        _accountDataStore = accountDataStore;
        _logger = logger;
    }
    public Account RetrieveAccount(string accountNumber)
    {
        var account = _accountDataStore.GetAccount(accountNumber);
        
        if (account is not null)
        { 
            _logger.LogInformation(LogEventNames.AccountServiceSuccess); return account;
        }
        
        _logger.LogWarning(LogEventNames.AccountServiceUnableToFindAccount);
        return null;
    }

    public void UpdateAccount(Account account)
    {
        _logger.LogInformation("{EventName} {AccountNumber}", LogEventNames.AccountServiceUpdatingAccount,
            account.AccountNumber);
        
        _accountDataStore.UpdateAccount(account);
    }
}