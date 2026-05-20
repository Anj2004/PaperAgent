using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;
using PaperAgent.Models;
using System.Collections.ObjectModel;

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

        [ObservableProperty]
        private string _householdAddress;

        public ObservableCollection<Subscription> Subscriptions { get; set; } = new();

        public HouseholdDetailViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        //Added the LoadHouseholdAsync()
        //that takes the houseold object returned from the db and stores the household name
        //into a variable, in order to display."
        public async Task LoadHouseholdAsync()
        {
            Household household = await _dbService.GetHouseholdAsync(HouseholdId);
            HouseholdName = household.Name;
            HouseholdAddress = household.Address;
            await LoadSubscriptionsAsync();
        }

        partial void OnHouseholdIdChanged(int value)//The other half is auto generated. It returns the id as "value"
        {
            _ = LoadHouseholdAsync();//Loadhousehold returns a task. But we dont want to await for it. Its fire & forget. Need to check why.
        }

        public async Task LoadSubscriptionsAsync()
        {
            var subscriptions = await _dbService.GetSubscriptionsAsync(HouseholdId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Subscriptions.Clear();
                foreach (var subscription in subscriptions)
                {
                    Subscriptions.Add(subscription);
                }
            });
        }


    }
}
