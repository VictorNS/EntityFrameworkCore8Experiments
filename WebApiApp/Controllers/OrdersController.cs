using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
	readonly MyContext _context;

	public OrdersController(MyContext context)
	{
		_context = context;
	}

	[HttpGet()]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		Console.WriteLine("---OrdersController::Get()");
		return Ok(await _context.Orders.ToListAsync(cancellationToken));
	}
}
