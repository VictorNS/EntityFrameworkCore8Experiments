using ConsoleApp;
using Data;
using Microsoft.EntityFrameworkCore;

using var logger = new ConsoleFileLogger();

try
{
	#region create context
	var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
	optionsBuilder
		.UseSqlServer("server=(local); database=MyContext; Integrated Security=true; Encrypt=false")
		.EnableSensitiveDataLogging()
		.LogTo(logger.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

	using var context = new MyContext(optionsBuilder.Options);
	#endregion create context

	//await MigrationLogic.ExecuteMigrations(logger, context);
	//await MigrationLogic.ExecuteSeeding(logger, context);
	//await FunctionLogic.ExecuteInList(logger, context);
	//await FunctionLogic.ExecuteAsFunction(logger, context);
	//await FunctionLogic.ExecuteAsSql(logger, context);
	//await RawSqlQueryLogic.ExecuteFromSql(logger, context);
	//await EfQueryLogic.ExecuteSplitOneQuery(logger, context);
	//await EfQueryLogic.ExecuteSplitManyQuery(logger, context);
	//await EfQueryLogic.ExecuteClientEvaluation(logger, context);
	//await EfJsonLogic.ExecuteCreate(logger, context);
	//await EfJsonLogic.ExecuteUpdate(logger, context);
	//await EfJsonLogic.ExecuteSelect(logger, context);
	//await EfBulkLogic.ExecuteDelete(logger, context);
	//await EfBulkLogic.ExecuteUpdate(logger, context);
}
catch (Exception ex)
{
	logger.WriteLine();
	logger.WriteLine();
	logger.WriteLine(ex.ToString());
	return 1;
}
finally
{
	logger.MentionHerself();
}

return 0;
