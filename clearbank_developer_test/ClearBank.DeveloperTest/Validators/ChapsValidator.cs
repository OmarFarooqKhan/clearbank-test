using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class ChapsValidator : IPaymentSchemeValidator
{
    public bool IsValid(MakePaymentRequest request, Account account)
    {
        return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) || account.Status == AccountStatus.Live;
    }
}