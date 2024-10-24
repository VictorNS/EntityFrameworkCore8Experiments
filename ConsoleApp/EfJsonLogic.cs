using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

internal class EfJsonLogic
{
	internal static async Task ExecuteCreate(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.OrderRequests.AddRangeAsync(...");
			await context.OrderRequests.AddRangeAsync(
			[
				new()
				{
					Customer = "Mercedes",
					ExpectedRevenue = 123.456M,
					ExpirationYear = 2030,
					MetaData = new()
					{
						CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
						Contact = new()
						{
							Email = "someone@mail.com",
							Phone = "12-34-56"
						}
					}
				},
				new()
				{
					Customer = "Ford",
					ExpectedRevenue = 456.123M,
					ExpirationYear = 2031,
					MetaData = new()
					{
						CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
					}
				},
			]);
			await context.SaveChangesAsync();

			logger.WriteLine();
			logger.WriteLine("await context.OrderRequests.ToListAsync();");
			var orders = await context.OrderRequests.ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
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
			var obj = await context.OrderRequests.Where(x => x.Customer == "Mercedes").FirstAsync();
			obj.MetaData.Contact!.Phone = "+13 12-34-56";
			logger.WriteLine("obj.MetaData.Contact!.Phone = \"+13 12-34-56\";");
			await context.SaveChangesAsync();

			logger.WriteLine();
			logger.WriteLine("await context.OrderRequests.ToListAsync();");
			var orders = await context.OrderRequests.ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}

	internal static async Task ExecuteSelect(ConsoleFileLogger logger, MyContext context)
	{
		try
		{
			logger.WriteLine();
			logger.WriteLine("await context.OrderRequests.Where(x => x.MetaData.Contact != null && x.MetaData.Contact.Email == \"someone@mail.com\").Select(x => new { x.Id, Email = x.MetaData.Contact == null ? \"\" : x.MetaData.Contact.Email }).ToListAsync();");
			var orders = await context.OrderRequests
				.Where(x => x.MetaData.Contact != null && x.MetaData.Contact.Email == "someone@mail.com")
				.Select(x => new { x.Id, Email = x.MetaData.Contact == null ? "" : x.MetaData.Contact.Email }).ToListAsync();
			logger.WriteLine(System.Text.Json.JsonSerializer.Serialize(orders));
		}
		catch (Exception ex)
		{
			logger.WriteLine(ex.ToString());
		}
	}
}
