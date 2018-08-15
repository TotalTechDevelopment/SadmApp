using System;
using System.Windows.Input;
using SADM.Behaviors;
using SADM.Extensions;
using Xamarin.Forms;

namespace SADM.Controls
{
    /// <summary>
    /// Control for user inputs.
    /// </summary>
    public partial class InputBox : ImprovedFrame
    {
        public ICommand ImageLeftCommand
        {
            get => (ICommand)GetValue(ImageLeftCommandProperty);
            set => SetValue(ImageLeftCommandProperty, value);
        }
        public ICommand ImageRightCommand
        {
            get => (ICommand)GetValue(ImageRightCommandProperty);
            set => SetValue(ImageRightCommandProperty, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public string ImageLeft
        {
            get => (string)GetValue(ImageLeftProperty);
            set => SetValue(ImageLeftProperty, value);
        }
        public string ImageRight
        {
            get => (string)GetValue(ImageRightProperty);
            set => SetValue(ImageRightProperty, value);
        }
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public event EventHandler<TextChangedEventArgs> TextChanged;

        public static readonly BindableProperty ImageLeftCommandProperty =
            BindableProperty.Create(nameof(ImageLeftCommand), typeof(ICommand), typeof(InputBox), null);
        public static readonly BindableProperty ImageRightCommandProperty =
            BindableProperty.Create(nameof(ImageRightCommand), typeof(ICommand), typeof(InputBox), null);
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(InputBox), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(InputBox), string.Empty);
        public static readonly BindableProperty ImageLeftProperty =
            BindableProperty.Create(nameof(ImageLeft), typeof(string), typeof(InputBox), null);
        public static readonly BindableProperty ImageRightProperty =
            BindableProperty.Create(nameof(ImageRight), typeof(string), typeof(InputBox), null);
        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(InputBox), Keyboard.Default);
        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(InputBox), false);
        public static readonly BindableProperty IsRequiredProperty =
            BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(InputBox), false);

        public InputBox()
        {
            InitializeComponent();
            RightImage.GestureRecognizers.Add(new TapGestureRecognizer 
            { 
                Command = new Command(() => {
                    if(ImageRightCommand is null)
                    {
                        Input.Focus();
                    }
                    ImageRightCommand?.Execute(null);
                }) 
            });
            LeftImage.GestureRecognizers.Add(new TapGestureRecognizer
            { 
                Command = new Command(() => {
                    if (ImageLeftCommand is null)
                    {
                        Input.Focus();
                    }
                    ImageLeftCommand?.Execute(null);
                }) 
            });
            Input.PropertyChanged += EntryPropertyChanged;
            Input.TextChanged += (s, e) => TextChanged?.Invoke(s, e);
            CornerRadius = 5;
            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Input.Focus()) });
        }

        protected void EntryPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TextProperty.PropertyName)
            {
                Text = Input.Text;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                Input.Text = Text;
            }
            if (propertyName == PlaceholderProperty.PropertyName)
            {
                Input.Placeholder = Placeholder;
            }
            if (propertyName == ImageLeftProperty.PropertyName)
            {
                LeftImage.IsVisible = true;
                LeftImage.Source = ImageSource.FromFile(ImageLeft);
            }
            if (propertyName == ImageRightProperty.PropertyName)
            {
                RightImage.IsVisible = true;
                RightImage.Source = ImageSource.FromFile(ImageRight);
            }
            if (propertyName == KeyboardProperty.PropertyName)
            {
                Input.Keyboard = Keyboard;
            }
            if (propertyName == IsPasswordProperty.PropertyName)
            {
                Input.IsPassword = IsPassword;
            }
            if (propertyName == IsRequiredProperty.PropertyName)
            {
                RequiredLabel.IsVisible = IsRequired;
            }
        }
    }
}
