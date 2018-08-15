using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using SADM.Enums;

namespace SADM.Controls
{
    public partial class DownloadBillPopup : PopupPage
    {
        protected TaskCompletionSource<DownloadBillPopupResult> task;

        public DownloadBillPopup()
        {
            InitializeComponent();
        }

        public DownloadBillPopup(TaskCompletionSource<DownloadBillPopupResult> task, string nis, string first, string second, string third, string acceptButton) : this()
        {
            this.task = task;
            TitleLabel.Text = string.Format(AppResources.NisFormat, nis);
            FirstMonthLabel.Text = first;
            if(second != null)
            {
                SecondMonthLabel.IsVisible = true;
                SecondStackLayout.IsVisible = true;
                SecondMonthLabel.Text = second;

                if(third != null)
                {
                    ThirdMonthLabel.IsVisible = true;
                    ThirdStackLayout.IsVisible = true;
                    ThirdMonthLabel.Text = third;
                }
            }

            ContinueButton.Text = acceptButton;
        }

        public void HidePopup(object sender, EventArgs e)
        {
            if(sender == ContinueButton)
            {
                task?.SetResult(DownloadBillPopupResult.Cancel);
            }
            else if(sender == FirstLeftButton)
            {
                task?.SetResult(DownloadBillPopupResult.FirstPdf);
            }
            else if (sender == FirstRigthButton)
            {
                task?.SetResult(DownloadBillPopupResult.FirstXml);
            }

            else if (sender == SecondLeftButton)
            {
                task?.SetResult(DownloadBillPopupResult.SecondPdf);
            }
            else if (sender == SecondRigthButton)
            {
                task?.SetResult(DownloadBillPopupResult.SecondXml);
            }

            else if (sender == ThirdLeftButton)
            {
                task?.SetResult(DownloadBillPopupResult.ThirdPdf);
            }
            else if (sender == ThirdRigthButton)
            {
                task?.SetResult(DownloadBillPopupResult.ThirdXml);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            task?.SetResult(DownloadBillPopupResult.Cancel);
            return true;
        }
    }
}
