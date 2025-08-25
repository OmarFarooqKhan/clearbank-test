namespace ClearBank.DeveloperTest.Factories;

public class AccountDataStoreFactoryOptions(string dataStoreType)
{
    public string DataStoreType { get; set; } = dataStoreType;
}