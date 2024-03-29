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
using Relive.Server.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Relive.Server.Core.Interfaces;
using Relive.Server.Infrastructure.Repositories;
using Relive.Server.Core.UserAggregate;
using Relive.Server.API.Services;
using Swashbuckle.AspNetCore.Swagger;
using Relive.Server.API.Mapper;
using AutoMapper;
using Relive.Server.API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Relive.Server.API.Authorization.AuthorizationHandlers;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Relive.Server.Core.Entities.ProfileAggregate;
using Relive.Server.API.Middlewares;

namespace Relive.Server.API
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
            ConfigureDependencies.ConfigureServices(Configuration, services);
            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:AppSecret"])),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("OwnerPolicy", policy =>
                {
                    policy.Requirements.Add(new OwnerRequirement());    
                });
            });

            services.AddSingleton<IAuthorizationHandler, OwnerAuthorizationHandler>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Relive.Server.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT" 
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")));
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<TravellerProfile>, Repository<TravellerProfile>>();
            services.AddScoped<UserAuthenticationService>();

            var mapConfig = new MapperConfiguration(options =>
            {
                options.AddProfile<MapperProfile>();
            });
            IMapper mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Relive.Server.API v1"));
            }
            app.UseMiddleware<ExceptionHandler>();
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
