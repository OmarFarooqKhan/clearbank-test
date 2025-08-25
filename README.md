# Clearbank Tech Test

## Things to refactor

- `PaymentService` utilises `AppSettings` to decide on what Database to use. That responsibility shouldnt be down to the `PaymentService`. Also with AppSettings being used it implies it can be changed without restarting the application.

- Switch case for PaymentSchemes would need to be split and each of the PaymentSchemes logic should be encased in their own validator class.

- Deductions Should be handled seperately from the `PaymentService` as we would need to consider balance e.g what should happen when there is a negative balance after the deduction?.

- `PaymentService` should just care about determining how to Make a payment and return a result.

### What I've changed and why

#### Overall

- I've followed SOLID principles to help split out as much responsiblity from the original `PaymentService` as possible. Interfaces/Classes have been made to help break down some of the logic into their own services to be easier to test and work with.
- With my refactor I've considered observability for the changes too as there previously was none.
  - This would be beneficial for any neccesary debugging if an issue arises with any of the accounts or just general validation that the changes are behaving expectedly.

#### AppSettings and DBs

- I also made sure to make a note of the usage of AppSettings and find a way to handle the potential change from the standard `AccountDataStore` and `BackupAccountDataStore` without having to restart the application. I have an example in `ServiceCollectionExtensions.cs` showing how it would be managed via `IOptionsMonitor` and DI.
- As a result of the interchangeableness of the 2 databases, I decided it would be a good idea to use a Proxy (`AccountDataStoreProxy`) as it shouldnt matter to a dev what DB to use based off the names of the dataStore, with the `AccountDataStoreFactory` being responsible for creating it.
- It has also allowed for easier testing as I can declare what outcome I should see depending on the `DataStoreType` using the Options.

#### AccountService

- Following on from the above I decided to create a generic to use `AccountService` to update or retrieve an account. Previously we needed to know what DB to use but now those lower level details shouldnt matter as the `AccountDataStoreProxy` handles that. The `AccountService` uses the proxy and brings visibility to the space using logs.

#### PaymentSchemeService

- I made an effort to move out all the PaymentScheme specific logic into their own Validators so that they can be tested individually. Its also important to note that I removed the common null check against the account into its own AccountValidator that is called once in the PaymentSchemeService.
- Additionally I handled the SwitchCase in the `PaymentSchemeValidationResolver` as any new schemes could easily be added to the resolver and any unknown schemes that have not been setup can be flagged up.
- Added a structured log here too; will help with debugging the account and any discrepencies with the paymentScheme and the outcome, with the accountNumber being the identifier (So long as its just a number and NOT PII).

#### PaymentService

- After the refactor the only job of the paymentService is to orchestrate the work across all the respective services.
  - It:
    - Looks up the account the payment is being made from (via `AccountService`) ✅
    - Check the account is in a valid state to make the payment (via `PaymentSchemeService`) ✅
    - Deduct the payment amount from the account's balance and update the account in the database (via `PaymentCalculationService`) ✅
- Its no longer interested in the details but rather the outcomes for it to produce a `MakePaymentResult`. 🚀

#### Packages

- Microsoft.Extensions.Logging => To Enable Observability.
- Microsoft.Extensions.Options.ConfigurationExtensions => Allows for a structured expected OptionModel to be configured, also beneficial as you can block app startup if config is messed up. I used it additionally since it would allow me to check for config changes while the app was running via `IOptionsMonitor`.
- `System.Configuration.ConfigurationManager` can be removed as I am using the above which does the same thing.

### Testing

- AutoFixture to remove the faf around populating test data; makes tests easier to work with.
- FluentAssertions as they are really readable and assertions just make sense.
- `Microsoft.Extensions.Diagnostics.Testing` to help make logs easier to test, without it verification of logs becomes a giant block.
- Moq for mocking out dependencies; really useful and very familiar with/its wide known.
- XUnit as familiar framework.

#### Testing Strategy

- Generally I try to stay close to the `Given_When_Then` format as its clear to identify the objective behind a test.
- I prioritised mocking dependencies and Followed the Arrange Act Assert Pattern to make the tests clear to a reader.
- Utilising SOLID patterns also helped make tests easier as the objective behind a class was straight forward.
- Tests share an identical name with their class and follow a similar folder structure to ensure easy traceability.

Thank you so much for reading through my changes and I hope they give you an understanding of my though process when it comes to problem solving 😊
