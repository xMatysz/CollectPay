using CollectPay.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Infrastructure.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services)
	{
		services.AddDbContext<CollectPayDbContext>(opt =>
			opt.UseNpgsql(""));

		return services;
	}
}