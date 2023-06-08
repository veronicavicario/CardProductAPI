using System.Text.Json.Serialization;
using CardProductAPI.Commons.Filters;
using CardProductAPI.Commons.Validators;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using CardProductAPI.Repository;
using CardProductAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<CardDto>, CardValidator>();
builder.Services.AddScoped<IValidator<ContractDto>, ContractValidator>();

builder.Services.AddScoped<IRepository<Contract>, Repository<Contract>>();
builder.Services.AddScoped<IRepository<Card>, Repository<Card>>();

builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddSqlite<CardProductContext>(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddControllers(options => options.Filters.Add<CardProductExceptionFilter>())
    .AddJsonOptions(x =>{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
