using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces;

public interface IPaymentSchemeService
{
    bool IsValidPaymentScheme(MakePaymentRequest request, Account account);
}