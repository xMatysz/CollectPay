using CollectPay.Api.Installers;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
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
	app.MapControllers();

	app.Run();
}
