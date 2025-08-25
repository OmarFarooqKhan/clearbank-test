using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class ChapsValidator : IPaymentSchemeValidator
{
    public bool IsValidPaymentScheme(MakePaymentRequest request, Account account)
    {
        return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) || account.Status == AccountStatus.Live ;
    }
}