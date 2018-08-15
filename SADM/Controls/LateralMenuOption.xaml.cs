using System.Windows.Input;
using Xamarin.Forms;

namespace SADM.Controls
{
    public partial class LateralMenuOption : ContentView
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LateralMenuOption), null);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(InputBox), string.Empty);
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(InputBox), null);
        public LateralMenuOption()
        {
            InitializeComponent();
            OptionStackLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Command?.Execute(null))
            });
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                TextLabel.Text = Text;
            }
            if (propertyName == ImageProperty.PropertyName)
            {
                IconImage.IsVisible = true;
                IconImage.Source = ImageSource.FromFile(Image);
            }
        }
    }
}
