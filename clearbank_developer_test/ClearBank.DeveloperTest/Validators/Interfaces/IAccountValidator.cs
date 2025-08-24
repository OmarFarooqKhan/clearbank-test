using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Interfaces;

public interface IAccountValidator
{
    bool IsValidAccount(Account account);
}