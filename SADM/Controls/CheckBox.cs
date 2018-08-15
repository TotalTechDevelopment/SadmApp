using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SADM.Controls
{
    public class CheckBox : ContentView
    {
        protected Grid ContentGrid;
        protected ContentView ContentContainer;
        public Label TextContainer;
        protected Image ImageContainer;

        public CheckBox()
        {
            var TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += TapGestureOnTapped;
            GestureRecognizers.Add(TapGesture);

            ContentGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            ImageContainer = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            ImageContainer.HeightRequest = HeightRequest;
            ImageContainer.WidthRequest = WidthRequest;
            ContentGrid.Children.Add(ImageContainer);
            ContentContainer = new ContentView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Grid.SetColumn(ContentContainer, 1);

            TextContainer = new Label
            {
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            ContentContainer.Content = TextContainer;
            ContentGrid.Children.Add(ContentContainer);
            Content = ContentGrid;
            Image.Source = "checkoff.png";
            BackgroundColor = Color.Transparent;
        }



        public static BindableProperty CheckedProperty = BindableProperty.Create(
            propertyName: "Checked",
            returnType: typeof(Boolean?),
            declaringType: typeof(CheckBox),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: CheckedValueChanged);

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(String),
            declaringType: typeof(CheckBox),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextValueChanged);

        public Boolean? Checked
        {
            get => GetValue(CheckedProperty) is Boolean value ? new Boolean?(value) : null;
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        protected static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CheckBox checkBox)
            {
                checkBox.Image.Source = newValue is Boolean tmp && tmp ? "checkon.png" : "checkoff.png";
            }
        }

        public event EventHandler CheckedChanged;
        protected void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        private Boolean isCheckBoxEnabled = true;
        public Boolean IsCheckBoxEnabled
        {
            get { return isCheckBoxEnabled; }
            set
            {
                isCheckBoxEnabled = value;
                OnPropertyChanged();
                Opacity = value ? 1 : .5;
                IsEnabled = value;
            }
        }

        public event EventHandler Clicked;
        protected void TapGestureOnTapped(object sender, EventArgs eventArgs)
        {
            if (IsCheckBoxEnabled)
            {
                Checked = !Checked;
                Clicked?.Invoke(this, new EventArgs());
            }
        }

        protected static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CheckBox checkBox)
            {
                checkBox.TextContainer.Text = newValue as string;
            }
        }

        public event EventHandler TextChanged;
        protected void RaiseTextChanged()
        {
            TextChanged?.Invoke(this, EventArgs.Empty);
        }

        public Image Image
        {
            get { return ImageContainer; }
            set { ImageContainer = value; }
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
                RaiseTextChanged();
            }
        }
    }
}