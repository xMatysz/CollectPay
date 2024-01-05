using CollectPay.Api.Installers;
using CollectPay.Application.Common;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Configuration.AddSystemsManager(config =>
	{
		var envName = builder.Environment.EnvironmentName;
		config.Path = $"/CollectPay/{envName}/";
		config.ReloadAfter = TimeSpan.FromSeconds(5);
	});

	builder.Services.Configure<ApplicationOptions>(
		builder.Configuration.GetSection(nameof(ApplicationOptions)));

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
	app.UseSerilogRequestLogging();
	app.UseExceptionHandler("/error");
	app.UseHttpsRedirection();
	app.MapControllers();

	app.Run();
}
