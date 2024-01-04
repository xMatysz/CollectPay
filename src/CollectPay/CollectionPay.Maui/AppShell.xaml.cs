using CollectionPay.Maui.Pages.Bills.BillList;

namespace CollectionPay.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(BillListView), typeof(BillListView));
	}
}