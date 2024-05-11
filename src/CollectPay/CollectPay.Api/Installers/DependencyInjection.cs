using CollectPay.Api.Authentication;
using CollectPay.Api.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

	public static IServiceCollection AddAuth(
		this IServiceCollection services)
	{
		services
			.AddAuthentication(cfg =>
			{
				cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer();

		services.AddAuthorization();

		services.ConfigureOptions<SecretProviderSetup>();
		services.ConfigureOptions<JwtBearerOptionsSetup>();

		return services;
	}
}