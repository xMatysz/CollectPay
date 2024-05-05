using CollectPay.Api.Installers;
using CollectPay.Application.Common.Configuration;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence.Installers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.Configure<SecretProvider>(
		builder.Configuration.GetSection("Secrets"));

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
