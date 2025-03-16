using Autenticacao.Jwt.Application.Commands.v1.GenerateToken;
using Autenticacao.Jwt.Application.Commands.v1.Users.CreateUser;
using Autenticacao.Jwt.Application.Constants.v1;
using Autenticacao.Jwt.Application.Services.v1;
using Autenticacao.Jwt.Domain.Interfaces.v1.Patterns;
using Autenticacao.Jwt.Domain.Interfaces.v1.Repositories;
using Autenticacao.Jwt.Domain.Interfaces.v1.Services;
using Autenticacao.Jwt.Filters.v1;
using Autenticacao.Jwt.Infrastructure;
using Autenticacao.Jwt.Infrastructure.Repositories.v1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Store.User.CrossCutting.Configurations.v1;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppsettingsConfigurations>(builder.Configuration.GetSection(nameof(AppsettingsConfigurations)));
builder.Services.AddTransient(sp => sp.GetRequiredService<IOptions<AppsettingsConfigurations>>().Value);
var appSettingsConfigurations = builder?.Services?.BuildServiceProvider()?.GetRequiredService<AppsettingsConfigurations>();

builder.Services.AddTransient<IDbConnection>(dbOptions => new SqlConnection(appSettingsConfigurations?.Database));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Authenticação", Version = "v1" });

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

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddTransient<IUnityOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IPasswordServices<>), typeof(PasswordService<>));
builder.Services.AddTransient<FilterHeader>();

builder.Services.AddMediatR(
    new MediatRServiceConfiguration().RegisterServicesFromAssemblyContaining(typeof(GenerateTokenCommandHandler)));

builder.Services.AddAutoMapper(opt => opt.AddMaps(typeof(CreateUserCommand).Assembly));

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsConfigurations.JwtConfiguration!.SecretJwtKey)),
            ValidIssuer = appSettingsConfigurations.JwtConfiguration.Issuer,
            ValidAudience = appSettingsConfigurations.JwtConfiguration.Audience,
            ValidateIssuer = true,
            ValidateAudience = true
        };
    });

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = appSettingsConfigurations.RedisConfiguration.Server
);

var configurations = builder.Configuration.GetSection(nameof(AppsettingsConfigurations)).Get<AppsettingsConfigurations>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AccessPolicies.Write, policy =>
        policy.RequireRole(appSettingsConfigurations.JwtConfiguration!.WriteRoles))
    .AddPolicy(AccessPolicies.Read, policy =>
        policy.RequireRole(appSettingsConfigurations.JwtConfiguration!.ReadRoles));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();