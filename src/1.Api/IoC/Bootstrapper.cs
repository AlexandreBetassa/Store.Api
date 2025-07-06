﻿using Fatec.Store.Framework.Core.Enums;
using Fatec.Store.User.Api.Filters.v1;
using Fatec.Store.User.Application.Commands.v1.Auth.GenerateToken;
using Fatec.Store.User.Application.Commands.v1.Users.CreateUser;
using Fatec.Store.User.Application.Services.v1;
using Fatec.Store.User.Domain.Interfaces.v1.Repositories;
using Fatec.Store.User.Domain.Interfaces.v1.Services;
using Fatec.Store.User.Infrastructure.CrossCutting.Configurations.v1;
using Fatec.Store.User.Infrastructure.Data.Context;
using Fatec.Store.User.Infrastructure.Data.Repositories.v1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Fatec.Store.User.Api.IoC
{
    public static class Bootstrapper
    {
        public static void InjectDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var appSettingsConfigurations = services.AddConfigurations(builder);

            services.InjectAuthenticationSwagger();
            services.InjectContext(appSettingsConfigurations);
            services.InjectRepositories();
            services.InjectServices();
            services.InjectFilters();
            services.InjectMediator();
            services.InjectAutoMapper();
            services.AddHttpContextAccessor();
            services.ConfigureAuthentication(appSettingsConfigurations);
            services.InjectRedis(appSettingsConfigurations);
        }

        private static void InjectRedis(this IServiceCollection services, AppsettingsConfigurations appSettingsConfigurations) =>
           services.AddStackExchangeRedisCache(options => options.Configuration = appSettingsConfigurations.RedisConfiguration.Server);

        private static void ConfigureAuthentication(this IServiceCollection services, AppsettingsConfigurations appSettingsConfigurations)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = false;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsConfigurations.JwtConfiguration!.SecretJwtKey)),
                    ValidIssuer = appSettingsConfigurations.JwtConfiguration.Issuer,
                    ValidAudience = appSettingsConfigurations.JwtConfiguration.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });

            services.AddAuthorizationBuilder()
              .AddPolicy(nameof(AccessPoliciesEnum.Write),
                policy => policy.RequireRole(appSettingsConfigurations.JwtConfiguration!.WriteRoles))
             .AddPolicy(nameof(AccessPoliciesEnum.Read),
                policy => policy.RequireRole(appSettingsConfigurations.JwtConfiguration!.ReadRoles));
        }

        private static void InjectAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(opt => opt.AddMaps(typeof(CreateUserCommand).Assembly));

        private static void InjectMediator(this IServiceCollection services) =>
            services.AddMediatR(new MediatRServiceConfiguration().RegisterServicesFromAssemblyContaining(typeof(GenerateTokenCommandHandler)));

        private static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IRedisService, RedisService>();
            services.AddTransient(typeof(IPasswordServices), typeof(PasswordService));
        }

        private static void InjectContext(this IServiceCollection services, AppsettingsConfigurations appSettingsConfigurations) =>
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(appSettingsConfigurations!.Database));

        private static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
        }

        private static void InjectFilters(this IServiceCollection services)
        {
            services.AddTransient<FilterHeader>();
        }

        public static void InjectAuthenticationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fatec.Store.Users.Api", Version = "v1", Description = "Api responsável por gerenciar dados pessoais e de login do usuário" });

                 c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                 {
                     Description =
                         "JWT Authorization Header - Utilizado com Bearer Authentication.\r\n\r\n" +
                         "Digite seu token no campo abaixo.\r\n\r\n" +
                         "Exemplo (informar apenas o token): '12345abcdef'",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer",
                     BearerFormat = "JWT"
                 });

                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                 });
             });
        }

        public static AppsettingsConfigurations? AddConfigurations(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<AppsettingsConfigurations>(builder.Configuration.GetSection(nameof(AppsettingsConfigurations)));
            services.AddTransient(sp => sp.GetRequiredService<IOptions<AppsettingsConfigurations>>().Value);

            return services?.BuildServiceProvider()?.GetRequiredService<AppsettingsConfigurations>();
        }
    }
}
