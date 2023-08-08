using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Fuel9.Context;
using Fuel9.DTO;
using Fuel9.Interfaces;
using Fuel9.Mapping;
using Fuel9.Repository;
using Fuel9.Repository.Interfaces;
using Fuel9.Services;
using Fuel9.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cors", builder => {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
    options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());

});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<Fuel9DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Fuel9Database")));
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IFuelLogRepository, FuelLogRepository>();
builder.Services.AddScoped<IFuelLogService, FuelLogService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<VehicleDTO>, VehicleDTOValidator>();
builder.Services.AddScoped<IValidator<FuelLogDTO>, FuelLogDTOValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Fuel9DbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

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
