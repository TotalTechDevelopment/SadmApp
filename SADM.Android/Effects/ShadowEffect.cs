using System;
using System.Linq;
using SADM.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(ShadowEffect), nameof(ShadowEffect))]
namespace SADM.Droid.Effects
{
    public class ShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control ?? Container as Android.Views.View;

                var effect = (SADM.Effects.ShadowEffect)Element.Effects.FirstOrDefault(e => e is SADM.Effects.ShadowEffect);

                if (effect != null)
                {
                    control.Elevation = effect.Radius * 3;
                    control.TranslationZ = effect.DistanceY * 3;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
