namespace CollectionPay.Maui.Abstraction;

public class ContentPageBase<TViewModel> : ContentPage
{
	protected ContentPageBase(TViewModel viewModel)
	{
		BindingContext = viewModel;
		ViewModel = viewModel;
	}

	protected TViewModel ViewModel { get; }
}