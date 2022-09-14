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
using Target.Backend.Web.Interfaces.Repositories;
using Target.Backend.Web.Repositories;
using Target.Backend.Web.Data;
using Target.Backend.Web.Interfaces.Transaction;
using Target.Backend.Web.Transaction;
using AutoMapper;
using NLog;
using System.IO;
using Target.Backend.Web.Interfaces.Services;
using Target.Backend.Web.Services;
using Target.Backend.Web.Extensions;

namespace Target.Backend.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson(c =>
                c.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteEnderecoRepository, ClienteEnderecoRepository>();
            services.AddTransient<IPlanoRepository, PlanoRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Target Api",
                    Description = "Target Api",
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Target.Backend.Web.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("X-API-Key", new OpenApiSecurityScheme
                {
                    Description = "Api key necessária para acessar as rotas. X-Api-Key: SecretKey",
                    In = ParameterLocation.Header,
                    Name = "X-API-Key",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                        {
                            Name = "X-API-Key",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "X-API-Key" }
                        }, new string[] {}}
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Target API V1");

                options.RoutePrefix = string.Empty;
            });
        }
    }
}
