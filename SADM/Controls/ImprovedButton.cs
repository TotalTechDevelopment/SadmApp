using Xamarin.Forms;

namespace SADM.Controls
{
    /// <summary>
    /// Custom Button for the App.
    /// </summary>
    public class ImprovedButton : Button
    {
        protected const int BORDER_RADIUS_DEFAULT = 4;
        protected const double BORDER_WIDTH = 4.0;

        public Color ImprovedBorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public int ImprovedBorderRadius
        {
            get => (int)GetValue(ImprovedBorderRadiusProperty);
            set => SetValue(ImprovedBorderRadiusProperty, value);
        }
        public double ImprovedBorderWidth
        {
            get => (double)GetValue(ImprovedBorderWidthProperty);
            set => SetValue(ImprovedBorderWidthProperty, value);
        }
        public Color ImprovedBackgroundColor
        {
            get => (Color)GetValue(ImprovedBackgroundColorProperty);
            set => SetValue(ImprovedBackgroundColorProperty, value);
        }

        public static readonly BindableProperty ImprovedBorderColorProperty =
            BindableProperty.Create(nameof(ImprovedBorderColor), typeof(Color), typeof(ImprovedButton), Color.Default);
        public static readonly BindableProperty ImprovedBorderRadiusProperty =
            BindableProperty.Create(nameof(ImprovedBorderRadius), typeof(int), typeof(ImprovedButton), BORDER_RADIUS_DEFAULT);
        public static readonly BindableProperty ImprovedBorderWidthProperty =
            BindableProperty.Create(nameof(ImprovedBorderWidth), typeof(double), typeof(ImprovedButton),BORDER_WIDTH);
        public static readonly BindableProperty ImprovedBackgroundColorProperty =
            BindableProperty.Create(nameof(ImprovedBorderColor), typeof(Color), typeof(ImprovedButton), Color.Default);
    }
}
