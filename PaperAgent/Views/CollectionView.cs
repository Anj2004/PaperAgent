using PaperAgent.ViewModels;

namespace PaperAgent.Views;

public class CollectionView : ContentPage
{
	public CollectionView()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
		BindingContext = new HouseholdsPageViewModel();
	}
}