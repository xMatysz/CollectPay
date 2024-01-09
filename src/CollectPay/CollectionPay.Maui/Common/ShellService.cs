namespace CollectionPay.Maui.Common;

public interface IShellService
{
	public Task DisplayAlert(string title, string message);

	public Task GoToAsync(ShellNavigationState state);

	public Task ShowError(string message);
}

public class ShellService : IShellService
{
	public Task DisplayAlert(string title, string message) =>
		Shell.Current.DisplayAlert(title, message, "OK");

	public Task GoToAsync(ShellNavigationState state) =>
		Shell.Current.GoToAsync(state);

	public Task ShowError(string message) =>
		Shell.Current.DisplayAlert("Error", message, "OK");
}