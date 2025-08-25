using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentCalculationService _paymentCalculationService;
        private readonly IPaymentSchemeService _paymentSchemeService;

        public PaymentService(IAccountService accountService,
            IPaymentCalculationService paymentCalculationService,
            IPaymentSchemeService paymentSchemeService)
        {
            _accountService = accountService;
            _paymentCalculationService = paymentCalculationService;
            _paymentSchemeService = paymentSchemeService;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.RetrieveAccount(request.DebtorAccountNumber);
            var isValidPaymentScheme = _paymentSchemeService.IsValidPaymentScheme(request, account);
            if (!isValidPaymentScheme) return new MakePaymentResult { Success = false };
            _paymentCalculationService.ProcessDeductions(account, request.Amount);
            return new MakePaymentResult { Success = true };
        }
    }
}
