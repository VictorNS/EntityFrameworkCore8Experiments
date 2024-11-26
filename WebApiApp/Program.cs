using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

#region Add services to the container
builder.Services.AddDbContext<MyContext>(options => BuildEFOptions(options, builder.Environment.IsDevelopment()));
//builder.Services.AddDbContextFactory<MyContext>(options => BuildEFOptions(options, builder.Environment.IsDevelopment()));
#endregion Add services to the container

builder.Services.AddControllers();
var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();

static void BuildEFOptions(DbContextOptionsBuilder options, bool isDevelopment)
{
	if (isDevelopment)
	{
		options
			.UseSqlServer("server=(local); database=MyContext; Integrated Security=true; Encrypt=false")
			.EnableSensitiveDataLogging()
			.LogTo(Console.WriteLine, LogLevel.Information);
	}
	else
	{
		options.UseSqlServer("server=(local); database=MyContext; Integrated Security=true; Encrypt=false");
	}
}
