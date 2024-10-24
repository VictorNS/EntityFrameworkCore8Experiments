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
	await FunctionLogic.ExecuteInList(logger, context);
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
