using AirlineService.Interfaces;
using AirlineService.Models;
using Common;
using CommonDAL;
using CommonDAL.Interfaces;
using CommonDAL.Models;
using CommonDAL.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new StringToIntConverter());
                    options.JsonSerializerOptions.Converters.Add(new StringToDecimalConverter());
                    options.JsonSerializerOptions.Converters.Add(new StringToDateTimeConverter());                    
                });
            services.AddConsulConfig(Configuration);
            services.AddDbContext<FlightBookingDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FlightBookingConnection")));
            services.AddSwaggerGen();
            services.AddTransient<IAirlineRepository, AirlineRepository>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
                x.AddRider(rider =>
                {
                    rider.AddConsumer<AirlineRepository>();
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");
                        k.TopicEndpoint<InventoryModificationDetails>(nameof(InventoryModificationDetails), GetUniqueName(nameof(InventoryModificationDetails)), e =>
                        {
                            e.CheckpointInterval = TimeSpan.FromSeconds(10);
                            e.ConfigureConsumer<AirlineRepository>(context);
                        });
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {

                var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddTransient<IJWTManagerRepository, JWTManagerRepository>();
        }

        private string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string classAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{classAssembly}.{eventName}";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul(Configuration);

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseAuthentication();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
