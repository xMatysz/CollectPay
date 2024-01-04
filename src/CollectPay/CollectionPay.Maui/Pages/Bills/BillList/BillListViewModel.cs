using System.Collections.ObjectModel;
using CollectionPay.Maui.Common;

namespace CollectionPay.Maui.Pages.Bills.BillList;

public class BillListViewModel : ViewModelBase
{
	public ObservableCollection<BillModel> Bills { get; } = new()
	{
		new BillModel(){Name = "Wakacyjny"},
		new BillModel(){Name = "Rachunki Malta"}
	};

	public string TestName { get; } = "TestsNames";
}