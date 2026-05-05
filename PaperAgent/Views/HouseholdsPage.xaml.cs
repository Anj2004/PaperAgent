using PaperAgent.ViewModels;

namespace PaperAgent.Views;

public partial class HouseholdsPage : ContentPage
{

    private readonly HouseholdsPageViewModel _viewModel;
    public HouseholdsPage(HouseholdsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadHouseholdsAsync();
    }
}