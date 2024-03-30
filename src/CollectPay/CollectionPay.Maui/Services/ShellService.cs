namespace CollectionPay.Maui.Services;

public interface IShellService
{
	Task GoToAsync(ShellNavigationState route);
	Task GoToAsync(ShellNavigationState route, Dictionary<string, object> queries);
}

public class ShellService : IShellService
{
	public async Task GoToAsync(ShellNavigationState route)
	{
		await Shell.Current.GoToAsync(route);
	}

	public async Task GoToAsync(ShellNavigationState route, Dictionary<string, object> queries)
	{
		await Shell.Current.GoToAsync(route, queries);
	}
}