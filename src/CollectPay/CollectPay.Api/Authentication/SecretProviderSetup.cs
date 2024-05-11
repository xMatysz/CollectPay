using CollectPay.Application.Common.Configuration;
using Microsoft.Extensions.Options;

namespace CollectPay.Api.Authentication;

public class SecretProviderSetup : IConfigureOptions<SecretProvider>
{
	private const string _sectionName = "Secrets";
	private readonly IConfiguration _configuration;

	public SecretProviderSetup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(SecretProvider options)
	{
		_configuration.GetSection(_sectionName).Bind(options);
	}
}