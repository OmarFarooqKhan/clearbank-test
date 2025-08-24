using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.Logging;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentCalculationService _paymentCalculationService;
        private readonly IPaymentSchemeService _paymentSchemeService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IAccountService accountService,
            IPaymentCalculationService paymentCalculationService,
            IPaymentSchemeService paymentSchemeService,
            ILogger<PaymentService> logger)
        {
            _accountService = accountService;
            _paymentCalculationService = paymentCalculationService;
            _paymentSchemeService = paymentSchemeService;
            _logger = logger;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
           var account = _accountService.RetrieveAccount(request.DebtorAccountNumber);
           var isSuccessfulPayment = _paymentSchemeService.IsSuccessfulPayment(request, account);
           if (!isSuccessfulPayment) return new MakePaymentResult { Success = false };
           _paymentCalculationService.ProcessDeductions(account, request.Amount);
           return new MakePaymentResult { Success = true };
        }
    }
}
