using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace SADM.Droid.Splash
{
    /// <summary>
    /// Splash activity.
    /// </summary>
    [Activity(MainLauncher = true, Theme = "@style/SplashTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, 
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            Finish();
        }

        public override void OnBackPressed()
        {
            //Disabled for effect Splash
        }
    }
}
