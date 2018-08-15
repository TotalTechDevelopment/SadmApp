using Xamarin.Forms;

namespace SADM.Controls
{
    public class ImprovedImage : Image
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(ImprovedImage), Color.Black);
        public Color TintColor { get => (Color)GetValue(TintColorProperty); set => SetValue(TintColorProperty, value); }
    }
}