using Infrastructure.Data;
using ApplicationCore.Entities;
//using ApplicationCore.RepositoryInterfaces;
//using Infrastructure.Services;
//using ApplicationCore.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using ClientInfoSystem.API.Middlewares;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace ClientInfoSystem.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientInfoSystem.API", Version = "v1" });
            });

            services.AddDbContext<ClientInfoSystemDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ClientInfoSystemDbContext")));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IInteractionRepository, InteractionRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IInteractionService, InteractionService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientInfoSystem.API v1"));
            }

            app.UseClientInfoSystemExceptionMiddleware();

            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
