using Xamarin.Forms;

namespace SADM.Controls
{
    public class CounterIcon : Image
    {
        public static readonly BindableProperty CounterProperty =
            BindableProperty.Create(nameof(Counter), typeof(int), typeof(CounterIcon), default(int));
        
        public int Counter
        {
            get => (int)GetValue(CounterProperty);
            set => SetValue(CounterProperty, value);
        }
    }
}
