using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Abstraction;

public abstract partial class ViewModelBase : ObservableObject
{
	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	private bool _isRefreshing;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotBusy))]
	private bool _isBusy;

	public bool IsNotBusy => !IsBusy;
}