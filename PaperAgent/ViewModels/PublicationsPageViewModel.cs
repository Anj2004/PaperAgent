using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;
using System.Collections.ObjectModel;
using PaperAgent.Models;
using CommunityToolkit.Mvvm.Input;

namespace PaperAgent.ViewModels
{
    public partial class PublicationsPageViewModel: ObservableObject
    {
        private readonly DatabaseService _dbService;

        public ObservableCollection<Publication> Publications { get; set; } = new();

        [ObservableProperty] 
        private string _newName;

        [ObservableProperty]
        private string __newType;

        [ObservableProperty]
        private string _newFrequency;

        [ObservableProperty]
        private decimal _newPricePerIssue;
        public PublicationsPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task LoadPublicationsAsync()
        {
            var publications = await _dbService.GetAllPublicationsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Publications.Clear();
                foreach (var publication in publications)
                {
                    Publications.Add(publication);
                }
            });
        }

        [RelayCommand]
        public async Task AddPublicationAsync() // stopping here, need to analyse , learn & doc
        {
            try
            {
                Publication publication = new Publication
                {
                    Name = NewName,
                    Type = NewType,
                    Frequency = NewFrequency,
                    PricePerIssue = NewPricePerIssue,
                    IsActive = true
                };
                await _dbService.SavePublicationAsync(publication);
                await LoadPublicationsAsync();

                NewName = string.Empty;
                NewType = string.Empty;
                NewFrequency = string.Empty;
                NewPricePerIssue = 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., show an error message to the user)
                Console.WriteLine($"Error adding publication: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Could not add publication. Please try again.", "OK");
            }

        }

    }
}
