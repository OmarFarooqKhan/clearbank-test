using ClearBank.DeveloperTest.Data.Interfaces;

namespace ClearBank.DeveloperTest.Factories.Interfaces;

public interface IAccountDataStoreFactory
{
    IAccountDataStore Create();
}