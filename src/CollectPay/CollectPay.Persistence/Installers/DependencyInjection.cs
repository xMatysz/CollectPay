using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Persistence.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<CollectPayDbContext>(opt =>
			opt.UseNpgsql(connectionString));

		return services;
	}
}