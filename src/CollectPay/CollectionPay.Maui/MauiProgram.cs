﻿using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Pages.Bills.CreateBill;
using CollectionPay.Maui.Services;
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

		builder.Services.AddSingleton(Connectivity.Current);
		builder.Services.AddScoped<IBillService, BillService>();

		builder.Services.AddHttpClient<IBillService, BillService>(client =>
		{
			var baseAddress = DeviceInfo.Platform == DevicePlatform.Android
				? "http://10.0.2.2:5066"
				: "http://localhost:5066";

			client.BaseAddress = new Uri(baseAddress);
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}

	public static MauiAppBuilder AddPages(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<BillListViewModel>();
		builder.Services.AddTransient<BillListView>();

		builder.Services.AddTransient<CreateBillViewModel>();
		builder.Services.AddTransient<CreateBillView>();

		return builder;
	}
}