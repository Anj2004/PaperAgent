using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;
using PaperAgent.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace PaperAgent.ViewModels
{

    [QueryProperty("HouseholdId", "id")]
    public partial class HouseholdDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        private readonly BillingService _billingService;

        [ObservableProperty]
        private Household _household;

        [ObservableProperty]
        private int _householdId;

        [ObservableProperty]
        private string _householdName;

        [ObservableProperty]
        private string _householdAddress;

        [ObservableProperty]
        private Publication _selectedPublication;

        [ObservableProperty]
        private SubscriptionDisplay _selectedSubscription;

        [ObservableProperty]
        private DateTime _pauseFromDate = DateTime.Now;

        [ObservableProperty]
        private DateTime _pauseToDate = DateTime.Now.AddDays(7);

        [ObservableProperty]
        private string _pauseReason;

        [ObservableProperty]
        private int _selectedMonth = DateTime.Now.Month;

        [ObservableProperty]
        private int _selectedYear = DateTime.Now.Year;

        [ObservableProperty]
        private bool _showPauseForm = false;

        [ObservableProperty]
        private bool _hasSubscriptions;


        public ObservableCollection<SubscriptionDisplay> Subscriptions { get; set; } = new();

        public ObservableCollection<Publication> Publications { get; set; } = new();

        public class SubscriptionDisplay
        {
            public int SubscriptionId { get; set; }
            public string PublicationName { get; set; }
            public int Quantity { get; set; }
            public bool IsActive { get; set; }
        }

        public HouseholdDetailViewModel(DatabaseService dbService, BillingService billingService)
        {
            _dbService = dbService;
            _billingService = billingService;
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
            await LoadPublicationsAsync();
        }

        partial void OnHouseholdIdChanged(int value)//The other half is auto generated. It returns the id as "value"
        {
            _ = LoadHouseholdAsync();//Loadhousehold returns a task. But we dont want to await for it. Its fire & forget. Need to check why.
        }

        public async Task LoadSubscriptionsAsync()
        {
            var subscriptions = await _dbService.GetSubscriptionsAsync(HouseholdId);
            var publications = await _dbService.GetAllPublicationsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Subscriptions.Clear();
                foreach (var subscription in subscriptions)
                {
                    var pub = publications.FirstOrDefault(p => p.Id == subscription.PublicationId);
                    Subscriptions.Add(new SubscriptionDisplay
                    {
                        SubscriptionId = subscription.Id,
                        PublicationName = pub?.Name ?? "Unknown",
                        Quantity = subscription.Quantity,
                        IsActive = subscription.IsActive
                    });
                }
                HasSubscriptions = Subscriptions.Count > 0;
            });
        }

        public async Task LoadPublicationsAsync()
        {
            var publications = await _dbService.GetAllPublicationsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Publications.Clear(); ;
                foreach (var publication in publications)
                {
                    Publications.Add(publication);
                }
            });
        }

        [RelayCommand]
        public async Task AddSubscription()
        {
            try
            {
                if (SelectedPublication == null) return;

                var subscription = new Subscription
                {
                    HouseholdId = HouseholdId,
                    PublicationId = SelectedPublication.Id,
                    StartDate = DateTime.Now,
                    IsActive = true,
                    Quantity = 1
                };

                await _dbService.SaveSubscriptionAsync(subscription);
                await LoadSubscriptionsAsync();
                SelectedPublication = null;
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error", "Could not add subscription. Please try again.", "OK");
            }


        }

        [RelayCommand]
        private async Task AddPause()
        {
            try
            {
                if (SelectedSubscription == null) return;
                var pause = new PauseRequest
                {
                    SubscriptionId = SelectedSubscription.SubscriptionId,
                    FromDate = PauseFromDate,
                    ToDate = PauseToDate,
                    Reason = PauseReason
                };
                await _dbService.SavePauseRequestAsync(pause);
                SelectedSubscription = null;
                PauseReason = string.Empty;

                await Shell.Current.DisplayAlertAsync("Success", "Pause Added Successfully","OK");
            }
            catch
            {
                await Shell.Current.DisplayAlertAsync("Error", "Could not add pause. Please try again.", "OK");
            }
        }

        [RelayCommand]
        private async Task GenerateBill()
        {
            try
            {
                var bill = await _billingService.GenerateBillAsync(HouseholdId, SelectedMonth, SelectedYear);
                await Shell.Current.GoToAsync($"bill?id={bill.Id}"); //navigate to the Bill page and tell it which bill to show
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error", "Could not add generate bill. Please try again.", "OK");
            }
        }

        [RelayCommand]
        private void TogglePausedForm()
        {
            ShowPauseForm = !ShowPauseForm;
            PauseReason = string.Empty;
        }
    }
}
