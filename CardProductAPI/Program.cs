using System.Reflection;
using System.Text.Json.Serialization;
using CardProductAPI;
using CardProductAPI.Features.Cards;
using CardProductAPI.Models.Data;
using CardProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddMvc();
builder.Services.AddSqlite<CardProductContext>("Data Source=Card.db");
builder.Services.AddMediatR(configuration => 
    configuration.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>());
builder.Services.AddSingleton<PostCardRequest>();
builder.Services.AddControllers().AddJsonOptions(x =>{
// serialize enums as strings in api responses (e.g. Role)
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
