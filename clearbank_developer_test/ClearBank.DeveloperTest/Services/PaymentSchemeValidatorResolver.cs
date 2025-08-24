using System;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Services;

public class PaymentSchemeValidatorResolver : IPaymentSchemeValidatorResolver
{
    public IPaymentSchemeValidator RetrievePaymentSchemeValidator(PaymentScheme paymentScheme)
    {
        IPaymentSchemeValidator paymentSchemeValidator = paymentScheme switch
        {
            PaymentScheme.FasterPayments => new FasterPaymentsValidator(),
            PaymentScheme.Bacs => new BacsValidator(),
            PaymentScheme.Chaps => new ChapsValidator(),
            _ => throw new ArgumentOutOfRangeException(nameof(PaymentScheme),
                paymentScheme, null)
        };

        return paymentSchemeValidator;
    }
}