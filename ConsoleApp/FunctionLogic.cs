using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class FunctionLogic
{
	internal static async Task ExecuteInList(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("var revenues = await context.Orders.Select(x => new { x.Id, Revenue = context.GetOrderRevenue(x.Id) }).ToListAsync();");
			var revenues = await context.Orders.Select(x => new { x.Id, Revenue = context.GetOrderRevenue(x.Id) }).ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(revenues));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}
}
