using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sleekflow;
using Sleekflow.Implementations;
using Sleekflow.Interfaces;
using Sleekflow.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TodoService>();
string connectionString = "Server=(local);Database=Sleekflow;Trusted_Connection=True;TrustServerCertificate=true;";
builder.Services.AddDbContext<TodoDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<UsersDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<IDbUserRepo, DbUserRepo>();
builder.Services.AddScoped<IDbTodoRepo, DbTodoRepo>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = "UserSession";
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		.AddCookie();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Version = "v1",
		Title = "Todo API",
		Description = "An ASP.NET Core Web API for managing ToDo items"
	});
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
