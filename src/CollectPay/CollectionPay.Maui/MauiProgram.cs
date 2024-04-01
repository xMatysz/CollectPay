using CollectionPay.Maui.Pages.BillPages.BillCreate;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
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

		builder.Services.AddSingleton<IShellService, ShellService>();

		// builder.Services.AddHttpClient<IBillService, BillService>(client =>
		// {
		// 	var baseAddress = DeviceInfo.Platform == DevicePlatform.Android
		// 		? "http://10.0.2.2:5066"
		// 		: "http://localhost:5066";
		//
		// 	client.BaseAddress = new Uri(baseAddress);
		// 	client.Timeout = TimeSpan.FromSeconds(39);
		// });

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}