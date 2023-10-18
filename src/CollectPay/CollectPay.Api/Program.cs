using CollectPay.Application.Installers;
using CollectPay.Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddApplication()
		.AddInfrastructure();
}

var app = builder.Build();
{
	app.MapGet("/", () => "Hello World!");

	app.Run();
}
