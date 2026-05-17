using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;
using PaperAgent.Models;

namespace PaperAgent.ViewModels
{

    [QueryProperty("HouseholdId", "id")]
    public partial class HouseholdDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Household _household;

        [ObservableProperty]
        private int _householdId;

        [ObservableProperty]
        private string _householdName;

        public HouseholdDetailViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }


        public async Task LoadHouseholdAsync()
        {
            Household household = await _dbService.GetHouseholdAsync(HouseholdId);
            HouseholdName = household.Name;
        }

        partial void OnHouseholdIdChanged(int value)//but why partial void and why value? Start from here
        {
            _ = LoadHouseholdAsync();
        }

    }
}
