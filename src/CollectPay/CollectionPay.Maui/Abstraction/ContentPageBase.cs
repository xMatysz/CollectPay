namespace CollectionPay.Maui.Abstraction;

public class ContentPageBase<TViewModel> : ContentPage
{
	protected ContentPageBase(TViewModel viewModel)
	{
		BindingContext = viewModel;
	}
}