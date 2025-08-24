using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClearBank.DeveloperTest;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection ConfigureDbSelection(IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.Configure<AccountManagementOptions>(
            configuration.GetSection("AccountManagementOptions"));

        serviceCollection.AddSingleton<AccountDataStore>();
        serviceCollection.AddSingleton<BackupAccountDataStore>();

        serviceCollection.AddSingleton<IAccountDataStore, AccountDataStoreFactory>();
        
        return serviceCollection;
    }
}