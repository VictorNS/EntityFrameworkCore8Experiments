using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class EfBulkLogic
{
	internal static async Task ExecuteDelete(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.OrderRequests.Where(x => x.Id > 100).ExecuteDeleteAsync();");
			await context.OrderRequests.Where(x => x.Id > 100).ExecuteDeleteAsync();
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}

	internal static async Task ExecuteUpdate(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("context.OrderRequests.Where(x => x.Id == 2).ExecuteUpdateAsync(setters => ...");
			await context.OrderRequests.Where(x => x.Id == 2)
				.ExecuteUpdateAsync(setters => setters
					.SetProperty(exp => exp.ExpectedRevenue, 100000.9999M)
					.SetProperty(exp => exp.ExpirationYear, exp => exp.ExpirationYear + 1)
					.SetProperty(exp => exp.Customer, exp => exp.Customer + "...")
				);
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}
}
