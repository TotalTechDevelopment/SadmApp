using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SADM.Controls
{
    public class FlipFlop : Image
    {
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlipFlop), null);
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlipFlop), null);
        
        public bool Pressed
        {
            get => (bool)GetValue(PressedProperty);
            set => SetValue(PressedProperty, value);
        }

        public string PressedImage
        {
            get => (string)GetValue(PressedImageProperty);
            set => SetValue(PressedImageProperty, value);
        }

        public string UnPressedImage
        {
            get => (string)GetValue(UnPressedImageProperty);
            set => SetValue(UnPressedImageProperty, value);
        }

        public static readonly BindableProperty PressedProperty =
            BindableProperty.Create(nameof(Pressed), typeof(bool), typeof(FlipFlop), false, BindingMode.TwoWay, propertyChanged:PressedValueChanged);
        public static readonly BindableProperty PressedImageProperty =
            BindableProperty.Create(nameof(PressedImage), typeof(string), typeof(FlipFlop), null);
        public static readonly BindableProperty UnPressedImageProperty =
            BindableProperty.Create(nameof(UnPressedImage), typeof(string), typeof(FlipFlop), null);

        public FlipFlop()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnTapped)
            });
        }

        protected void OnTapped(object obj)
        {
            Pressed = !Pressed;
            Command?.Execute(CommandParameter);
        }

        protected static void PressedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FlipFlop flipFlop)
            {
                flipFlop.Source = newValue is bool tmp && tmp ? flipFlop.PressedImage : flipFlop.UnPressedImage;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == PressedImageProperty.PropertyName)
            {
                if (Pressed)
                {
                    Source = PressedImage;
                }
            }
            if(propertyName == UnPressedImageProperty.PropertyName)
            {
                if(!Pressed)
                {
                    Source = UnPressedImage;
                }
            }
        }
    }
}
