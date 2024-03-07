using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Pages.Bills.BillDetails;

[QueryProperty(nameof(BillModel), "Bill")]
public partial class BillDetailsViewModel : ViewModelBase
{
	[ObservableProperty]
	private BillModel _bill;
}