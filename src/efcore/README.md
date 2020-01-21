# EntityFrameworkCore Extensions

## Cosmos

Adds support for string method translation in queries. Enable this as follows:

``` CSharp

services.AddEntityFrameworkCosmos();
services.AddDbContext<MyDbContext>(options =>
{
    options.UseCosmos(
        accountEndpoint: settings.CosmosDb.AccountEndpoint,
        accountKey: settings.CosmosDb.AccountKey,
        databaseName: settings.CosmosDb.DatabaseName
    );

    options.AddCosmosExtensions(); // Adds our custom plugins, which includes the string query translations.
});

```

__NOTE__: This makes use of internal Cosmos API's and has no guarantee of stability.
