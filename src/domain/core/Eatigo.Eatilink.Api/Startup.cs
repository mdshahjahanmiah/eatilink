using Eatigo.Eatilink.DataObjects.Settings;
using Eatigo.Eatilink.Security.Handlers;
using Eatigo.Eatilink.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
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
            ConfigureSingletonServices(services);
            ConfigureTransientServices(services);
            ConfigureJwtAuthentication(services, settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }

        private AppSettings GetAppConfigurationSection() => Configuration.GetSection("AppSettings").Get<AppSettings>();

        private void ConfigureSingletonServices(IServiceCollection services)
        {
            services.AddSingleton(GetAppConfigurationSection());
        }

        private void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ILinkShortenerValidator), typeof(LinkShortenerValidator));
            services.AddTransient(typeof(IJwtTokenHandler), typeof(JwtTokenHandler));
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
