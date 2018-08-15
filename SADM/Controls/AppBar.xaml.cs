using System.Windows.Input;
using Xamarin.Forms;

namespace SADM.Controls
{
    public partial class AppBar : ContentView
    {
        public ICommand LeftIconCommand
        {
            get => (ICommand)GetValue(LeftIconCommandProperty);
            set => SetValue(LeftIconCommandProperty, value);
        }
        public ICommand RightIconCommand
        {
            get => (ICommand)GetValue(RightIconCommandProperty);
            set => SetValue(RightIconCommandProperty, value);
        }

        public string LeftIcon
        {
            get => (string)GetValue(LeftIconProperty);
            set => SetValue(LeftIconProperty, value);
        }
        public string RightIcon
        {
            get => (string)GetValue(RightIconProperty);
            set => SetValue(RightIconProperty, value);
        }

        public static readonly BindableProperty LeftIconCommandProperty =
            BindableProperty.Create(nameof(LeftIconCommand), typeof(ICommand), typeof(InputBox), null);
        public static readonly BindableProperty RightIconCommandProperty =
            BindableProperty.Create(nameof(RightIconCommand), typeof(ICommand), typeof(InputBox), null);
        public static readonly BindableProperty LeftIconProperty =
            BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(InputBox), null);
        public static readonly BindableProperty RightIconProperty =
            BindableProperty.Create(nameof(RightIcon), typeof(string), typeof(InputBox), null);

        public AppBar()
        {
            InitializeComponent();
            LeftIconImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => LeftIconCommand?.Execute(null))
            });
            RightIconImage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => RightIconCommand?.Execute(null))
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == LeftIconProperty.PropertyName)
            {
                LeftIconImage.IsVisible = true;
                LeftIconImage.Source = ImageSource.FromFile(LeftIcon);
            }
            if (propertyName == RightIconProperty.PropertyName)
            {
                RightIconImage.IsVisible = true;
                RightIconImage.Source = ImageSource.FromFile(RightIcon);
            }
        }
    }
}
