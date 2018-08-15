using System.Windows.Input;
using Xamarin.Forms;

namespace SADM.Controls
{
	public class ImprovedListView : ListView
    {
        public bool ShowItemSelected
        {
            get => (bool)GetValue(ShowItemSelectedProperty);
            set => SetValue(ShowItemSelectedProperty, value);
        }

        public ICommand ItemSelectedCommand
        {
            get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        public static readonly BindableProperty ShowItemSelectedProperty =
            BindableProperty.Create(nameof(ShowItemSelected), typeof(bool), typeof(ImprovedListView), false);
        
        public static readonly BindableProperty ItemSelectedCommandProperty =
            BindableProperty.Create(nameof(ItemSelectedCommand), typeof(ICommand), typeof(ImprovedListView), null);

        public ImprovedListView()
        {
            ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(SelectedItem != null)
            {
                if (!ShowItemSelected)
                {
                    SelectedItem = null;
                }
                ItemSelectedCommand?.Execute(e.SelectedItem);
            }
        }
    }
}
