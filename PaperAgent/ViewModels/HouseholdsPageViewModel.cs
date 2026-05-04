using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using PaperAgent.Models;
using PaperAgent.Services;

namespace PaperAgent.ViewModels
{
    public partial class HouseholdsPageViewModel : ObservableObject
    {
        public ObservableCollection<Household> Households { get; set; } = new();

        private readonly DatabaseService _dbService;
        public HouseholdsPageViewModel(DatabaseService dbService) 
        {
            _dbService = dbService;
        
        }

        public async Task LoadHouseholdsAsync()
        {
            var items = await _dbService.GetAllHouseholdsAsync();
            Households.Clear();
            foreach (var item in items)
            {
                Households.Add(item);
            }
        }
    }
}
