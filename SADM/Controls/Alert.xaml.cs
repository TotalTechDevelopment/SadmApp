using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

namespace SADM.Controls
{
    public partial class Alert : PopupPage
    {
        protected TaskCompletionSource<bool> task;

        public Alert()
        {
            InitializeComponent();
        }

        public Alert(TaskCompletionSource<bool> task, string message, string buttonText)
        {
            InitializeComponent();
            this.task = task;
            MessageLabel.Text = message;
            ContinueButton.Text = buttonText;
        }

        public void HidePopup(object sender, EventArgs e)
        {
            task?.SetResult(true);
        }

        protected override bool OnBackButtonPressed()
        {
            task?.SetResult(true);
            return true;
        }
    }
}
