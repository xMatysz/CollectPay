using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Infrastructure.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services)
	{
		return services;
	}
}