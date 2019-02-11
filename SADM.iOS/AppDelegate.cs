using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace SADM.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyB7PdFV5A9xAXuBTY2ZIw_Jlk8blh_ilZM");
            Renderers.FloatingActionButtonRenderer.InitRenderer();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {

        }
    }
}
