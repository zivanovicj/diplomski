using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Fuel9_BE.Context;
using Fuel9_BE.DTO;
using Fuel9_BE.Mapping;
using Fuel9_BE.Repository;
using Fuel9_BE.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fuel9_BE.Interfaces;
using Fuel9_BE.Repository.Interfaces;
using Fuel9_BE.Services;

namespace Fuel9_BE
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fuel9", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());

            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<Fuel9DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Fuel9Database")));

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IFuelLogRepository, FuelLogRepository>();
            services.AddScoped<IFuelLogService, FuelLogService>();

            services.AddMvc();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<VehicleDTO>, VehicleDTOValidator>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddScoped<IValidator<FuelLogDTO>, FuelLogDTOValidator>();
            services.AddScoped<IValidator<VehicleDTO>, VehicleDTOValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Fuel9DbContext f9db)
        {
            if (f9db.Database.EnsureCreated())
            {
                f9db.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fuel9_BE v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
