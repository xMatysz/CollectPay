using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Common;

public partial class ViewModelBase : ObservableValidator
{
	[ObservableProperty]
	private bool _isRefreshing;

	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotBusy))]
	private bool _isBusy;

	public bool IsNotBusy => !IsBusy;

	protected ViewModelBase()
	{
		ValidateAllProperties();
	}

	protected Task OnRefreshingAsync(Action action)
	{
		IsRefreshing = true;

		try
		{
            action();
		}
		finally
		{
			IsRefreshing = false;
		}

		return Task.CompletedTask;
	}

	protected Task OnLoadingAsync(Action action)
	{
		IsBusy = true;

		try
		{
			action();
		}
		finally
		{
			IsBusy = false;

		}

		return Task.CompletedTask;
	}
}