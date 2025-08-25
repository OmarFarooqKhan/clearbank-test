using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Factories;
using ClearBank.DeveloperTest.Data.Factories.Interfaces;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Data.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection ConfigureDbSelection(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<AccountDataStoreFactoryOptions>(
            configuration.GetSection("AccountDataStoreFactoryOptions"));

        serviceCollection.AddSingleton<AccountDataStore>();
        serviceCollection.AddSingleton<BackupAccountDataStore>();
        serviceCollection.AddSingleton<IAccountDataStore, AccountDataStoreProxy>();
        serviceCollection.AddSingleton<IAccountDataStoreFactory, AccountDataStoreFactory>();

        return serviceCollection;
    }
}