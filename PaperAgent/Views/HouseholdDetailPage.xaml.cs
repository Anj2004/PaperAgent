using PaperAgent.ViewModels;

namespace PaperAgent.Views;

public partial class HouseholdDetailPage : ContentPage
{
	private readonly HouseholdDetailViewModel _viewModel;

    public HouseholdDetailPage(HouseholdDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
	}
}