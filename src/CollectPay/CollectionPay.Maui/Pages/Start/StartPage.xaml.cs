namespace CollectionPay.Maui.Pages.Start;

public partial class StartPage
{
	public StartPage(StartViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		await ViewModel.RedirectUser();
	}
}