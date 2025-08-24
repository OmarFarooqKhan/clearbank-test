using System;
using ClearBank.DeveloperTest.Logging;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;
using Microsoft.Extensions.Logging;

namespace ClearBank.DeveloperTest.Services;

public class PaymentSchemeService : IPaymentSchemeService
{
    private readonly IPaymentSchemeValidatorResolver _paymentSchemeValidatorResolver;
    private readonly IAccountValidator _accountValidator;
    private readonly ILogger<PaymentSchemeService> _logger;

    public PaymentSchemeService(
            IPaymentSchemeValidatorResolver paymentSchemeValidatorResolver,
            IAccountValidator accountValidator,
            ILogger<PaymentSchemeService> logger)
    {
        _paymentSchemeValidatorResolver = paymentSchemeValidatorResolver;
        _accountValidator = accountValidator;
        _logger = logger;
    }
    public bool IsSuccessfulPayment(MakePaymentRequest request, Account account)
    {
        try
        {
            var isValidAccount = _accountValidator.IsValidAccount(account);
            if (!isValidAccount)
            {
                _logger.LogWarning(
                    "{EventName} {DebtorAccountNumber}",
                    LogEventNames.PaymentSchemeResolverInvalidAccount, request.DebtorAccountNumber
                );
                return false;
            }

            var paymentScheme = _paymentSchemeValidatorResolver.RetrievePaymentSchemeValidator(request.PaymentScheme);
            var isValidPaymentScheme = paymentScheme.IsValidPaymentScheme(request, account);
            var log = new PaymentSchemeResolverLog(request.PaymentScheme, account.AccountNumber, isValidPaymentScheme);

            _logger.LogInformation(
                "{EventName} {@Log}",
                LogEventNames.PaymentSchemeResolverOutcome,
                log
            );
            return isValidPaymentScheme;

        }
        catch(ArgumentOutOfRangeException ex)
        {
            _logger.LogError(ex, LogEventNames.PaymentSchemeResolverUnknownScheme);
        }

        return false;
    }
}