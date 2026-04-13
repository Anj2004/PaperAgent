using PaperAgent.ViewModels;

namespace PaperAgent
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            MainPageViewModel viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
        private void OnTestClicked(object sender, EventArgs e)
        {
            var vm = BindingContext as MainPageViewModel;
            vm.ChangedTitle();
        }

    }
}
