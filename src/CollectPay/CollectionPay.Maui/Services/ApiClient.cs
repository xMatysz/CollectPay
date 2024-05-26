using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CollectionPay.Maui.Services;

public interface IApiClient
{
	Task<HttpResponseMessage> SendGet(string route, CancellationToken cancellationToken);
	Task<HttpResponseMessage> SendPost<TRequest>(string route, TRequest request, CancellationToken cancellationToken);
}

public class ApiClient : IApiClient
{
	private readonly HttpClient _httpClient;
	private readonly ISecureStorage _secureStorage;

	public ApiClient(HttpClient httpClient,
		ISecureStorage secureStorage)
	{
		_httpClient = httpClient;
		_secureStorage = secureStorage;

		var token = secureStorage.GetAsync("IsAuth").GetAwaiter().GetResult();
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}

	public Task<HttpResponseMessage> SendGet(string route, CancellationToken cancellationToken)
	{
		var token = _secureStorage.GetAsync("IsAuth").GetAwaiter().GetResult();
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		return _httpClient.GetAsync(route, cancellationToken);
	}

	public Task<HttpResponseMessage> SendPost<TRequest>(string route, TRequest request, CancellationToken cancellationToken)
	{
		return _httpClient.PostAsJsonAsync(route, request, cancellationToken);
	}
}