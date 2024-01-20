using CollectPay.Api.Installers;
using CollectPay.Application.Common;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	if (!IsTestEnv(builder.Environment.EnvironmentName))
	{
		builder.Configuration.AddSystemsManager(config =>
		{
			var envName = builder.Environment.EnvironmentName;
			config.Path = $"/CollectPay/{envName}/";
			config.ReloadAfter = TimeSpan.FromSeconds(5);
		});
	}

	builder.Services.Configure<ApplicationOptions>(
		builder.Configuration.GetSection(nameof(ApplicationOptions)));

	builder.Services
		.AddApplication()
		.AddInfrastructure()
		.AddPersistence(builder.Configuration)
		.AddPresentation();

	builder.Services.AddControllers();

	if (!IsTestEnv(builder.Environment.EnvironmentName))
	{
		builder.Host.UseSerilog((context, config) =>
			config.ReadFrom.Configuration(context.Configuration));
	}
}

var app = builder.Build();
{
	if (!IsTestEnv(builder.Environment.EnvironmentName))
	{
		app.UseSerilogRequestLogging();
	}

	app.UseExceptionHandler("/error");
	app.MapControllers();

	app.Run();
}

bool IsTestEnv(string envName)
{
	return envName == "Test";
}
