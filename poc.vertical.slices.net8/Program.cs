using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using poc.vertical.slices.net8.Database;

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
