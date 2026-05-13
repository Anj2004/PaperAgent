using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using PaperAgent.Models;
using PaperAgent.Services;
using CommunityToolkit.Mvvm.Input;

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

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Households.Clear();
                foreach (var item in items)
                {
                    Households.Add(item);
                }
            });
        }

        [ObservableProperty]
        private string _newName; //generates public property NewName. This is mainly to keep the users data through get and set. Since there
                                //might come more and more instances of this, we do the get and set using the source generator. After get;set; the 
                               //source generator generates the public property NewName. 

        [ObservableProperty]
        private string _newAddress; //generates public property NewAddress

        [RelayCommand]
        public async Task AddHousehold()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewAddress))
                    return;

                Household newHouse = new Household
                {
                    Name = NewName,
                    Address = NewAddress,
                };
                await _dbService.SaveHouseholdAsync(newHouse);//save to db
                await LoadHouseholdsAsync(); //refresh the list

                NewName = string.Empty;//empty both
                NewAddress = string.Empty;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddHousehold failed: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }

        }
    }
}
