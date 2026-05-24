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
        private string _newPricePerIssue;
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
            Publication publication = new Publication
            {
                Name = NewName,
                Type = NewType,
                Frequency = NewFrequency,
                PricePerIssue = decimal.Parse(NewPricePerIssue)
            };
            await _dbService.SavePublicationAsync(publication);
        }

    }
}
