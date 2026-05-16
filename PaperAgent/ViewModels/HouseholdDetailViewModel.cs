using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using PaperAgent.Services;

namespace PaperAgent.ViewModels
{
    public partial class HouseholdDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;
        public HouseholdDetailViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }
    }
}
