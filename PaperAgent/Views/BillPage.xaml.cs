using PaperAgent.ViewModels;

namespace PaperAgent.Views;

public partial class BillPage : ContentPage
{
    private readonly BillPageViewModel _viewModel;

    public BillPage(BillPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
}