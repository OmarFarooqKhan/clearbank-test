using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Factories.Interfaces;
using ClearBank.DeveloperTest.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection ConfigureDbSelection(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<AccountDataStoreFactoryOptions>(
            configuration.GetSection("AccountDataStoreFactoryOptions"));

        serviceCollection.AddSingleton<IAccountDataStore, AccountDataStore>();
        serviceCollection.AddSingleton<IAccountDataStore, BackupAccountDataStore>();
        serviceCollection.AddSingleton<IAccountDataStore, AccountDataStoreProxy>();
        serviceCollection.AddSingleton<IAccountDataStoreFactory, AccountDataStoreFactory>();

        return serviceCollection;
    }
}