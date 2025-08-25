using ClearBank.DeveloperTest.Data.Interfaces;

namespace ClearBank.DeveloperTest.Data.Factories.Interfaces;

public interface IAccountDataStoreFactory
{
    IAccountDataStore Create();
}