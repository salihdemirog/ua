using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Profiles;
using UIusalAjans.Domain.ValidationRules;
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
builder.Services.AddScoped<ICategoryService, DapperCategoryService>();

builder.Services.AddScoped<SqlConnection>(_ =>
{
    return new SqlConnection(builder.Configuration.GetConnectionString("NorthwindConnStr"));
});

builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnStr"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
