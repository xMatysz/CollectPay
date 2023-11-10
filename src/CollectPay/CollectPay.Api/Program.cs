using CollectPay.Api.Errors;
using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;
using CollectPay.Persistence.Installers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddApplication()
		.AddInfrastructure()
		.AddPersistence(builder.Configuration.GetConnectionString("dbConnection")!);

	builder.Services.AddControllers();

	builder.Host.UseSerilog((context, config) =>
		config.ReadFrom.Configuration(context.Configuration));

	builder.Services.AddSingleton<ProblemDetailsFactory, CollectPayProblemDetailsFactory>();
}

var app = builder.Build();
{
	app.UseSerilogRequestLogging();
	app.UseExceptionHandler("/error");
	app.UseHttpsRedirection();
	app.MapControllers();

	app.Run();
}
