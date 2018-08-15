using Xamarin.Forms;

namespace SADM.Views
{
    /// <summary>
    /// Page base.
    /// </summary>
    public partial class PageBase : ContentPage
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageBase()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }
    }
}
