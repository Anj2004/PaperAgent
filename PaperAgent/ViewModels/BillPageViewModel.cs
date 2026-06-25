using System;
using System.Collections.Generic;
using System.Text;
using PaperAgent.Services;
using System.Collections.ObjectModel;
using PaperAgent.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Android.Support.Customtabs.Trusted;

namespace PaperAgent.ViewModels
{

    [QueryProperty("BillId","id")]
    public partial class BillPageViewModel: ObservableObject 
    {

        [ObservableProperty]
        private decimal _totalAmount;

        [ObservableProperty]
        private string _householdName;

        [ObservableProperty]
        private int _billId;

        [ObservableProperty]
        private bool _isPaid; 

        public ObservableCollection<BillLineItem> LineItems { get; set; } = new();

        private readonly DatabaseService _dbService;
        public BillPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;

        }

        partial void OnBillIdChanged(int value)
        {
            _ = LoadBillAsync(value);
        }

        public async Task LoadBillAsync(int id)
        {
            var bill = await _dbService.GetBillByIdAsync(id);
            if (bill == null) return;

            TotalAmount = bill.TotalAmount;

            IsPaid = bill.IsPaid;

            var household = await _dbService.GetHouseholdByIdAsync(bill.HouseholdId);
            HouseholdName = household?.Name ?? "Unknown";

            var items = await _dbService.GetBillLineItemsAsync(id);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LineItems.Clear();
                foreach (var item in items)
                {
                    LineItems.Add(item);
                }
            });

        }

        [RelayCommand]
        private async Task MarkAsPaid()
        {
            var bill = await _dbService.GetBillByIdAsync(BillId);
            if (bill == null) return;

            bill.IsPaid = true;
            bill.PaidAt = DateTime.Now;

            await _dbService.UpdateBillAsync(bill);
            IsPaid = true;
        }
    }
}
