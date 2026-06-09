using System;
using System.Collections.Generic;
using System.Text;
using PaperAgent.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace PaperAgent.ViewModels
{
    public partial class BillPageViewModel: ObservableObject 
    {
        private readonly DatabaseService _dbService;
        public BillPageViewModel(DatabaseService dbService)
        {
            _dbService = dbService;

        }

    }
}
