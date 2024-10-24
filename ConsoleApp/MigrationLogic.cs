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

	internal static async Task ExecuteSeeding(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Order.AddRangeAsync(...");
			await context.Orders.AddRangeAsync(
			[
				new()
				{
					Customer = "BMW",
					Items =
					[
						new()
						{
							Name = "Gear",
							Quantity = 1000,
							Price = 1.5001M,
						},
						new()
						{
							Name = "Pedal",
							Quantity = 1,
							Price = 15001M,
						},
					]
				},
				new()
				{
					Customer = "FIAT",
					Items =
					[
						new()
						{
							Name = "Spoiler",
							Quantity = 12.34M,
							Price = 12.34M,
						},
						new()
						{
							Name = "Sticker",
							Quantity = 34.12M,
							Price = 34.12M,
						},
					]
				},
			]);
			await context.SaveChangesAsync();

			logger.WriteLine();
			logger.WriteLine("await context.Orders.ToListAsync();");
			var orders = await context.Orders.ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
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
