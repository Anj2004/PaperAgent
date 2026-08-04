using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;
using PaperAgent.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace PaperAgent.ViewModels
{
    public partial class RoutesPageViewModel : ObservableObject
    {
        public ObservableCollection<Route> Routes { get; set; } = new();

        private readonly DatabaseService _dbservice;

        [ObservableProperty]
        private string _newName;

        [ObservableProperty]
        private string _newDescription;

        public RoutesPageViewModel(DatabaseService dbservice)
        {
            _dbservice = dbservice;
        }

        public async Task LoadRoutesAsync()
        {
            var items = await _dbservice.GetAllRoutesAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Routes.Clear();
                foreach (var item in items)
                {
                    Routes.Add(item);
                }
            });
        }

        [RelayCommand]
        public async Task AddRoute()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewDescription))
                    return;

                Route newRoute = new Route
                {
                    Name = NewName,
                    Description = NewDescription,
                    IsActive = true
                };
                await _dbservice.SaveRouteAsync(newRoute);
                await LoadRoutesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"Error adding route: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task DeleteRoute(Route route)
        {
            await _dbservice.DeleteRouteAsync(route);
            await LoadRoutesAsync();
        }
    }
}
