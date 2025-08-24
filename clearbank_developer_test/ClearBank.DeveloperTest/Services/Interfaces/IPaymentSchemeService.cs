using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces;

public interface IPaymentSchemeService
{
    bool IsSuccessfulPayment(MakePaymentRequest request, Account account);
}