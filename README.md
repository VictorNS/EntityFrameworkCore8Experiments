# EntityFrameworkCore8Experiments

## Content
1. EF logging
	1. Sensitive data & password
	1. Custom logger
1. Idempotent migration for SQL objects
1. Experiments with a function
1. `FromSql` uses `FormattableString`
1. EF `AsSingleQuery` vs `AsSplitQuery`
1. EF client evaluation
1. EF & JSON
	1. `null` in an JSON object
	1. Update -> `JSON_MODIFY`
	1. Select & Where -> `JSON_QUERY`
1. EF `ExecuteDeleteAsync`
1. EF `ExecuteUpdateAsync` atomic changes are available
1. ASP.NET Core EF logging
1. ASP.NET Core `AddDbContext<>()` & `IDisposable`
1. ASP.NET Core `AddDbContext<>()` vs `AddDbContextFactory<>()`

Links:
* [User-defined function mapping](https://learn.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping)
* [SQL Queries](https://learn.microsoft.com/en-us/ef/core/querying/sql-queries)
* [DbContext initialization](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
* [AddDbContext is `ServiceLifetime.Scoped`](https://github.com/dotnet/efcore/blob/main/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs)
* [DI Disposal of services](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0#disposal-of-services)


Migrations:
```
dotnet ef migrations add Initial -c MyContext -o Migrations
dotnet ef migrations add AddFunctionGetOrderRevenue -c MyContext -o Migrations
```
