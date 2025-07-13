using Project.CrossCutting.Configurations.v1;
using Store.Api.IoC;

var builder = WebApplication.CreateBuilder(args);
Bootstrapper.CreateBootstrapper<Bootstrapper, Appsettings>(builder).InjectDependencies();

var app = builder.Build();
//DatabaseManagementService.MigrationInitialisation(app);

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