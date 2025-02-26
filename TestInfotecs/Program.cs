using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestInfotecs.Data;
using TestInfotecs.Models;
using TestInfotecs.Servises;
using TestInfotecs.Servises.IServises;
using TestInfotecs.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<CSVModel>, Validation>();
builder.Services.AddScoped<IReader, CsvReader>();
builder.Services.AddScoped<ICreateResults, CreateResults>();
builder.Services.AddScoped<IFilters, Filters>();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
