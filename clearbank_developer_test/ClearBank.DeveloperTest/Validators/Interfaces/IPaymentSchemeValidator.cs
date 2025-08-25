using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Interfaces;

public interface IPaymentSchemeValidator
{
    /// <summary>
    /// Validates against Associated PaymentScheme
    /// </summary>
    /// <param name="request"></param>
    /// <param name="account"></param>
    /// <returns>Whether the PaymentScheme is valid or not</returns>
    public bool IsValid(MakePaymentRequest request, Account account);
}