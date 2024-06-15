using CollectPay.Api.Installers;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence;
using CollectPay.Persistence.Installers;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddAuth();

	builder.Services
		.AddApplication()
		.AddInfrastructure()
		.AddPersistence(builder.Configuration)
		.AddPresentation();

	builder.Services.AddControllers();

	builder.Host.UseSerilog((context, config) =>
		config.ReadFrom.Configuration(context.Configuration));
}

var app = builder.Build();
{
	app.UseExceptionHandler("/error");

	app.UseSerilogRequestLogging();

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllers();

	if (app.Environment.IsDevelopment())
	{
		// RunMigration(app);
	}

	app.Run();
}

static void RunMigration(IHost app)
{
	using var scope = app.Services.CreateScope();
	using var dbContext = scope.ServiceProvider.GetRequiredService<CollectPayDbContext>();
	dbContext.Database.Migrate();
}
