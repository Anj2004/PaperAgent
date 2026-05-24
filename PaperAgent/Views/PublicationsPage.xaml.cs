using PaperAgent.ViewModels;

namespace PaperAgent.Views;

public partial class PublicationsPage : ContentPage
{
	private readonly PublicationsPageViewModel _viewModel;
	public PublicationsPage(PublicationsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadPublicationsAsync();
	}
}