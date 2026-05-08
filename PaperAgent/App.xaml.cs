using PaperAgent.Services;
using PaperAgent.Views;

namespace PaperAgent;

public partial class App : Application
{
    private readonly AppShell _appShell;

    public App(DatabaseService dbService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        Task.Run(async () =>
        {
            try
            {
                await dbService.InitAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB Init failed: {ex.Message}");
            }
        }).Wait();
        _appShell = serviceProvider.GetRequiredService<AppShell>();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_appShell);
    }
}