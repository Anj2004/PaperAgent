using Microsoft.Extensions.DependencyInjection;
using PaperAgent.Services;

namespace PaperAgent
{
    public partial class App : Application
    {
        public App(DatabaseService dbService)//By accepting DatabaseService as a constructor parameter, MAUI's dependency injection automatically passes in the singleton we registered in MauiProgram.cs
        {
            InitializeComponent();
            //MainPage = new AppShell();
            Task.Run(async () =>
            {
                try
                {
                    await dbService.InitAsync();
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error initializing database: {ex.Message}");
                }
            }).Wait();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}