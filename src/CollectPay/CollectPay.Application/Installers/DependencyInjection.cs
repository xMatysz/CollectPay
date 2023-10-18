using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(
		this IServiceCollection services)
	{
		return services;
	}
}