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
    public class Bootstrapper(WebApplicationBuilder builder) : BaseBootstrapper(builder)
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
            Services.AddTransient<IEmailService, EmailService>();
            Services.AddTransient<IRedisService, RedisService>();
            Services.AddTransient(typeof(IPasswordServices), typeof(PasswordService));
        }

        private void InjectRepositories()
        {
            Services.AddTransient<IUserRepository, UserRepository>();
            Services.AddTransient<ILoginRepository, LoginRepository>();
        }

        private void InjectFilters()
        {
            Services.AddTransient<FilterHeader>();
        }
    }
}