using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PaperAgent.Models;
using PaperAgent.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
//using static Java.Util.Concurrent.Flow;

namespace PaperAgent.ViewModels
{
    public partial class HouseholdsPageViewModel : ObservableObject
    {
        public ObservableCollection<Household> Households { get; set; } = new();

        private readonly DatabaseService _dbService;


        [ObservableProperty]
        private string _newName; //generates public property NewName. This is mainly to keep the users data through get and set. Since theremight come more and more instances of this, we do the get and set using the source generator. After get;set; the source generator generates the public property NewName. 

        [ObservableProperty]
        private string _newAddress; //generates public property NewAddress

        [ObservableProperty]
        private int _householdCount;

        [ObservableProperty]
        private bool _hasHouseholds;

        [ObservableProperty]
        private string _searchedHousehold;

        public ObservableCollection<Household> FilteredHouseholds { get; set; } = new(); //the list we are going to update through filerhouseholds fn down below


        public HouseholdsPageViewModel(DatabaseService dbService) 
        {
            _dbService = dbService;
        }

        public async Task LoadHouseholdsAsync()
        {
            var items = await _dbService.GetAllHouseholdsAsync();

            MainThread.BeginInvokeOnMainThread(() => //UI chngs must be touched by Main thread only. Right before running this the cntrl would be with a bg thread. So at this step, we are clearing the household list; ie touching the UI. So we pass the cntrl back to the main thread. 
            {
                Households.Clear();
                foreach (var item in items)
                {
                    Households.Add(item);
                    FilteredHouseholds.Add(item);
                }
                HouseholdCount = Households.Count; //update the count after loading the households
                FilterHouseholds(string.Empty);
            });
            HasHouseholds = HouseholdCount > 0;
        }

        partial void OnSearchedHouseholdChanged(string value)
        {
            FilterHouseholds(value);
        }

        private void FilterHouseholds(string value)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FilteredHouseholds.Clear();

                var houses = string.IsNullOrEmpty(value) ? Households : Households.Where(h => h.Name.Contains(value, StringComparison.OrdinalIgnoreCase));

                foreach (var house in houses)
                {
                    FilteredHouseholds.Add(house);
                }
            });
        }

        [RelayCommand] //To add a new household
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
                await Shell.Current.DisplayAlert("Error", "Could not add household. Please try again.", "OK");
            }

        }

        [RelayCommand] //To delete the household
        public async Task DeleteHousehold(int id)
        {
            await _dbService.DeleteHouseholdAsync(id);
            await LoadHouseholdsAsync();
        }

        [RelayCommand]
        private async Task GoToHousehold(Household household)
        {
            await Shell.Current.GoToAsync($"householddetail?id={household.Id}");
        }

        [RelayCommand]
        private async Task GoToPublications()
        {
            await Shell.Current.GoToAsync("publications");//navigate to the route named publications
        }

    }
}
