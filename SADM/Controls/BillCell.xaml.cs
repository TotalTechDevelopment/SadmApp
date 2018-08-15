using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SADM.Controls
{
    public partial class BillCell : ViewCell
    {
        public ICommand DownloadCommand
        {
            get => (ICommand)GetValue(DownloadCommandProperty);
            set => SetValue(DownloadCommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty DownloadCommandProperty =
            BindableProperty.Create(nameof(DownloadCommand), typeof(ICommand), typeof(BillCell), null);
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(BillCell), null);
        
        public BillCell()
        {
            InitializeComponent();
        }

        void OnDownloadClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnDownloadClicked");
            DownloadCommand?.Execute(this);
        }
    }
}
