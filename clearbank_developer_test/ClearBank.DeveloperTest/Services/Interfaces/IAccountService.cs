using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces;

public interface IAccountService
{
    public Account RetrieveAccount(string accountNumber);

    public void UpdateAccount(Account account);
}