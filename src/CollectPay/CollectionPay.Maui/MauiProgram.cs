using CollectionPay.Maui.Pages.Bills.BillList;
using Microsoft.Extensions.Logging;

namespace CollectionPay.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.AddPages();

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}

	public static MauiAppBuilder AddPages(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<BillListViewModel>();
		builder.Services.AddTransient<BillListView>();

		return builder;
	}
}