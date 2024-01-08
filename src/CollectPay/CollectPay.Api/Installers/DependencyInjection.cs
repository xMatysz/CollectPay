using System.Reflection;
using CollectPay.Api.Errors;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CollectPay.Api.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(
		this IServiceCollection services)
	{
		services.AddSingleton<ProblemDetailsFactory, CollectPayProblemDetailsFactory>();

		services.AddMapping();

		return services;
	}

	private static IServiceCollection AddMapping(this IServiceCollection services)
	{
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();
		return services;
	}
}