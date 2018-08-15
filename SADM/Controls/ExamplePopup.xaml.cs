using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

namespace SADM.Controls
{
    public partial class ExamplePopup : PopupPage
    {
        protected TaskCompletionSource<bool> task;

        public ExamplePopup()
        {
            InitializeComponent();
        }

        public ExamplePopup(TaskCompletionSource<bool> task, string message, string exampleImage, string acceptButton) : this()
        {
            this.task = task;
            MessageLabel.Text = message;
            ExampleImage.Source = exampleImage;
            ContinueButton.Text = acceptButton;
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
