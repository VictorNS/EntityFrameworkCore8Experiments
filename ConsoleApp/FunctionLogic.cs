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

	internal static async Task ExecuteAsFunction(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("var revenue = context.GetOrderRevenue(1);");
			var revenue = context.GetOrderRevenue(1);
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}

		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Where(x => x.Id == 1).Select(x => context.GetOrderRevenue(x.Id)).FirstAsync();");
			var revenue = await context.Orders.Where(x => x.Id == 1).Select(x => context.GetOrderRevenue(x.Id)).FirstAsync();
			logger.WriteLine($"{revenue}");
		}
		catch { }

		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Where(x => x.Id == 1).Select(x => context.GetOrderRevenue(1)).FirstAsync();");
			var revenue = await context.Orders.Where(x => x.Id == 1).Select(x => context.GetOrderRevenue(1)).FirstAsync();
			logger.WriteLine($"{revenue}");
		}
		catch (Exception ex)
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Where(x => x.Id == 1).Select(x => context.GetOrderRevenue(1)).FirstAsync();");
			logger.WriteLine(ex.ToString());
			logger.WriteLine();
		}
	}

	internal static async Task ExecuteAsSql(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Database.SqlQuery<decimal>($\"SELECT dbo.ufn_GetOrderRevenue({orderId})\").FirstAsync();");
			int orderId = 1;
			var revenue = await context.Database.SqlQuery<decimal>($"SELECT dbo.ufn_GetOrderRevenue({orderId})").FirstAsync();
			logger.WriteLine($"{revenue}");
		}
		catch { }

		try
		{
			logger.WriteLine();
			logger.WriteLine("(await context.Database.SqlQuery<decimal>($\"SELECT dbo.ufn_GetOrderRevenue({orderId})\").ToListAsync()).First();");
			int orderId = 1;
			var revenue = (await context.Database.SqlQuery<decimal>($"SELECT dbo.ufn_GetOrderRevenue({orderId})").ToListAsync()).First();
			logger.WriteLine($"{revenue}");
		}
		catch { }
	}

	internal static async Task ExecuteExpressionsInList(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Select(x => new { x.Id, x.Customer, DayFullMonthYear = EfFunctions.GetDayFullMonthYear(x.CreatedAt), DayShortMonthYear = EfFunctions.GetDayShortMonthYear(x.CreatedAt) }).ToListAsync();");
			var revenues = await context.Orders
				.Select(x => new { x.Id, x.Customer, DayFullMonthYear = EfFunctions.GetDayFullMonthYear(x.CreatedAt), DayShortMonthYear = EfFunctions.GetDayShortMonthYear(x.CreatedAt) })
				.Where(x => x.DayFullMonthYear.Contains("er 2024"))
				.ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(revenues));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}
}
