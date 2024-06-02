using CollectionPay.Maui.Pages.BillPages.BillCreate;
using CollectionPay.Maui.Pages.BillPages.BillDetails;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.LoginPages.Login;
using CollectionPay.Maui.Pages.LoginPages.Register;
using CollectionPay.Maui.Pages.LoginPages.User;
using CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
using CollectionPay.Maui.Pages.Start;
using CollectionPay.Maui.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CollectionPay.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton(Connectivity.Current);
		builder.Services.AddSingleton(Preferences.Default);
		builder.Services.AddSingleton(SecureStorage.Default);

		builder.Services.AddTransient<BillListPage>();
		builder.Services.AddTransient<BillListViewModel>();

		builder.Services.AddTransient<BillCreatePage>();
		builder.Services.AddTransient<BillCreateViewModel>();

		builder.Services.AddTransient<PaymentListPage>();
		builder.Services.AddTransient<PaymentListViewModel>();

		builder.Services.AddTransient<PaymentCreatePage>();
		builder.Services.AddTransient<PaymentCreateViewModel>();

		builder.Services.AddTransient<PaymentDetailsPage>();
		builder.Services.AddTransient<PaymentDetailsViewModel>();

		builder.Services.AddTransient<BillDetailsPage>();
		builder.Services.AddTransient<BillDetailsViewModel>();

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginViewModel>();

		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<RegisterViewModel>();

		builder.Services.AddTransient<UserPage>();
		builder.Services.AddTransient<UserViewModel>();

		builder.Services.AddTransient<StartPage>();
		builder.Services.AddTransient<StartViewModel>();

		builder.Services.AddSingleton<IShellService, ShellService>();
		builder.Services.AddSingleton<ILoginService, LoginService>();

		builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
		{
			var baseAddress = DeviceInfo.Platform == DevicePlatform.Android
				? "http://10.0.2.2:5066"
				: "http://localhost:5066";

			client.BaseAddress = new Uri(baseAddress);
			client.Timeout = TimeSpan.FromSeconds(5);
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}