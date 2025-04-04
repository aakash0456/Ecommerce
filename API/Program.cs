using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<StoreContext> (opt =>
  {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
  }
);
builder.Services.AddCors();
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
});
app.UseStaticFiles();
app.MapControllers();

DbInitializer.InitDb(app);

app.Run();
