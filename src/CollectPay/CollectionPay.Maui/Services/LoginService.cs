using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.User;
using CollectionPay.Contracts.Responses;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;

namespace CollectionPay.Maui.Services;

public interface ILoginService
{
	public bool IsAuthenticated();
	public Task<bool> LoginAsync(LoginModel model, CancellationToken cancellationToken = default);
	public Task RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default);
	public void LogOut();
}

public class LoginService : ILoginService
{
	private const string _authenticationKey = "IsAuth";

	private readonly IPreferences _preferences;
	private readonly ISecureStorage _secureStorage;
	private readonly IApiClient _client;

	public LoginService(
		IPreferences preferences,
		ISecureStorage secureStorage,
		IApiClient client)
	{
		_preferences = preferences;
		_secureStorage = secureStorage;
		_client = client;
	}

	public bool IsAuthenticated()
	{
		// TODO: Do better
		var test = _client.SendGet("/test", CancellationToken.None).GetAwaiter().GetResult();

		return test.StatusCode != HttpStatusCode.Unauthorized;
	}

	public async Task<bool> LoginAsync(LoginModel model, CancellationToken cancellationToken)
	{
		var loginRequest = new LoginUserRequest(model.Login, model.Password);
		var result = await _client.SendPost("/user/login", loginRequest, cancellationToken);
		if (!result.IsSuccessStatusCode)
		{
			return false;
		}

		var response = result.Content.ReadFromJsonAsync<LoginUserResponse>(cancellationToken).GetAwaiter().GetResult();
		await _secureStorage.SetAsync(_authenticationKey, response.TokenValue);
		_preferences.Set("email", response.Email);
		return true;
	}

	public async Task RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default)
	{
		var registerRequest = new RegisterUserRequest(model.Login, model.Password);

		var response = await _client.SendPost("/user/register", registerRequest, cancellationToken);

		if (response.IsSuccessStatusCode)
		{
			await Shell.Current.DisplayAlert("Success", "User registered", "Ok");
			return;
		}

		var error = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

		await Shell.Current.DisplayAlert("Error", error.Title, "Ok");
	}

	public void LogOut()
	{
		_preferences.Clear();
		_secureStorage.RemoveAll();
	}
}