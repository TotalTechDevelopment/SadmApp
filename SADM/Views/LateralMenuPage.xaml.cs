using Prism.Navigation;
using Xamarin.Forms;

namespace SADM.Views
{
    public partial class LateralMenuPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public LateralMenuPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => false;
    }
}
