# EntityFrameworkCore8Experiments

## Content
1. EF logging
	1. Sensitive data & password
	1. Custom logger
1. 

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
