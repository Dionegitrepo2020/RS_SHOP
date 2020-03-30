using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Xamarin.Forms;
using Plugin.FacebookClient;
using Android.Content;
using Plugin.GoogleClient;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Prism;
using Prism.Ioc;
using Plugin.CurrentActivity;
using Plugin.Permissions;

namespace RS_SHOP_Dev.Droid
{
    [Activity(Label = "RS_SHOP_Dev", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    //Invite App Link
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "xamboy.com",
                  DataPathPrefix = "/hello",
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "https",
                  DataHost = "xamboy.com",
                  DataPathPrefix = "/email",
                  AutoVerify = true,
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                  DataScheme = "http",
                  DataHost = "xamboy.com",
                  AutoVerify = true,
                  DataPathPrefix = "/hello",
                  Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Author:Omkar - Carousel View,FacebookClientManager Init
            Forms.SetFlags("CollectionView_Experimental");
            FacebookClientManager.Initialize(this);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this,savedInstanceState);
            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-2380813622558954/8286981453");
            //
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Image
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //Author:Omkar - GoogleClientManager Init
            GoogleClientManager.Initialize(this);
            //

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /**/
       
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, intent);
        }

    }
}