using PaperAgent.Models;
using PaperAgent.Services;
using PaperAgent.Views;

namespace PaperAgent
{
    public partial class AppShell : Shell
    {

        public AppShell(HouseholdsPage householdsPage)
        {
            InitializeComponent();
            // You can now use the injected HouseholdsPage instance
            Routing.RegisterRoute("householddetail",typeof(HouseholdDetailPage));//when someone navigates to householddetail, show HouseholdDetailPage

            Routing.RegisterRoute("publications", typeof(PublicationsPage));//Likewise
        }

    }
}
