using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Pages.Bills.CreateBill;
using CollectionPay.Maui.Pages.Login;
using CollectionPay.Maui.Pages.Register;

namespace CollectionPay.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
		Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
		Routing.RegisterRoute(nameof(BillListView), typeof(BillListView));
		Routing.RegisterRoute(nameof(CreateBillView), typeof(CreateBillView));
	}
}