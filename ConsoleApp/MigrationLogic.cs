using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class MigrationLogic
{
	internal static async Task ExecuteMigrations(ConsoleFileLogger logger, MyContext context)
	{
		logger.WriteLine();
		logger.WriteLine($"Start migration for: {GetSafetyConnectionString(context.Database.GetConnectionString()!)}");
		logger.WriteLine();
		await context.Database.MigrateAsync();
	}

	static string GetSafetyConnectionString(string connectionString)
	{
		var builder = new System.Data.Common.DbConnectionStringBuilder
		{
			ConnectionString = connectionString
		};

		if (builder.ContainsKey("password"))
			builder["password"] = "***";

		return builder.ToString();
	}
}
