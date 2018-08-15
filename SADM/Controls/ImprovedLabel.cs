using Xamarin.Forms;

namespace SADM.Controls
{
    public class ImprovedLabel : Label
    {
        protected const int LINES_DEFAULT = 1;

        public int Lines
        {
            get => (int)GetValue(LinesProperty);
            set => SetValue(LinesProperty, value);
        }

        public static readonly BindableProperty LinesProperty =
            BindableProperty.Create(nameof(Lines), typeof(int), typeof(ImprovedLabel), LINES_DEFAULT);
    }
}
