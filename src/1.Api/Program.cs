using Store.Api.IoC;
using Store.Application.Services.v1;

var builder = WebApplication.CreateBuilder(args);
Bootstrapper.CreateBootstrapper<Bootstrapper>(builder).InjectDependencies();

var app = builder.Build();
DatabaseManagementService.MigrationInitialisation(app);

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