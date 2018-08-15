using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

namespace SADM.Controls
{
    public partial class TermsAndConditionsPopUp : PopupPage
    {
        protected TaskCompletionSource<bool> task;

        public TermsAndConditionsPopUp()
        {
            InitializeComponent();
        }

        public TermsAndConditionsPopUp(TaskCompletionSource<bool> task, string title, string message, string cancelButton)
        {
            InitializeComponent();
            this.task = task;
            TitleLabel.Text = title;
            TextLabel.Text = message;
            CancelButton.Text = cancelButton;
        }

        public TermsAndConditionsPopUp(TaskCompletionSource<bool> task, string title, string message, string title2, string message2, string cancelButton)
        {
            InitializeComponent();
            this.task = task;
            TitleLabel.Text = title;
            TextLabel.Text = message;
            Title2Label.IsVisible = true;
            Text2Label.IsVisible = true;
            Title2Label.Text = title2;
            Text2Label.Text = message2;
            CancelButton.Text = cancelButton;

        }

        void Handle_FocusChangeRequested(object sender, Xamarin.Forms.VisualElement.FocusRequestArgs e)
        {
            throw new NotImplementedException();
        }

        public void HidePopup(object sender, EventArgs e)
        {
            task?.SetResult(true);
        }

        protected override bool OnBackButtonPressed()
        {
            task?.SetResult(false);
            return false;
        }
    }
}
