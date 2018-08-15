using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace SADM.Controls
{
    public partial class ListPopup : PopupPage
    {
        protected TaskCompletionSource<string> task;

        public ListPopup()
        {
            InitializeComponent();
        }

        public ListPopup(TaskCompletionSource<string> task, string title, IEnumerable<string> list, string cancelButton) : this()
        {
            this.task = task;
            TitleLabel.Text = title;
            CancelButton.Text = cancelButton;
            OptionListView.ItemsSource = list;
            OptionListView.HeightRequest = 25 * (list.Count() < 8 ? list.Count() : 8);
        }

        void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            task?.SetResult(e.SelectedItem as string);
        }

        public void HidePopup(object sender, EventArgs e)
        {
            task?.SetResult(null);
        }

        protected override bool OnBackButtonPressed()
        {
            task?.SetResult(null);
            return false;
        }
    }
}
