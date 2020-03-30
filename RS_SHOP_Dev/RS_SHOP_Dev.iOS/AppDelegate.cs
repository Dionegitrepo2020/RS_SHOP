using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Google.SignIn;
using Naxam.Controls.Platform.iOS;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using UIKit;
using Xamarin.Forms;

namespace RS_SHOP_Dev.iOS
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
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Author:Omkar - Carousel View
            Forms.SetFlags("CollectionView_Experimental");
            Google.MobileAds.MobileAds.Configure("ca-app-pub-2380813622558954/6411627156");
            //
            Rg.Plugins.Popup.Popup.Init();
            TopTabbedRenderer.Init();
            global::Xamarin.Forms.Forms.Init();

         //   Plugin.InputKit.Platforms.iOS.Config.Init();

            //  GoogleClientManager.Initialize();
            LoadApplication(new App());
            Plugin.InputKit.Platforms.iOS.Config.Init();
            //Author:Omkar - FacebookClientManager Init
            FacebookClientManager.Initialize(app, options);
            FacebookClientManager.OnActivated();
            //GoogleClientManager.Initialize();
            //
            return base.FinishedLaunching(app, options);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return FacebookClientManager.OpenUrl(app, url, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
        }
    }
}
