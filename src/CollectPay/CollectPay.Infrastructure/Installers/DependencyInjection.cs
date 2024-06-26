﻿using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Infrastructure.Repositories;
using CollectPay.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Infrastructure.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IBillRepository, BillRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		services.AddTransient<IDebtService, DebtService>();
		services.AddTransient<IPasswordHasher, PasswordHasher>();
		services.AddTransient<ITokenService, TokenService>();

		// services.Configure<ISecretProvider>(config.GetSection("Secrets"));
		return services;
	}
}