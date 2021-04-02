using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Domain.Interfaces;
using Eatigo.Eatilink.Domain.Managers;
using Eatigo.Eatilink.Domain.Services;
using Eatigo.Eatilink.Infrastructure.Repository;
using Eatigo.Eatilink.Security.Handlers;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Api
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
            var settings = GetAppConfigurationSection();
            services.AddControllers();
            services.AddApiVersioning();
            ConfigureSingletonServices(services);
            ConfigureTransientServices(services);
            ConfigureJwtAuthentication(services, settings);
            ConfigureSwaggerServices(services);
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Eatilink API");

            });
        }

        private AppSettings GetAppConfigurationSection()
        {
            return Configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        private void ConfigureSingletonServices(IServiceCollection services)
        {
            var settings = GetAppConfigurationSection();
            services.AddSingleton(settings);
        }

        private void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ILinkShortenerValidator), typeof(LinkShortenerValidator));
            services.AddTransient(typeof(IJwtTokenHandler), typeof(JwtTokenHandler));
            services.AddTransient(typeof(IAutoRefreshingCacheService), typeof(AutoRefreshingCacheService));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepositoryLinkShortenUrl), typeof(LinkShortenUrlRepository));
            services.AddTransient(typeof(ILinkShortenManager), typeof(LinkShortenManager));
            services.AddTransient(typeof(ILinkShortenService), typeof(LinkShortenService));
        }

        private static void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Swagger API",
                        Description = "Eatilink API",
                        Version = "v1"
                    });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

            });
        }
        private void ConfigureJwtAuthentication(IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.JsonWebTokens.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
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
            IdentityModelEventSource.ShowPII = true;
        }
    }
}
