using CollectionPay.Maui.Pages.BillPages.BillCreate;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;

namespace CollectionPay.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		RegisterRoutes();
	}

	private static void RegisterRoutes()
	{
		RegisterFor<BillListPage>();
		RegisterFor<BillCreatePage>();

		RegisterFor<PaymentListPage>();
		RegisterFor<PaymentCreatePage>();
	}

	private static void RegisterFor<T>() where T : Page
	{
		Routing.RegisterRoute(GetRoute<T>(), typeof(T));
	}

	public static string GetRoute<T>() where T : Page
	{
		var page = typeof(T);

		return page switch
		{
			not null when page == typeof(BillListPage) => $"//{nameof(BillListPage)}",
			not null when page == typeof(BillCreatePage) => $"//{nameof(BillListPage)}//{nameof(BillCreatePage)}",
			not null when page == typeof(PaymentListPage) => $"//{nameof(BillListPage)}//{nameof(PaymentListPage)}",
			not null when page == typeof(PaymentCreatePage) => $"//{nameof(BillListPage)}//{nameof(PaymentListPage)}//{nameof(PaymentCreatePage)}",
			_ => throw new ArgumentException($"Route for {page.Name} is not implemented")
		};
	}
}