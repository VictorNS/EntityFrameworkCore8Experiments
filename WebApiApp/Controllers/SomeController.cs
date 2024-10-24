using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SomeController : ControllerBase
{
	readonly MyContext _context;
	readonly IDbContextFactory<MyContext> _contextFactory;

	public SomeController(MyContext context, IDbContextFactory<MyContext> contextFactory)
	{
		_context = context;
		_contextFactory = contextFactory;
	}

	[HttpPost()]
	public async Task<IActionResult> Post1(int count, CancellationToken cancellationToken)
	{
		var contract1 = await _context.Orders.FirstAsync(x => x.Id == 1, cancellationToken);
		for (int i = 0; i < count; i++)
			contract1.Items.Add(new()
			{
				Name = i.ToString(),
			});
		await _context.SaveChangesAsync(cancellationToken);

		var contract2 = await _context.Orders.FirstAsync(x => x.Id == 2, cancellationToken);
		for (int i = 0; i < count; i++)
			contract2.Items.Add(new()
			{
				Name = i.ToString(),
			});
		await _context.SaveChangesAsync(cancellationToken);

		return Ok();
    }

	[HttpPost()]
	public async Task<IActionResult> Post(int count, CancellationToken cancellationToken)
	{
		using (var context = _contextFactory.CreateDbContext())
		{
			var contract1 = await context.Orders.FirstAsync(x => x.Id == 1, cancellationToken);
			for (int i = 0; i < count; i++)
				contract1.Items.Add(new()
				{
					Name = i.ToString(),
				});
			await context.SaveChangesAsync(cancellationToken);
		}

		using (var context = _contextFactory.CreateDbContext())
		{
			var contract2 = await context.Orders.FirstAsync(x => x.Id == 2, cancellationToken);
			for (int i = 0; i < count; i++)
				contract2.Items.Add(new()
				{
					Name = i.ToString(),
				});
			await context.SaveChangesAsync(cancellationToken);
		}

		return Ok();
	}
}
