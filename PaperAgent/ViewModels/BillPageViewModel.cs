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
        private readonly DatabaseService _dbService;
        public BillPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;

        }

        [ObservableProperty]
        private int _billId;
    }
}
