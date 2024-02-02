using ENTITYAPP.Middleware;
using NLog;
using ENTITYAPP.Repository;
using ENTITYAPP.Repository.Data;
using ENTITYAPP.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ENTITYAPP.Service.Contract;
using Microsoft.Extensions.DependencyInjection;
using ENTITYAPP.ApiDocumentation;
using GeeksConfiguration;
using Contract;
using LoggerService;
using System.Runtime.CompilerServices;
using AutoMapper;
using ENTITYAPP.Repository.Models;
using ENTITYAPP.Dto;
using FluentValidation;
using ENTITYAPP.DTO;
using FluentValidation.AspNetCore;
using System.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddDbContext<EmployeeDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EntityConnectionString")));
// In Startup.cs or your dependency injection configuration
builder.Services.AddScoped<IJWTAuthenticationManager, JWTAuthenticationManager>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString(
        "EntityConnectionString");
    options.SchemaName = "dbo";
    options.TableName = "CacheItems";
});


//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration["ConnectionStrings:Redis"];
//    options.InstanceName = "SampleInstance";
//});


builder.Services.AddSingleton<IConfigManager, ConfigManager>();
builder.Services.AddScoped<IloggerManager, LoggerManager>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddLazyCache();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services
.AddFluentValidationAutoValidation(x =>
{
    ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
});

builder.Services.AddValidatorsFromAssembly(typeof(UserValidator).Assembly);

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
});

builder.Services.AddScoped<MyActionFilter>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Web Api", Version = "v1" });
    
    c.AddSecurityDefinition(SwaggerDocumentation.jwtSecurityScheme.Reference.Id, SwaggerDocumentation.jwtSecurityScheme);
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();




var jwtkey = builder.Configuration["Jwt:Key"].ToString();

var key = Encoding.ASCII.GetBytes(jwtkey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>

{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
