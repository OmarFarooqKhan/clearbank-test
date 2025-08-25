using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class FasterPaymentsValidator : IPaymentSchemeValidator
{
    public bool IsValid(MakePaymentRequest request, Account account)
    {
        return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) ||
               account.Balance < request.Amount;

    }
}