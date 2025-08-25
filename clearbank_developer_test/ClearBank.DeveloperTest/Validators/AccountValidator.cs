using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class AccountValidator : IAccountValidator
{
    public bool IsValidAccount(Account account)
    {
        return account is not null;
    }
}