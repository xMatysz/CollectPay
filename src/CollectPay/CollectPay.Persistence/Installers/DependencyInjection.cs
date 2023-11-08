using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Persistence.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services)
	{
		services.AddDbContext<CollectPayDbContext>(opt =>
			opt.UseNpgsql("host=localhost;port=1000;username=root;password=123;database=collect"));

		return services;
	}
}