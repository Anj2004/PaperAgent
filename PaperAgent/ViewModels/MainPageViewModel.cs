using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PaperAgent.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageTitle = "Our Route";
        public void ChangedTitle()
        {
            PageTitle = "Changed!";
        }
    } 
}
