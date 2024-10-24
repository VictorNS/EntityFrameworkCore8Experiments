using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class EfQueryLogic
{
	internal static async Task ExecuteSplitOneQuery(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Include(x=> x.Items).AsSingleQuery().ToListAsync();");
			var order = await context.Orders.Where(x => x.Id == 1).Include(x => x.Items).AsSingleQuery().FirstAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(order));

			logger.WriteLine();
			logger.WriteLine("await context.Orders.Include(x=> x.Items).AsSplitQuery().ToListAsync();");
			order = await context.Orders.Where(x => x.Id == 1).Include(x => x.Items).AsSplitQuery().FirstAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(order));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}

	internal static async Task ExecuteSplitManyQuery(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Where(x => x.Id < 100).Include(x => x.Items.Where(i => i.Quantity > 1)).AsSingleQuery().ToListAsync();");
			var orders = await context.Orders.Where(x => x.Id < 100).Include(x => x.Items.Where(i => i.Quantity > 1)).AsSingleQuery().ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));

			logger.WriteLine();
			logger.WriteLine("await context.Orders.Where(x => x.Id < 100).Include(x => x.Items.Where(i => i.Quantity > 1)).AsSplitQuery().ToListAsync();");
			orders = await context.Orders.Where(x => x.Id < 100).Include(x => x.Items.Where(i => i.Quantity > 1)).AsSplitQuery().ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}

	internal static async Task ExecuteClientEvaluation(ConsoleFileLogger logger, MyContext context)
	{
		#region client evaluation
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.Orders.Select(x => x.Customer + \" #\" + x.Id).ToListAsync();");
			var clOrders1 = await context.Orders.Select(x => x.Customer + " #" + x.Id).ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(clOrders1));

			logger.WriteLine();
			logger.WriteLine("await context.Orders.Select(x => GetCustomerWithNumber(x.Customer, x.Id)).ToListAsync();");
			var clOrders2 = await context.Orders.Select(x => GetCustomerWithNumber(x.Customer, x.Id)).ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(clOrders2));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
		#endregion client evaluation
	}

	static string GetCustomerWithNumber(string customer, int id)
	{
		return customer + " #" + id;
	}
}
