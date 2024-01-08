using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Common;

public partial class ViewModelBase : ObservableObject
{
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotBusy))]
	private bool _isRefreshing;

	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	private bool _isBusy;

	public bool IsNotBusy => !IsBusy;
}