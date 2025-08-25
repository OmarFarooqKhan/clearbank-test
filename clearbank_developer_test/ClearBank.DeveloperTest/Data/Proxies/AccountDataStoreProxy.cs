using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Data.Factories.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data.Proxies;

public class AccountDataStoreProxy : IAccountDataStore
{
    private readonly IAccountDataStoreFactory _accountDataStoreFactory;

    public AccountDataStoreProxy(IAccountDataStoreFactory accountDataStoreFactory)
    {
        _accountDataStoreFactory = accountDataStoreFactory;
    }
    public Account GetAccount(string accountNumber)
    {
        return _accountDataStoreFactory.Create().GetAccount(accountNumber);
    }

    public void UpdateAccount(Account account)
    {
        _accountDataStoreFactory.Create().UpdateAccount(account);
    }
}