using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Zeus.Core.Helpers;
using Zeus.Core.Helpers.Interface;
using Zeus.Core.UnitOfWork;
using Zeus.EntityFrameworkCore;
using Zeus.OneSignalCoreAPI.Services;
using Zeus.OneSignalCoreAPI.Services.Interfaces;

namespace Zeus.OneSignalCoreAPI
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
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "OneSignal API",
                        Version = "1"
                    });

                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Swagger.xml"));
            });
            services.AddMemoryCache();
            services.AddSingleton(Configuration);
            services.AddDbContext<OneSignalDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppService, AppService>();
            services.AddSingleton<IInputValidator, DefaultInputValidator>();
            services.AddAutoMapper(Assembly.Load("Zeus.OneSignalCoreAPI"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint
                ("/swagger/v1/swagger.json", "OneSignal API");
                setupAction.RoutePrefix = ""; // To available at root
            });

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
