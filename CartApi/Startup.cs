using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using CartApi.Infrastructure.Filters;
using CartApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace CartApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ICartRepository, RedisCartRepository>();
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                //var settings = sp.GetRequiredService<IOptions<CartSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(Configuration["ConnectionString"], true);
                //resolving via dns before connecting
                configuration.ResolveDns = true;
                configuration.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(configuration);
            });

            ConfigureAuthService(services);
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Basket HTTP API",
                    Version = "v1",
                    Description = "The Basket Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrl")}/connect/authorize",
                    TokenUrl = $"{Configuration.GetValue<string>("IdentityUrl")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "basket", "Basket Api" }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();

            });

        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "basket";
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseAuthentication();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "CartApi V1");
                   //c.ConfigureOAuth2("basketswaggerui", "", "", "Basket Swagger UI");
               });

            app.UseMvc();
        }
    }
}
