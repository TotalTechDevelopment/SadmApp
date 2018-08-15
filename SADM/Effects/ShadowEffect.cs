using Xamarin.Forms;

namespace SADM.Effects
{
    public class ShadowEffect : RoutingEffect
    {
        public float Radius { get; set; }

        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShadowEffect() : base($"sadm.{nameof(ShadowEffect)}")
        {

        }
    }
}
