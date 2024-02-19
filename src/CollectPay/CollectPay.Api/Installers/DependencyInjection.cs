using CollectPay.Api.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CollectPay.Api.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(
		this IServiceCollection services)
	{
		services.AddSingleton<ProblemDetailsFactory, CollectPayProblemDetailsFactory>();

		return services;
	}
}