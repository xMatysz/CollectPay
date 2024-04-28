using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Infrastructure.Repositories;
using CollectPay.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Infrastructure.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IBillRepository, BillRepository>();

		services.AddTransient<IDebtService, DebtService>();
		services.AddTransient<IPasswordHasher, PasswordHasher>();

		return services;
	}
}