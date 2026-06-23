using System;
using System.Collections.Generic;
using System.Text;
using PaperAgent.Services;
using System.Collections.ObjectModel;
using PaperAgent.Models;

using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}
