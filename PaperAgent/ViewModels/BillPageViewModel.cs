using System;
using System.Collections.Generic;
using System.Text;
using PaperAgent.Services;

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

        private readonly DatabaseService _dbService;
        public BillPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;

        }

        [ObservableProperty]
        private int _billId;

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
        }
    }
}
