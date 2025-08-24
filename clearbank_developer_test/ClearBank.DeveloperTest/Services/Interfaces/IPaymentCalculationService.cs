using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces;

public interface IPaymentCalculationService
{
    void ProcessDeductions(Account account, decimal amountToDeduct);
}