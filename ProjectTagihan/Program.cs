using Microsoft.EntityFrameworkCore;
using ProjectTagihan.Contexts;
using ProjectTagihan.Contracts;
using ProjectTagihan.Repositories;
using ProjectTagihan.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<BillServices>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BillDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Database"), new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
