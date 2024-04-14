using System.Diagnostics.SymbolStore;
using CollectionPay.Maui.Models;

namespace CollectionPay.Maui.Services;

public interface ILoginService
{
	public bool IsAuthenticated();
	public Task<bool> LoginAsync(LoginModel model, CancellationToken cancellationToken = default);
	public Task<bool> RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default);
	public void LogOut();
}

public class LoginService : ILoginService
{
	private const string _isAuthenticatedKey = "IsAuth";

	private readonly IPreferences _preferences;
	private readonly ISecureStorage _secureStorage;

	private List<RegisterModel> _users = new();

	public LoginService(
		IPreferences preferences,
		ISecureStorage secureStorage)
	{
		_preferences = preferences;
		_secureStorage = secureStorage;
	}

	public bool IsAuthenticated()
	{
		return _preferences.Get(_isAuthenticatedKey, false);
	}

	public async Task<bool> LoginAsync(LoginModel model, CancellationToken cancellationToken)
	{
		var result = _users.Any(user => user.Login == model.Login && user.Password == model.Password);
		_preferences.Set(_isAuthenticatedKey, result);
		return result;
	}

	public async Task<bool> RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default)
	{
		_users.Add(model);
		return true;
	}

	public void LogOut()
	{
		_preferences.Clear();
		_secureStorage.RemoveAll();
	}
}