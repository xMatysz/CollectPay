using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddApplication()
		.AddInfrastructure();

	builder.Host.UseSerilog((context, config) =>
		config.ReadFrom.Configuration(context.Configuration));
}

var app = builder.Build();
{
	app.UseSerilogRequestLogging();
	app.MapGet("/", () => "Hello World!");

	app.Run();
}
