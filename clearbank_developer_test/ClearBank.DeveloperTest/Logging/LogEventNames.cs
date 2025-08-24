namespace ClearBank.DeveloperTest.Logging;

public static class LogEventNames
{
    public const string AccountServiceSuccess = "AccountService_Success";
    public const string AccountServiceUpdatingAccount = "AccountService_UpdatingAccount";
    public const string AccountServiceUnableToFindAccount = "AccountService_FailedToFindAccount";

    public const string PaymentSchemeResolverInvalidAccount = "PaymentSchemeResolver_InvalidAccount";
    public const string PaymentSchemeResolverUnknownScheme = "PaymentSchemeResolver_UnknownScheme";

    public const string PaymentSchemeResolverOutcome = "PaymentSchemeResolver_Outcome";
}