using Project.CrossCutting.Configurations.v1;
using Project.Framework.Core.v1.IoC;
using Store.Api.Filters.v1;
using Store.Application.Commands.v1.Auth.GenerateToken;
using Store.Application.Commands.v1.Users.CreateUser;
using Store.Application.Services.v1;
using Store.Data.Context;
using Store.Data.Repositories.v1;
using Store.Domain.Interfaces.v1.Repositories;
using Store.Domain.Interfaces.v1.Services;

namespace Store.Api.IoC
{
    public class Bootstrapper(WebApplicationBuilder builder) : BaseBootstrapper<Appsettings>(builder)
    {
        public override void InjectDependencies()
        {
            InjectServices();
            InjectRepositories();
            InjectContext<UserDbContext>();
            InjectMediatorFromAssembly(typeof(GenerateTokenCommandHandler));
            InjectAutoMapperFromAssembly(typeof(CreateUserCommandProfile).Assembly);
            InjectRedis();
            InjectFilters();
        }

        private void InjectServices()
        {
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddScoped<IRedisService, RedisService>();
            Services.AddScoped(typeof(IPasswordServices), typeof(PasswordService));
        }

        private void InjectRepositories()
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<ILoginRepository, LoginRepository>();
        }

        private void InjectFilters()
        {
            Services.AddTransient<FilterHeader>();
        }
    }
}