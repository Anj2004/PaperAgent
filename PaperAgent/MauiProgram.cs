using Microsoft.Extensions.Logging;
using PaperAgent.Services;
using PaperAgent.ViewModels;
using PaperAgent.Views;

namespace PaperAgent
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DatabaseService>();//databaseService is registered as singleton in MauiProgram.cs so that exactly one instance of the db service is created and shared accross the whole app.

            builder.Services.AddTransient<HouseholdsPage>(); // Adding the HouseholdsPage as a transient service means that a new instance of the page will be created each time it is requested. This is useful for pages that may have dynamic content or need to be refreshed each time they are displayed.
            builder.Services.AddTransient<HouseholdsPageViewModel>(); //Corresponding viewmodel must also be transient

            builder.Services.AddTransient<HouseholdDetailPage>();
            builder.Services.AddTransient<HouseholdDetailViewModel>();

            builder.Services.AddSingleton<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
