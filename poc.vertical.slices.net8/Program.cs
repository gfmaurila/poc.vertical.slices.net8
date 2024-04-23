using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Database;
using poc.vertical.slices.net8.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EFSqlServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker") || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapCarter();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

await app.MigrateAsync(); // Aqui faz migrations

app.Run();
