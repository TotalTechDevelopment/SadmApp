using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace SADM.Controls
{
    public partial class ProgressDialog : PopupPage
    {

        public ProgressDialog()
        {
            InitializeComponent();
        }

        public ProgressDialog(string message)
        {
            InitializeComponent();
            MessageLabel.Text = message;
        }

        protected override bool OnBackButtonPressed()
        {
            //disabled this action
            return false;
        }
    }
}
