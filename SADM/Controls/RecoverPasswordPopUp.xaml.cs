﻿using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

namespace SADM.Controls
{
    public partial class RecoverPasswordPopup : PopupPage
    {
        protected TaskCompletionSource<bool> task;

        public RecoverPasswordPopup()
        {
            InitializeComponent();
        }

        public RecoverPasswordPopup(TaskCompletionSource<bool> task, string title, string message, string acceptButton, string cancelButton)
        {
            InitializeComponent();
            this.task = task;
            TitleLabel.Text = title;
            MessageLabel.Text = message;
            AcceptButton.Text = acceptButton;
            CancelButton.Text = cancelButton;
        }

        public void HidePopup(object sender, EventArgs e)
        {
            task?.SetResult(sender == AcceptButton);
        }

        protected override bool OnBackButtonPressed()
        {
            task?.SetResult(false);
            return false;
        }
    }
}
