using CollectPay.Api.ApiTests.Common;
using ErrorOr;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CollectPay.Api.ApiTests.Controllers;

public abstract class ControllerTestBase
{
	private HttpClient? _client;
	private Action<IServiceCollection> _configuration = _ => { };

	protected HttpClient Client => _client ??= CreateClient();

	private HttpClient CreateClient()
	{
		var api = new WebApiFactory(_configuration);
		return api.CreateClient();
	}

	protected void ConfigureHandler<TRequest, TResult, TDouble>()
		where TRequest : IRequest<TResult>
		where TResult : IErrorOr
		where TDouble : class, IRequestHandler<TRequest, TResult>
	{
		_configuration = services =>
		{
			services.RemoveAll(typeof(IRequestHandler<TRequest, TResult>));
			services.TryAddScoped<IRequestHandler<TRequest, TResult>, TDouble>();
		};
	}
}