
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Multilingual;
using Prism.Ioc;
using Rg.Plugins.Popup.Services;
using RS_SHOP_Dev.Resources;
using RS_SHOP_Dev.Services;
using RS_SHOP_Dev.Views;
using RS_SHOP_Dev.Views.PopupViews.Alerts;
using RS_SHOP_Dev.Views.Wallets;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RS_SHOP_Dev
{
    public partial class App : Application
    {
        private readonly LoginSignupService _apiServices = new LoginSignupService();
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {

            //comment by Prakash
            
            InitializeComponent();
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new IntroPage())
                {
                    BarBackgroundColor = Color.SkyBlue,
                    BarTextColor = Color.FromHex("#00c3ff"),
                };
            }
            else

                MainPage = new NavigationPage(new Home(0));

        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.Host.EndsWith("xamboy.com", StringComparison.OrdinalIgnoreCase))
            {

                if (uri.Segments != null && uri.Segments.Length == 3)
                {
                    var action = uri.Segments[1].Replace("/", "");
                    var msg = uri.Segments[2];

                    switch (action)
                    {
                        case "hello":
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    //await Current.MainPage.DisplayAlert("hello", msg.Replace("&", " "), "ok");
                                    await Application.Current.MainPage.Navigation.PushAsync(new Forgot(msg));
                                });
                            }

                            break;
                        case "email":
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                                    var contents = await _apiServices.ActivateAccount(msg);
                                    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(contents);
                                    var Message = jwtDynamic.Value<string>("Message");
                                    var UserId = jwtDynamic.Value<string>("User_Id");
                                    if (Message.Contains("Your acoount has been successfully activated"))
                                    {
                                        await PopupNavigation.Instance.PushAsync(new AccountActivatedAlert(Message, UserId));
                                    }
                                    else
                                    {
                                        await PopupNavigation.Instance.PushAsync(new LoginAlert(Message));
                                    }
                                });
                            }

                            break;

                        default:
                            Xamarin.Forms.Device.OpenUri(uri);
                            break;
                    }
                }
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
