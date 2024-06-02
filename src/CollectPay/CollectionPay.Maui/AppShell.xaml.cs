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
		RegisterFor<BillDetailsPage>();

		RegisterFor<PaymentListPage>();
		RegisterFor<PaymentCreatePage>();
		RegisterFor<PaymentDetailsPage>();

		RegisterFor<LoginPage>();
		RegisterFor<RegisterPage>();
		RegisterFor<UserPage>();
		RegisterFor<StartPage>();
	}

	private static void RegisterFor<T>() where T : Page
	{
		Routing.RegisterRoute(GetRoute<T>(), typeof(T));
	}

	public static string GetRoute<T>() where T : Page
	{
		var pageType = typeof(T);

		return pageType switch
		{
			not null when pageType == typeof(StartPage) => $"//{nameof(StartPage)}",
			not null when pageType == typeof(LoginPage) => $"//{nameof(LoginPage)}",
			not null when pageType == typeof(RegisterPage) => $"//{nameof(RegisterPage)}",
			not null when pageType == typeof(UserPage) => $"//{nameof(UserPage)}",

			not null when pageType == typeof(BillListPage) => $"//{nameof(BillListPage)}",
			not null when pageType == typeof(BillCreatePage) => $"//{nameof(BillListPage)}/{nameof(BillCreatePage)}",
			not null when pageType == typeof(PaymentListPage) => $"//{nameof(BillListPage)}/{nameof(PaymentListPage)}",
			not null when pageType == typeof(BillDetailsPage) => $"//{nameof(BillListPage)}/{nameof(PaymentListPage)}/{nameof(BillDetailsPage)}",
			not null when pageType == typeof(PaymentCreatePage) => $"//{nameof(BillListPage)}/{nameof(PaymentListPage)}/{nameof(PaymentCreatePage)}",
			not null when pageType == typeof(PaymentDetailsPage) => $"//{nameof(BillListPage)}/{nameof(PaymentListPage)}/{nameof(PaymentDetailsPage)}",
			_ => throw new ArgumentException($"Route for {pageType.Name} is not implemented")
		};
	}
}