using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using SADM.Enums;

namespace SADM.Controls
{
    public partial class AlertPopup : PopupPage
    {
        protected TaskCompletionSource<bool> task;

        public AlertPopup()
        {
            InitializeComponent();
        }

        public AlertPopup(TaskCompletionSource<bool> task, NotificationType notificationType, string message, string acceptButton) : this()
        {
            this.task = task;
            MessageLabel.Text = message;
            ContinueButton.Text = acceptButton;
            switch(notificationType)
            {
                case NotificationType.Information:
                    IconImage.Source = "info.png";
                    break;
                case NotificationType.Error:
                    IconImage.Source = "ic_error.png";
                    break;
                case NotificationType.Success:
                    IconImage.Source = "ic_success.png";
                    break;
                default:
                    IconImage.IsVisible = false;
                    break;
            }
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
