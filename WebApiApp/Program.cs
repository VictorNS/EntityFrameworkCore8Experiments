var builder = WebApplication.CreateBuilder();

#region Add services to the container
#endregion Add services to the container

builder.Services.AddControllers();
var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
