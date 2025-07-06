using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Data.Context;

namespace Store.Application.Services.v1
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            serviceScope?.ServiceProvider?.GetService<UserDbContext>()?.Database?.Migrate();
        }
    }
}
