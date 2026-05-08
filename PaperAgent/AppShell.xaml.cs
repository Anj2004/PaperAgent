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
        }

    }
}
