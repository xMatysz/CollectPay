using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.User;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;

namespace CollectionPay.Maui.Services;

public interface ILoginService
{
	public Task<bool> IsAuthenticated();
	public Task<bool> TryLoginAsync(LoginModel model, CancellationToken cancellationToken = default);
	public Task<bool> TryRegisterAsync(RegisterModel model, CancellationToken cancellationToken = default);
	public void LogOut();
}

public class LoginService : ILoginService
{
	private const string _authenticationTokenKey = "IsAuth";
	private const string _isAuthenticatedKey = "IsAuthenticatedUser";

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

	public async Task<bool> IsAuthenticated()
	{
		var haveToken = _preferences.Get(_isAuthenticatedKey, false);
		if (!haveToken)
		{
			return false;
		}

		try
		{
			var testResponse = await _client.SendGet(BillRoutes.List, CancellationToken.None);
			return testResponse.StatusCode != HttpStatusCode.Unauthorized;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return false;
		}
	}

	public async Task<bool> TryLoginAsync(LoginModel model, CancellationToken cancellationToken)
	{
		var loginRequest = new LoginUserRequest(model.Login, model.Password);
		var result = await _client.SendPost(UserRoutes.Login, loginRequest, cancellationToken);

		if (!result.IsSuccessStatusCode)
		{
			var problem = await result.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken: cancellationToken);
			await Shell.Current.DisplayAlert(problem.Title, problem.Detail, "OK");
			return false;
		}

		var response = await result.Content.ReadFromJsonAsync<LoginUserResponse>(cancellationToken);

		await SetValuesFromResponse(response);

		return true;
	}

	private async Task SetValuesFromResponse(LoginUserResponse response)
	{
		await _secureStorage.SetAsync(_authenticationTokenKey, response.TokenValue);
		_preferences.Set(_isAuthenticatedKey, true);
		_preferences.Set("email", response.Email);
	}

	public async Task<bool> TryRegisterAsync(RegisterModel model, CancellationToken cancellationToken = default)
	{
		var registerRequest = new RegisterUserRequest(model.Login, model.Password);

		var response = await _client.SendPost(UserRoutes.Register, registerRequest, cancellationToken);

		if (response.IsSuccessStatusCode)
		{
			await Shell.Current.DisplayAlert("Success", "User registered", "Ok");
			return true;
		}

		var error = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);

		await Shell.Current.DisplayAlert(error.Title, error.Detail, "Ok");
		return false;
	}

	public void LogOut()
	{
		_preferences.Clear();
		_secureStorage.RemoveAll();
	}
}