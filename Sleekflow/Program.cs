using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sleekflow;
using Sleekflow.Interfaces;
using Sleekflow.Models;

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
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<UsersDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
});
builder.Services.AddScoped<IDbTodoRepo, DbTodoRepo>();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
