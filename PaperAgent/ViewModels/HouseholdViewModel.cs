using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.ViewModels
{
    internal partial class HouseholdViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _householdTitle = "My Route";
    }
}
