using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class RawSqlQueryLogic
{
	internal static async Task ExecuteFromSql(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.FromSql($\"SELECT * FROM Orders WHERE Customer = {propertyValue}\").ToListAsync();");
			var propertyValue = "BMW";
			var orders = await context.Orders.FromSql($"SELECT * FROM Orders WHERE Customer = {propertyValue}").ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
		}
		catch { }
	}
}
