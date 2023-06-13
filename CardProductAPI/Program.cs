using System.Text;
using System.Text.Json.Serialization;
using CardProductAPI.Commons.Filters;
using CardProductAPI.Commons.Validators;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using CardProductAPI.Repository;
using CardProductAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IValidator<CardDto>, CardValidator>();
builder.Services.AddScoped<IValidator<ContractDto>, ContractValidator>();

builder.Services.AddScoped<IRepository<Contract>, Repository<Contract>>();
builder.Services.AddScoped<IRepository<Card>, Repository<Card>>();

builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddSqlite<CardProductContext>(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddControllers(options => options.Filters.Add<CardProductExceptionFilter>())
    .AddJsonOptions(x =>{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Auth:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:Auth:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Auth:Key"]))
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddSwaggerGen(setup =>
{ 
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
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

app.Use(async (context, next) =>
{ 
    var headers = context.Request.Headers;
    
    Console.WriteLine($"  - Endpoint: {context.Request.Method} - {context.Request.Path}");
    
    foreach (var header in headers)
    {
        Console.WriteLine($"  - Header: {header}");
    }
    await next(context);
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
