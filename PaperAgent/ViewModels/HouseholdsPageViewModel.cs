using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using PaperAgent.Models;

namespace PaperAgent.ViewModels
{
    public partial class HouseholdsPageViewModel : ObservableObject
    {
        public ObservableCollection<Household> Households { get; set; } = new();
    }
}
