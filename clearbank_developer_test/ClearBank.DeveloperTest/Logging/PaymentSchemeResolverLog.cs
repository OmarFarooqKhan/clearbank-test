using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Logging;

public record PaymentSchemeResolverLog(PaymentScheme PaymentScheme, string AccountNumber, bool Outcome);