using AutoMapper;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Exceptions;
using UIusalAjans.Domain.Profiles;
using UIusalAjans.Domain.ValidationRules;
using UlusalAjans.Api;
using UlusalAjans.Api.Controllers;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Data.Dapper;
using UlusalAjans.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>();
    options.DisableDataAnnotationsValidation = true;
    options.AutomaticValidationEnabled = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, EfProductService>();
builder.Services.AddScoped<ICategoryService, EfCategoryService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<SqlConnection>(_ =>
{
    return new SqlConnection(builder.Configuration.GetConnectionString("NorthwindConnStr"));
});

builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnStr"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) => builder.Environment.IsDevelopment();
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
    options.Map<UlusalAjansException>(ex => new UlusalAjansProblemDetails(ex));
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("info202Iiskur$3k5"));

        options.TokenValidationParameters.ValidateAudience = false;

        options.TokenValidationParameters.ValidIssuer = "http://localhost:5052";
        options.TokenValidationParameters.ValidateIssuer = true;

        options.TokenValidationParameters.IssuerSigningKey = symmetricKey;
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;

        options.TokenValidationParameters.ValidateLifetime = true;

    });

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ulusal Ajans Wep Api",
        Description = "Authentication and Authorization in ASP.NET CORE 6 with JWT and Swagger",
    });
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token Bilgisini Giriniz: \r\n\r\nÖrnek Kullanım: \r\nBearer eyJhbGciOiJIUzI1NiIsInR5...",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("gmail", build =>
    {
        build.RequireAssertion(context =>
        {
            var email = context.User.FindFirst(ClaimTypes.Email).Value;
            return email.Contains("@gmail.com");
        });
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsdev", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });

    options.AddPolicy("corsprod", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("https://ua.gov.tr");
    });
});

var app = builder.Build();

app.UseProblemDetails();

app.UseCustomMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("corsdev");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("corsprod");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
