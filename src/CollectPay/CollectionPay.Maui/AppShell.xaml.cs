using CollectionPay.Maui.Pages.Bills.BillDetails;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Pages.Bills.CreateBill;

namespace CollectionPay.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(BillListView), typeof(BillListView));
		Routing.RegisterRoute(nameof(CreateBillView), typeof(CreateBillView));
		Routing.RegisterRoute(nameof(BillDetailsView), typeof(BillDetailsView));
	}
}