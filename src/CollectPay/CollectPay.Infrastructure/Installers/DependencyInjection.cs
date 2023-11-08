using CollectPay.Application.Common.Repositories;
using CollectPay.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Infrastructure.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IBillRepository, BillRepository>();
		return services;
	}
}