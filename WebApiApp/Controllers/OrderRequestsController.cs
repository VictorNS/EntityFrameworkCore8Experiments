using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderRequestsController : ControllerBase
{
	readonly IDbContextFactory<MyContext> _contextFactory;

	public OrderRequestsController(IDbContextFactory<MyContext> contextFactory)
	{
		_contextFactory = contextFactory;
	}

	[HttpGet()]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		Console.WriteLine("---OrderRequestsController::Get()");
		List<OrderRequest> orderRequests = [];

		using (var context = _contextFactory.CreateDbContext())
		{
			orderRequests.Add(await context.OrderRequests.FirstAsync(x => x.Id == 1, cancellationToken));
		}

		using (var context = _contextFactory.CreateDbContext())
		{
			orderRequests.Add(await context.OrderRequests.FirstAsync(x => x.Id == 2, cancellationToken));
		}

		return Ok(orderRequests);
	}
}
