using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public class PaymentCalculationService(IAccountService accountService) : IPaymentCalculationService
{
    public void ProcessDeductions(Account account, decimal amountToDeduct)
    {
        account.Balance -= amountToDeduct;
        accountService.UpdateAccount(account);
    }
}