using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Services.Interfaces;

public interface IPaymentSchemeValidatorResolver
{
    /// <summary>
    /// Retrieves Associated PaymentScheme Validator
    /// </summary>
    /// <param name="paymentScheme">Scheme to retrieve validator for</param>
    /// <returns>IPaymentSchemeValidator</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when Unknown PaymentScheme Provided</exception>
    IPaymentSchemeValidator RetrievePaymentSchemeValidator(PaymentScheme paymentScheme);
}